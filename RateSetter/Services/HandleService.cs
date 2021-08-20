using RateSetter.Domain;
using RateSetter.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RateSetter.Services
{

    public class DistanceHandler : IHandleService
    {

        private IHandleService next;
        public void Next(IHandleService next)
        {
            this.next = next;
        }

        public bool IsValidate(User newUser, User existingUser)
        {
            double distance = newUser.GetDistanceUsers(existingUser);
            if (Math.Abs(distance) <= Base.MAX_DISTANCE)
                return false;
            else
                return next.IsValidate(newUser, existingUser);
        }
    }
    public class AddressHandler : IHandleService
    {
        private IHandleService next;
        public void Next(IHandleService next)
        {
            this.next = next;
        }
        public bool IsValidate(User newUser, User existingUser)
        {
            string addressCurrentUserOrigin = string.Join(existingUser.Address.Suburb, existingUser.Address.StreetAddress, existingUser.Address.State);
            string addressNextUserOrigin = string.Join(newUser.Address.Suburb, newUser.Address.StreetAddress, newUser.Address.State);
            string addressCurrentUserFormarted = Regex.Replace(addressCurrentUserOrigin, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
            string addressNextUserFormarted = Regex.Replace(addressNextUserOrigin, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
            if (addressCurrentUserFormarted.ToUpper() == addressNextUserFormarted.ToUpper())
                return false;
            return next.IsValidate(newUser, existingUser);
        }
    }
    public class ReferralCodeHandler : IHandleService
    {
        private IHandleService next;
        public void Next(IHandleService next)
        {
            this.next = next;
        }
        public bool IsValidate(User newUser, User existingUser)
        {
            var refCodeCurrentUser = existingUser.ReferralCode.ToUpper().GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count() }).ToList();
            var refCodeNextUser = newUser.ReferralCode.ToUpper().GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count() }).ToList();
            refCodeNextUser.Sort((x1, x2) => x1.Char.CompareTo(x2.Char));
            refCodeCurrentUser.Sort((x1, x2) => x1.Char.CompareTo(x2.Char));

            //To avoid ABC123 = ABC1234
            if (refCodeCurrentUser.Count() != refCodeNextUser.Count())
                return true;

            for (int i = 0; i < refCodeCurrentUser.Count(); i++)
            {
                if (refCodeCurrentUser[i].Char != refCodeNextUser[i].Char || refCodeCurrentUser[i].Count != refCodeNextUser[i].Count)
                    return true;
            }
            return false;
        }
    }
}
