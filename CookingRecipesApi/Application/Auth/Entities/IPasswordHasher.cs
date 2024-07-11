using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Entities;
public interface IPasswordHasher
{
    string Generate( string password );
    public bool Verify( string password, string hashedPassword );
}
