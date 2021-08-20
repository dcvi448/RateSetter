using RateSetter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateSetter.Services
{
    public class UserService : IUserService
    {
        private List<User> _listUser;
        public UserService(List<User> listUsers)
        {
            _listUser = listUsers;
        }
    }
}
