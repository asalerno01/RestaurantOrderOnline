namespace Server.Old
{
    public class ReviewHelper
    {
        public long AccountId { get; set; }
        public int Rating { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
