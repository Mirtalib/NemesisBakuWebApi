namespace Application.Models.DTOs.OderDTOs
{
    public class MakeOrderDto
    {
        public string StoreId { get; set; }
        public string ClientId { get; set; }
        public List<string> ShoesIds { get; set; }
    }
}
