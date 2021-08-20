using RateSetter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateSetter.Services
{
    public interface IValidateService
    {
        // 0 : Ok, >0: existing members
        public uint Validate(User newUser, User existingUser);
    }
}
