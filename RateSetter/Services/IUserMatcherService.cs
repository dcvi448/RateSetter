using RateSetter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateSetter.Services
{
    public interface IUserMatcher
    {        
        public bool IsMatch(User newUser, User existingUser);
    }
}
