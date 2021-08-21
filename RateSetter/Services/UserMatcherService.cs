using RateSetter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateSetter.Services
{
    public class UserMatcher : IUserMatcher
    {
        public bool IsMatch(User newUser, User existingUser)
        {
            IHandleService distanceHandler = new DistanceHandler();
            IHandleService addressHandler = new AddressHandler();
            IHandleService refCodeHandler = new ReferralCodeHandler();
            distanceHandler.Next(addressHandler);
            addressHandler.Next(refCodeHandler);
            //if 2 objects are valid => not match
            return !distanceHandler.IsValidate(newUser, existingUser); 
        }
    }
}
