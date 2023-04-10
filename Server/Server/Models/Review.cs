﻿using Server.Models.Authentication;

namespace Server.Models
{
    public class Review
    {
        public long ReviewId { get; set; }
        public CustomerAccount CustomerAccount { get; set; }
        public int Rating { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}