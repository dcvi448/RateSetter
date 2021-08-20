using RateSetter.Domain;
using RateSetter.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RateSetter.Services
{

    public class ValidateService : IValidateService
    {
        uint IValidateService.Validate(User newUser, User existingUser)
        {
            return 0;
        }
    }
    public class DistanceValidate : IValidateService
    {
        private IValidateService validate;
        public DistanceValidate(IValidateService validate)
        {
            this.validate = validate;
        }
        public uint Validate(User newUser, User existingUser)
        {
            double distance = newUser.GetDistanceUsers(existingUser);
            return Math.Abs(distance) <= Base.MAX_DISTANCE ? (1 + validate.Validate(newUser, existingUser)) : 0 + validate.Validate(newUser, existingUser);
        }
    }
    public class AddressValidate : IValidateService
    {
        private IValidateService validate;
        public AddressValidate(IValidateService validate)
        {
            this.validate = validate;
        }
        public uint Validate(User newUser, User existingUser)
        {
            string addressCurrentUserOrigin = string.Join(existingUser.Address.Suburb, existingUser.Address.StreetAddress, existingUser.Address.State);
            string addressNextUserOrigin = string.Join(newUser.Address.Suburb, newUser.Address.StreetAddress, newUser.Address.State);
            string addressCurrentUserFormarted = Regex.Replace(addressCurrentUserOrigin, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
            string addressNextUserFormarted = Regex.Replace(addressNextUserOrigin, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
            if (addressCurrentUserFormarted.ToUpper() == addressNextUserFormarted.ToUpper())
                return 1 + validate.Validate(newUser, existingUser);
            return 0 + validate.Validate(newUser, existingUser);
        }
    }
    public class ReferralCodeValidate : IValidateService
    {
        private IValidateService validate;
        public ReferralCodeValidate(IValidateService validate)
        {
            this.validate = validate;
        }
        public uint Validate(User newUser, User existingUser)
        {
            var refCodeCurrentUser = existingUser.ReferralCode.ToUpper().GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count() }).ToList();
            var refCodeNextUser = newUser.ReferralCode.ToUpper().GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count() }).ToList();
            refCodeNextUser.Sort((x1, x2) => x1.Char.CompareTo(x2.Char));
            refCodeCurrentUser.Sort((x1, x2) => x1.Char.CompareTo(x2.Char));

            //To avoid ABC123 = ABC1234
            if (refCodeCurrentUser.Count() != refCodeNextUser.Count())
                return 0 + validate.Validate(newUser, existingUser);

            for (int i = 0; i < refCodeCurrentUser.Count(); i++)
            {
                if (refCodeCurrentUser[i].Char != refCodeNextUser[i].Char || refCodeCurrentUser[i].Count != refCodeNextUser[i].Count)
                    return 0 + validate.Validate(newUser, existingUser);
            }
            return 1 + validate.Validate(newUser, existingUser);
        }
    }

}
