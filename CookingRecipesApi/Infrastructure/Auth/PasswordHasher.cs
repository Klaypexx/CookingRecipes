using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Auth.Entities;

namespace Infrastructure.Auth;
public class PasswordHasher : IPasswordHasher
{
    public string Generate( string password ) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword( password );

    public bool Verify( string password, string hashedPassword ) =>
        BCrypt.Net.BCrypt.EnhancedVerify( password, hashedPassword );
}
