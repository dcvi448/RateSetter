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
            if (existingUser.ReferralCode.Length != newUser.ReferralCode.Length)
                return true;
            if (existingUser.ReferralCode == newUser.ReferralCode)
                return false;
            int ep = 0;
            bool haveReversed = false;            
            char[] existingUserArray = existingUser.ReferralCode.ToArray();
            char[] newUserUserArray = newUser.ReferralCode.ToArray();
            while(ep < existingUserArray.Count())
            {
                if(existingUserArray[ep] != newUserUserArray[ep])
                {
                    int np = ep + Base.INVERSER_NUMBER_CHARACTER - 1;
                    while(Math.Abs(np-ep) < Base.INVERSER_NUMBER_CHARACTER)
                    {
                        if (existingUserArray[ep] != newUserUserArray[np])
                            return true;
                        else
                        {
                            if (haveReversed)
                                return true;
                            np--;
                            if (Math.Abs(np - (ep + 1)) < Base.INVERSER_NUMBER_CHARACTER)
                                ep++;
                        }
                    }
                    haveReversed = true;
                }
                ep++;
            }
            return false;
        }
    }
}
