using Server.Models.Authentication;

namespace Server.Models
{
    public class ReviewDTO
    {
        public long ReviewId { get; set; }
        public long AccountId { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
