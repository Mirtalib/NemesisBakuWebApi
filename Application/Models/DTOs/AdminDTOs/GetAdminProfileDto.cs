﻿namespace Application.Models.DTOs.AdminDTOs
{
    public class GetAdminProfileDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BrithDate { get; set; }
    }
}
