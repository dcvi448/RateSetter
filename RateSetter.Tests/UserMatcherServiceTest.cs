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
        // Case 1: ABC123 vs ABC321
        public void UserSameRefCodeT1()
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
                ReferralCode = "ABC321",
                Address =
                new Address() { Latitude = (decimal)10.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan Tu", Suburb = "18" }
            };
            Assert.True(userMatcher.IsMatch(newUser, existingUser));
        }
        [Fact]
        // Case 2: ABC123 vs CBA321
        public void UserSameRefCodeT2()
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
                new Address() { Latitude = (decimal)10.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan Tu", Suburb = "18" }
            };
            Assert.False(userMatcher.IsMatch(newUser, existingUser));
        }
        [Fact]
        // Case 3: ABC123 vs AB21C3
        public void UserSameRefCodeT3()
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
                ReferralCode = "AB21C3",
                Address =
                new Address() { Latitude = (decimal)10.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan Tu", Suburb = "18" }
            };
            Assert.True(userMatcher.IsMatch(newUser, existingUser));
        }
        [Fact]
        // Case 4: ABC1234 vs AB21C34
        public void UserSameRefCodeT4()
        {
            IUserMatcher userMatcher = new UserMatcher();
            User newUser = new User
            {
                Name = "Mr Duc",
                ReferralCode = "ABC1234",
                Address =
                new Address() { Latitude = (decimal)16.045740, Longitude = (decimal)108.243220, State = "VN", StreetAddress = "Phan Tu", Suburb = "41" }
            };
            User existingUser = new User
            {
                Name = "Mr Huy",
                ReferralCode = "AB21C34",
                Address =
                new Address() { Latitude = (decimal)10.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan Tu", Suburb = "18" }
            };
            Assert.True(userMatcher.IsMatch(newUser, existingUser));
        }
        [Fact]
        // Case 5: ABC1234 vs ABC4321
        public void UserSameRefCodeT5()
        {
            IUserMatcher userMatcher = new UserMatcher();
            User newUser = new User
            {
                Name = "Mr Duc",
                ReferralCode = "ABC1234",
                Address =
                new Address() { Latitude = (decimal)16.045740, Longitude = (decimal)108.243220, State = "VN", StreetAddress = "Phan Tu", Suburb = "41" }
            };
            User existingUser = new User
            {
                Name = "Mr Huy",
                ReferralCode = "ABC4321",
                Address =
                new Address() { Latitude = (decimal)10.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan Tu", Suburb = "18" }
            };
            Assert.False(userMatcher.IsMatch(newUser, existingUser));
        }
        [Fact]
        // Case 6: ABC123 vs CBA123
        public void UserSameRefCodeT6()
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
                ReferralCode = "CBA123",
                Address =
                new Address() { Latitude = (decimal)5.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan Tu", Suburb = "18" }
            };
            Assert.True(userMatcher.IsMatch(newUser, existingUser));
        }
    }
}
