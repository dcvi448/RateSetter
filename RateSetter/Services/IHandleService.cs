using RateSetter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateSetter.Services
{
    public interface IHandleService
    {       
        public bool IsValidate(User newUser, User existingUser);
        public void Next(IHandleService next);
    }
}
