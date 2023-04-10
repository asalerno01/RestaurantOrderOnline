namespace Server.Models.ItemModels
{
    public class ReviewHelper
    {
        public long CustomerAccountId { get; set; }
        public int Rating { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
