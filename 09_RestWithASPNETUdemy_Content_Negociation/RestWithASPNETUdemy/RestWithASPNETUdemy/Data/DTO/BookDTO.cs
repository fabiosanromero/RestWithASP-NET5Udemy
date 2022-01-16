using System;

namespace RestWithASPNETUdemy.Data.DTO
{
    public class BookDTO
    {
        public long id { get; set; }

        public string Author { get; set; }

        public DateTime Launch_Date { get; set; }

        public double Price { get; set; }

        public string Title { get; set; }
    }
}
