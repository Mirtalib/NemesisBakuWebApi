namespace Application.Models.DTOs.ShoesCommentDTOs
{
    public class GetShoeCommentDto
    {
        public string Id { get; set; }
        public string ShoesId { get; set; }
        public string ClientId { get; set; }
        public string Content { get; set; }
        public byte Rate { get; set; }
    }
}
