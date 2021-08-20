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
       
        uint IValidateService.Validate(List <User> listUser, User user)
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
        public uint Validate(List<User> listUser, User user)
        {
            double distance = 0.0;
            for (int i = 0; i < listUser.Count; i++)
            {
                distance = listUser[i].GetDistanceUsers(user);
                if (Math.Abs(distance) <= Base.MAX_DISTANCE)
                {
                    return 1 + validate.Validate(listUser, user);
                }
            }
            return 0 + validate.Validate(listUser, user);

        }
        public class AddressValidate : IValidateService
        {
            private IValidateService validate;
            public AddressValidate(IValidateService validate)
            {
                this.validate = validate;
            }
            public uint Validate(List<User> listUser, User user)
            {
                string addressCurrentUserOrigin = "";
                string addressNextUserOrigin = "";
                string addressCurrentUserFormarted;
                string addressNextUserFormarted;

                for (int i = 0; i < listUser.Count; i++)
                {
                    addressCurrentUserOrigin = string.Join(listUser[i].Address.Suburb, listUser[i].Address.StreetAddress, listUser[i].Address.State);
                    addressNextUserOrigin = string.Join(user.Address.Suburb, user.Address.StreetAddress, user.Address.State);
                    addressCurrentUserFormarted = Regex.Replace(addressCurrentUserOrigin, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
                    addressNextUserFormarted = Regex.Replace(addressNextUserOrigin, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
                    if (addressCurrentUserFormarted.ToUpper() == addressNextUserFormarted.ToUpper())
                        return 1 + validate.Validate(listUser, user);
                }
                return 0 + validate.Validate(listUser, user);
            }
        }
        public class ReferralCodeValidate : IValidateService
        {
            private IValidateService validate;
            public ReferralCodeValidate(IValidateService validate)
            {
                this.validate = validate;
            }
            public uint Validate(List<User> listUser, User user)
            {
                bool isValid = true;
                var refCodeNextUser = user.ReferralCode.ToUpper().GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count() }).ToList();
                refCodeNextUser.Sort((x1, x2) => x1.Char.CompareTo(x2.Char));

                foreach (var item in listUser)
                {
                    var refCodeCurrentUser = item.ReferralCode.ToUpper().GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count() }).ToList();
                    refCodeCurrentUser.Sort((x1, x2) => x1.Char.CompareTo(x2.Char));
                    for (int i = 0; i < refCodeCurrentUser.Count(); i++)
                    {
                        if (refCodeCurrentUser.Count() == refCodeNextUser.Count() && refCodeCurrentUser[i].Char == refCodeNextUser[i].Char && refCodeCurrentUser[i].Count == refCodeNextUser[i].Count)
                            isValid = false;
                        else
                        {
                            isValid = true;
                            break;
                        }
                    }
                    if (isValid == false)
                        return 1 + validate.Validate(listUser, user);
                    //Reset the flag
                    isValid = true;
                }
                return 0 + validate.Validate(listUser, user);
            }
        }
    }
}
