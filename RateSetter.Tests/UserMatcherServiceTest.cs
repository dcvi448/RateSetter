using RateSetter.Domain;
using RateSetter.Services;
using System;
using Xunit;

namespace RateSetter.Tests
{
    public class UserMatcherServiceTest
    {
        [Fact]
        public void UserSameRefCode()
        {
            IUserMatcher userMatcher = new UserMatcher();
            User newUser = new User
            {
                Name = "Mr Duc",
                ReferralCode = "Ms Thao 123",
                Address =
                new Address() { Latitude = (decimal)16.045740, Longitude = (decimal)108.243220, State = "VN", StreetAddress = "Phan Tu", Suburb = "41" }
            };
            User existingUser = new User
            {
                Name = "Mr Huy",
                ReferralCode = "Ms 321 Thao",
                Address =
                new Address() { Latitude = (decimal)10.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan Tu", Suburb = "18" }
            };
            Assert.False(userMatcher.IsMatch(newUser,existingUser));
            #region 2. Same Address
            existingUser.Address.StreetAddress = "Phan!Tu@";
            existingUser.Address.Suburb = "4!1";
            existingUser.ReferralCode = "Ms 187 Thao";
            Assert.False(userMatcher.IsMatch(newUser, existingUser));
            #endregion
        }
        [Fact]
        public void UserSameAddress()
        {
            IUserMatcher userMatcher = new UserMatcher();
            User newUser = new User
            {
                Name = "Mr Duc",
                ReferralCode = "Ms Thao 123",
                Address =
                new Address() { Latitude = (decimal)16.045740, Longitude = (decimal)108.243220, State = "VN", StreetAddress = "Phan Tu", Suburb = "41" }
            };
            User existingUser = new User
            {
                Name = "Mr Huy",
                ReferralCode = "Ms 187 Thao",
                Address =
                new Address() { Latitude = (decimal)10.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan!Tu@", Suburb = "4!1" }
            };
            Assert.False(userMatcher.IsMatch(newUser, existingUser));
        }
        [Fact]
        public void UserIsNotSame()
        {
            IUserMatcher userMatcher = new UserMatcher();
            User newUser = new User
            {
                Name = "Mr Duc",
                ReferralCode = "Ms Thao 123",
                Address =
                new Address() { Latitude = (decimal)16.045740, Longitude = (decimal)108.243220, State = "VN", StreetAddress = "Phan Tu", Suburb = "41" }
            };
            User existingUser = new User
            {
                Name = "Mr Huy",
                ReferralCode = "Ms 187 Thao",
                Address =
                new Address() { Latitude = (decimal)10.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan!Tu@", Suburb = "1!1" }
            };
            Assert.True(userMatcher.IsMatch(newUser, existingUser));
        }
        [Fact]
        public void UserIsNearEachOther()
        {
            IUserMatcher userMatcher = new UserMatcher();
            User newUser = new User
            {
                Name = "Mr Duc",
                ReferralCode = "Ms Thao 123",
                Address =
                new Address() { Latitude = (decimal)16.045740, Longitude = (decimal)108.243220, State = "VN", StreetAddress = "Phan Tu", Suburb = "41" }
            };
            User existingUser = new User
            {
                Name = "Mr Huy",
                ReferralCode = "Ms 187 Thao",
                Address =
                new Address() { Latitude = (decimal)16.045959, Longitude = (decimal)108.244199, State = "VN", StreetAddress = "Phan!Tu@", Suburb = "1!1" }
            };
            Assert.False(userMatcher.IsMatch(newUser, existingUser));
        }
    }
}
