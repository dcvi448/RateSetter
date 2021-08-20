using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateSetter.Domain
{
    public class Address
    {
        private string _streetAddress;
        private string _suburb;
        private string _state;
        public decimal _latitude;
        public decimal _longitude;
        public string StreetAddress 
        {
            get { return _streetAddress;  }
            set { _streetAddress = value; }
        }
        public string Suburb
        {
            get { return _suburb; }
            set { _suburb = value; }
        }
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }
        public decimal Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        public decimal Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
    }
}
