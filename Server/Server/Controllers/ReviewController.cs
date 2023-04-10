using Microsoft.AspNetCore.Mvc;
using SalernoServer.Models;
using Server.Models.ItemModels.Helpers;
using Server.Models.ItemModels;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ReviewController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews()
        {
            var reviews = await _context.Reviews
                .Include(r => r.CustomerAccount)
                .ToListAsync();

            var reviewDTOList = new List<ReviewDTO>();
            foreach (var review in reviews)
            {
                reviewDTOList.Add(new ReviewDTO
                {
                    ReviewId = review.ReviewId,
                    CustomerAccountId = review.CustomerAccount.CustomerAccountId,
                    Name = review.CustomerAccount.FirstName,
                    Rating = review.Rating,
                    Message = review.Message,
                    Date = review.Date
                });
            }
            return Ok(reviewDTOList);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> GetReview(long id)
        {
            //var item = await _context.Items.FindAsync(itemId);
            var review = await _context.Reviews.Include(r => r.CustomerAccount).FirstOrDefaultAsync(r => r.ReviewId == id);

            if (review is null)
            {
                return NotFound();
            }
            var reviewDTO = new ReviewDTO
            {
                ReviewId = review.ReviewId,
                CustomerAccountId = review.CustomerAccount.CustomerAccountId,
                Name = review.CustomerAccount.FirstName,
                Rating = review.Rating,
                Message = review.Message,
                Date = review.Date
            };
            return Ok(reviewDTO);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateReview([FromBody] ReviewHelper review)
        {
            Console.WriteLine("hey");
            var refreshToken = Request.Cookies["RefreshToken"];
            Console.WriteLine($"RefreshToken=>{refreshToken}");
            var customerAccount = await _context.CustomerAccounts.Where(c => c.RefreshToken.Equals(refreshToken)).FirstOrDefaultAsync();

            if (customerAccount is null) return BadRequest();
            Review newReview = new()
            {
                CustomerAccount = customerAccount,
                Rating = review.Rating,
                Message = review.Message,
                Date = review.Date
            };
            await _context.Reviews.AddAsync(newReview);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
