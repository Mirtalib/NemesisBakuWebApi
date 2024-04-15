namespace Application.Models.DTOs.CategoryDTOs
{
    public class GetCategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> ShoesIds { get; set; }
    }
}
