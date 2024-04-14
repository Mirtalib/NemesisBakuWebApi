namespace Application.Models.DTOs.ShoesDTOs
{
    public class GetShoeDto
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public string Brend { get; set; }
        public float Price { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
