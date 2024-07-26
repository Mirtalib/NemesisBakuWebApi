namespace Application.Models.DTOs.ShoesDTOs
{
    public class UpdateShoeDto
    {
        public string Id { get; set; }
        public string StoreId { get; set; }
        public string CategoryId { get; set; }
        public string Model { get; set; }
        public string Brend { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }
    }
}
