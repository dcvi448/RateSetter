using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateSetter.Domain
{
    public class User
    {
        private Address _adddress;
        private string _name;
        private string _referralCode;
        public Address Address
        {
            get { return _adddress; }
            set { _adddress = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string ReferralCode
        {
            get { return _referralCode; }
            set { _referralCode = value; }
        }
        public double GetDistanceUsers(User user)
        {
            Address currentUser = this.Address;
            Address nextUser = user.Address;
            var currentCoord = new GeoCoordinate((double)currentUser.Latitude, (double)currentUser.Longitude);
            var nextCoord = new GeoCoordinate((double)nextUser.Latitude, (double)nextUser.Longitude);
            return nextCoord.GetDistanceTo(currentCoord);
        }
    }
}
