﻿namespace Application.Models.Configuration
{
    public class JWTConfiguration
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpiresDate { get; set; }
    }
}
