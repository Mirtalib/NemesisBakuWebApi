namespace Application.Models.DTOs.AuthDTOs
{
    public class AuthTokenDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpireDate { get; set; }
    }
}