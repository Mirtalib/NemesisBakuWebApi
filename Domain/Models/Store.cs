namespace Domain.Models
{
    public class Store 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public string Email { get; set; }    
        public string Description { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public List<string> Addresses { get; set; }



        // Navigation Property
        public List<Shoe> Shoes { get; set; }
        public List<Order> Orders { get; set; }
        public List<Category> Categorys { get;set; }
    }
}