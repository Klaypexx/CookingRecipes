using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Auth.Entities;

namespace Application.Auth.Entities;
public interface ITokenProvider
{
    string GenerateJwtToken( User user );
    /*string GenerateRefreshToken();*/
}
