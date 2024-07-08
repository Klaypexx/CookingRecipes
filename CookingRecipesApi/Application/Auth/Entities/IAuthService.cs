using Domain.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Entities;
public interface IAuthService
{
    Task RegisterUser(User user);
    Task<User> GetUserByUsername(string username);
}
