using Server.Models.Authentication;

namespace Server.Old
{
    public class Review
    {
        public long ReviewId { get; set; }
        public Account Account { get; set; }
        public int Rating { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
