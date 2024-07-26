namespace Application.Models.DTOs.ShoesCommentDTOs
{
    public class CreateShoeCommentDto
    {
        public string ShoeId { get; set; }
        public string ClientId { get; set; }
        public string Content { get; set; }
        public byte Rate { get; set; }
    }
}
