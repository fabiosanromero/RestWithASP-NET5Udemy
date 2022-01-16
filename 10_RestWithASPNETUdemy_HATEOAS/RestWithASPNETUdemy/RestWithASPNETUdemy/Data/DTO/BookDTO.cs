using RestWithASPNETUdemy.Hypermedia.Abstract;
using RestWithASPNETUdemy.Hypermedia;
using System;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Data.DTO
{
    public class BookDTO: ISupportHypermedia
    {
        public long id { get; set; }

        public string Author { get; set; }

        public DateTime Launch_Date { get; set; }

        public double Price { get; set; }

        public string Title { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
