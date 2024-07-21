using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Auth.Repositories;
using Domain.Auth.Entities;

namespace Application.Validation;
public class AuthValidationRules
{
    private readonly IUserRepository _userRepository;

    public AuthValidationRules( IUserRepository userRepository )
    {
        _userRepository = userRepository;
    }

    public async Task<bool> IsUniqueUsername( string username )
    {
        User user = await _userRepository.GetByUsername( username );

        return user is null;
    }
}
