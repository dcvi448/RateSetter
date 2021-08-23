using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateSetter.Domain
{
    public class Address
    {
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
