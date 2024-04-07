using Domain.Models.Common;

namespace Domain.Models
{
    public class Client : BaseEntitiy
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<string> CommnetId { get; set; }
        public List<string> OrdersId { get; set; }
    }
}
