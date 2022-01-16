using RestWithASPNETUdemy.Hypermedia.Abstract;
using RestWithASPNETUdemy.Hypermedia;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Data.DTO
{
    public class PersonDTO:ISupportHypermedia
    {
        public long id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
