using PreTrainee_Month2.CoreLayer.Entities.Product_Entities;
using System.Text.Json.Serialization;

namespace PreTrainee_Month2.CoreLayer.Product_Entities
{
    public class Product
    {
        public int ID { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string  Description { get; set; }=string.Empty;
        public decimal Price { get; set; } = 0;
        public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;


        public int UserId { get; set; } = 0;

        //для связи в EF
        [JsonIgnore]
        public User Owner { get; set; }

        public Product()
        {
                
        }
        public Product(ProductDTO productDTO)
        {
            Name = productDTO.Name;
            Description = productDTO.Description;
            Price = productDTO.Price;
            UserId = productDTO.UserId;
        }
        public Product(ProductPutDTO productDTO)
        {
            Name = productDTO.Name;
            Description = productDTO.Description;
            Price = productDTO.Price;
        }


    }
}
