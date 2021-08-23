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
            var distance = newUser.DistanceToOtherUser(existingUser);
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
            var addressCurrentUserOrigin = string.Join(existingUser.Address.Suburb, existingUser.Address.StreetAddress, existingUser.Address.State);
            var addressNextUserOrigin = string.Join(newUser.Address.Suburb, newUser.Address.StreetAddress, newUser.Address.State);
            var addressCurrentUserFormarted = Regex.Replace(addressCurrentUserOrigin, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
            var addressNextUserFormarted = Regex.Replace(addressNextUserOrigin, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
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
            if (existingUser.ReferralCode.Length != newUser.ReferralCode.Length)
                return true;
            if (existingUser.ReferralCode == newUser.ReferralCode)
                return false;
            int ep = 0;
            bool haveReversed = false;            
            var existingUserArray = existingUser.ReferralCode.ToArray();
            var newUserArray = newUser.ReferralCode.ToArray();
            while(ep < existingUserArray.Count())
            {
                if(existingUserArray[ep] != newUserArray[ep])
                {
                    if (ep + Base.INVERSER_NUMBER_CHARACTER > existingUserArray.Count())
                        return true;
                    char[] newUserComparingArray = newUser.ReferralCode.Substring(ep, Base.INVERSER_NUMBER_CHARACTER).ToArray();
                    Array.Reverse(newUserComparingArray);
                    if (haveReversed)
                        return true;
                    if (existingUser.ReferralCode.Substring(ep, Base.INVERSER_NUMBER_CHARACTER) == new string(newUserComparingArray))
                    {
                        haveReversed = true;
                        ep += Base.INVERSER_NUMBER_CHARACTER - 1;
                    }
                }
                ep++;
            }
            return false;
        }
    }
}
