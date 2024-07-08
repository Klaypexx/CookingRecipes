using Domain.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Repositories;
public interface IUserRepository
{
    Task AddUser(User user);
    Task<User> GetUser(int userId);
    Task<User> GetByUsername(string username);

}
