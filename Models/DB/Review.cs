﻿namespace IMDBApi_Assignment3.Models.DB
{
    public class Review
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public int MovieId { get; set; }
    }
}
