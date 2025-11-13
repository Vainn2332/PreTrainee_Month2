using PreTrainee_Month2.CoreLayer.Entities.User_Entities;
using PreTrainee_Month2.CoreLayer.Product_Entities;
using PreTrainee_Month2.CoreLayer.User_Entities;

namespace PreTrainee_Month2.CoreLayer
{
    public class User////////////////////////////////////добавить DTO на put и post
    {
        public int ID { get; set; } = 0;
        public string Name { get; set; } = String.Empty;
        public string EmailAddress { get; set; } = String.Empty;
        public string Role { get; set; } = string.Empty;
        

        //для связи
        public List<Product> Products { get; set; } = [];

     
        public User(UserDTO userDTO)
        {
            Name = userDTO.Name;
            EmailAddress = userDTO.EmailAddress;
            Role = userDTO.Role;

            foreach (var item in userDTO.Products)
            {
                Product product = new Product(item);
                Products.Add(product);
            }
        }
        public User(UserPostAndPutDTO userPostAndPutDTO)
        {
            Name = userPostAndPutDTO.Name;
            EmailAddress = userPostAndPutDTO.EmailAddress;
            Role = userPostAndPutDTO.Role;        
        }
        public User()
        {

        }
    }
}
