using RateSetter.Domain;
using RateSetter.Services;
using System;
using Xunit;

namespace RateSetter.Tests
{
    public class UserMatcherServiceTest
    {
        [Fact]
        public void UserSameAddress()
        {
            IUserMatcher userMatcher = new UserMatcher();
            User newUser = new User
            {
                Name = "Mr Duc",
                ReferralCode = "ABC123",
                Address =
                new Address() { Latitude = (decimal)16.045740, Longitude = (decimal)108.243220, State = "VN", StreetAddress = "Phan Tu", Suburb = "41" }
            };
            User existingUser = new User
            {
                Name = "Mr Huy",
                ReferralCode = "CBA321",
                Address =
                new Address() { Latitude = (decimal)10.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan!Tu@", Suburb = "4!1" }
            };
            Assert.True(userMatcher.IsMatch(newUser, existingUser));
        }
        [Fact]
        public void UserIsNotSame()
        {
            IUserMatcher userMatcher = new UserMatcher();
            User newUser = new User
            {
                Name = "Mr Duc",
                ReferralCode = "ABC123",
                Address =
                new Address() { Latitude = (decimal)16.045740, Longitude = (decimal)108.243220, State = "VN", StreetAddress = "Phan Tu", Suburb = "41" }
            };
            User existingUser = new User
            {
                Name = "Mr Huy",
                ReferralCode = "CBA321",
                Address =
                new Address() { Latitude = (decimal)10.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan!Tu@", Suburb = "1!1" }
            };
            Assert.False(userMatcher.IsMatch(newUser, existingUser));
        }
        [Fact]
        public void UserIsNearEachOther()
        {
            IUserMatcher userMatcher = new UserMatcher();
            User newUser = new User
            {
                Name = "Mr Duc",
                ReferralCode = "ABC123",
                Address =
                new Address() { Latitude = (decimal)16.045740, Longitude = (decimal)108.243220, State = "VN", StreetAddress = "Phan Tu", Suburb = "41" }
            };
            User existingUser = new User
            {
                Name = "Mr Huy",
                ReferralCode = "CBA321",
                Address =
                new Address() { Latitude = (decimal)16.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan!Tu@", Suburb = "1!1" }
            };
            Assert.True(userMatcher.IsMatch(newUser, existingUser));
        }

        [Fact]
        public void UserSameRefCode()
        {
            // Case 1: ABC123 vs ABC321
            IUserMatcher userMatcher = new UserMatcher();
            User newUser = new User
            {
                Name = "Mr Duc",
                ReferralCode = "ABC123",
                Address =
                new Address() { Latitude = (decimal)16.045740, Longitude = (decimal)108.243220, State = "VN", StreetAddress = "Phan Tu", Suburb = "41" }
            };
            User existingUser = new User
            {
                Name = "Mr Huy",
                ReferralCode = "ABC321",
                Address =
                new Address() { Latitude = (decimal)10.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan Tu", Suburb = "18" }
            };
            Assert.True(userMatcher.IsMatch(newUser, existingUser));

            // Case 2: ABC123 vs CBA321
            existingUser.ReferralCode = "CBA321";
            Assert.False(userMatcher.IsMatch(newUser, existingUser));

            // Case 3: ABC123 vs AB21C3
            existingUser.ReferralCode = "AB21C3";
            Assert.True(userMatcher.IsMatch(newUser, existingUser));

            // Case 4: ABC1234 vs AB21C34
            newUser.ReferralCode = "ABC1234";
            existingUser.ReferralCode = "AB21C34";
            Assert.True(userMatcher.IsMatch(newUser, existingUser));

            // Case 5: ABC1234 vs ABC4321
            existingUser.ReferralCode = "ABC4321";
            Assert.False(userMatcher.IsMatch(newUser, existingUser));

        }
    }
}
