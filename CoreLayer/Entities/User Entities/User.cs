using PreTrainee_Month2.CoreLayer.Product_Entities;

namespace PreTrainee_Month2.CoreLayer
{
    public class User
    {
        public int ID { get; set; } = 0;
        public string Name { get; set; } = String.Empty;
        public string EmailAddress { get; set; } = String.Empty;
        public string Role { get; set; } = string.Empty;

        //для связи
        public List<Product> Products { get; set; } = [];
    }
}
