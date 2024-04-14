using Application.Models.DTOs.AuthDTOs;

namespace Application.Services.IAuthServices
{
    public interface IJWTService
    {
        AuthTokenDto GenerateSecurityToken(string id, string email, string role);
    }
}
