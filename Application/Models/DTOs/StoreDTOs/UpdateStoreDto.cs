namespace Application.Models.DTOs.StoreDTOs
{
    public class UpdateStoreDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public List<string> Addresses { get; set; }
    }
}
