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
            IValidateService validate = new ValidateService();
            validate = new DistanceValidate(validate);
            validate = new AddressValidate(validate);
            validate = new ReferralCodeValidate(validate);
        }
    }
}
