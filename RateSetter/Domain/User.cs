using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateSetter.Domain
{
    public class User
    {
        public Address Address { get; set; }
        public string Name { get; set; }
        public string ReferralCode { get; set; }
        public double DistanceToOtherUser(User user)
        {
            Address currentUser = this.Address;
            Address nextUser = user.Address;
            var currentCoord = new GeoCoordinate((double)currentUser.Latitude, (double)currentUser.Longitude);
            var nextCoord = new GeoCoordinate((double)nextUser.Latitude, (double)nextUser.Longitude);
            return nextCoord.GetDistanceTo(currentCoord);
        }
    }
}
