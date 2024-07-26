namespace Application.Models.DTOs.ClientDTOs
{
    public class UpdateClientProfileDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BrithDate { get; set; }
        public string Address { get; set; }
    }
}
