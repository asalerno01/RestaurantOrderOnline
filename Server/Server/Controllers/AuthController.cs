using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SalernoServer.Models;
using SalernoServer.Models.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Server.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Web;
using Server.Models;
using System.Security.Principal;
using SalernoServer.JwtHelpers;
using Server.Models.ShoppingCartModels;
using Server.Logger;

namespace SalernoServer
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthenticateController(
            AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAccount([FromBody] LoginModel loginModel)
        {
            var cookie = Request.Cookies["RefreshToken"];

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Email.Equals(loginModel.Email));
            if (account is null) return BadRequest("Bad email");
            if (account.Password is null) return BadRequest("Account has null password");

            var shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(s => s.AccountId == account.AccountId);
            long shoppingCartId = (shoppingCart is null) ? 0 : shoppingCart.ShoppingCartId;

            if (account is not null && account.Password.Equals(loginModel.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim("ShoppingCartId", shoppingCartId.ToString(), ClaimValueTypes.String),
                    new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var accessToken = JwtHelpers.JwtHelpers.CreateAccessToken(authClaims);
                var refreshToken = JwtHelpers.JwtHelpers.CreateRefreshToken(authClaims);

                var refTokString = new JwtSecurityTokenHandler().WriteToken(refreshToken);
                account.RefreshToken = refTokString;

                _context.Update(account);
                await _context.SaveChangesAsync();

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(1),
                    Domain = "localhost",
                    SameSite = SameSiteMode.None,
                    IsEssential = true
                };
                Response.Cookies.Append("RefreshToken", refTokString, cookieOptions);
                return Ok(new
                {
                    accountId = account.AccountId,
                    accessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                    email = account.Email,
                    firstName = account.FirstName,
                    lastName = account.LastName,
                    phoneNumber = account.PhoneNumber
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var accountExists = _context.Accounts.Where(ca => ca.Email.Equals(registerModel.Email)).Any();
            if (accountExists) return StatusCode(500);
            Account account = new()
            {
                Email = registerModel.Email,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Password = registerModel.Password,
                PhoneNumber = registerModel.PhoneNumber,
                IsVerified = true
            };
            var result = await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["RefreshToken"];
            Console.WriteLine($"Logout refresh token=>{refreshToken}");
            var foundAccount = await _context.Accounts.Where(ca => ca.RefreshToken.Equals(refreshToken)).FirstOrDefaultAsync();
            if (foundAccount is null) return BadRequest("Can't find account");
            foundAccount.RefreshToken = "";
            _context.Update(foundAccount);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddDays(-1),
                Domain = "localhost",
                SameSite = SameSiteMode.None,
                IsEssential = true
            };
            Response.Cookies.Append("RefreshToken", "", cookieOptions);
            return Ok("Done");
        }

        [HttpGet]
        [Route("unregisteredtoken")]
        public async Task<IActionResult> GetUnregisteredToken()
        {
            var newShoppingCart = new ShoppingCart();
            await _context.ShoppingCarts.AddAsync(newShoppingCart);

			var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, string.Empty),
				new Claim("ShoppingCartId", newShoppingCart.ShoppingCartId.ToString(), ClaimValueTypes.String),
				new Claim(ClaimTypes.NameIdentifier, "0"),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var accessToken = JwtHelpers.JwtHelpers.CreateRefreshToken(authClaims);

			await _context.SaveChangesAsync();

			return Ok(new
			{
				accessToken = new JwtSecurityTokenHandler().WriteToken(accessToken)
			});
		}

        [HttpGet]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            Logger.Log("==========refreshing===========");
            string? refreshToken = Request.Cookies["RefreshToken"];
            Logger.Log(refreshToken);
            if (string.IsNullOrEmpty(refreshToken)) return NoContent();
            Console.WriteLine("RefreshToken==============>" + refreshToken);
            
            var principal = JwtHelpers.JwtHelpers.ValidateToken(refreshToken);
            if (principal is null)
            {
                return BadRequest("Invalid token");
            }
            //string emailFromClaim = principal.Claims.Where(c => c.Type.Equals("Email")).FirstOrDefault().Value;
            //if (emailFromClaim is null) return BadRequest("Bad email");
            //var account = _context.Accounts.Where(a => a.Email.Equals(emailFromClaim)).FirstOrDefault();
            var accountIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (accountIdClaim is null) return BadRequest("AccountId is null");

			var account = await _context.Accounts.FindAsync(long.Parse(accountIdClaim.Value)); // check this
            if (account is null) return BadRequest("Invalid token account!");
            var tokenDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(principal.Claims.Where(c => c.Type.Equals("exp")).FirstOrDefault().Value)).UtcDateTime;
            var now = DateTime.UtcNow.ToUniversalTime();

            var valid = tokenDate >= now;

            Console.WriteLine($"Token Date => {tokenDate}, Current Date => {now}");
            if (!valid)
            {
                account.RefreshToken = "";
                Response.Cookies.Delete("refreshtoken");
                return StatusCode(403);
            }
            if (!account.RefreshToken.Equals(refreshToken))
            {
                account.RefreshToken = "";
                return BadRequest("Token does not match DB");
            }

			var shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(s => s.AccountId == account.AccountId);
			long shoppingCartId = (shoppingCart is null) ? 0 : shoppingCart.ShoppingCartId;

			var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, account.Email),
				new Claim("ShoppingCartId", shoppingCartId.ToString(), ClaimValueTypes.String),
				new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var token = JwtHelpers.JwtHelpers.CreateAccessToken(authClaims);
            
            var tok = new JwtSecurityTokenHandler().WriteToken(token);
            
            await _context.SaveChangesAsync();

            return Ok(new
            {
                accountId = account.AccountId,
                firstName = account.FirstName,
                lastName = account.LastName,
                email = account.Email,
                phoneNumber = account.PhoneNumber,
                accessToken = tok
            });
        }
    }
}