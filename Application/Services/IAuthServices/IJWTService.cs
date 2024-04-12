using Application.Models.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.IAuthServices
{
    public interface IJWTService
    {
        AuthTokenDto GenerateSecurityToken(string id, string email, string role);
    }
}
