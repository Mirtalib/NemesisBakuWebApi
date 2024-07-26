namespace Application.Models.DTOs.CourierDTOs
{
    public class GetCourierProfileDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BrithDate { get; set; }
        public byte Rate { get; set; }
        public int OrderSize { get; set; }
    }
}
