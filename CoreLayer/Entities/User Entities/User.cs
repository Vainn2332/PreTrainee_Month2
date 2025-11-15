using PreTrainee_Month2.CoreLayer.Entities.User_Entities;
using PreTrainee_Month2.CoreLayer.Product_Entities;

namespace PreTrainee_Month2.CoreLayer
{
    public class User
    {
        public int ID { get; set; } = 0;
        public string Name { get; set; } = String.Empty;
        public string EmailAddress { get; set; } = String.Empty;
        public string Role { get; set; } = "user";
        public string Password { get; set; }
        public bool HasVerifiedEmail { get; set; } = false;

        //для связи
        public List<Product> Products { get; set; } = [];

        public User(UserRegisterDTO userRegisterDTO)
        {
            Name = userRegisterDTO.Name;
            EmailAddress = userRegisterDTO.EmailAddress;
            Password = userRegisterDTO.Password;
        }
        public User()
        {

        }
    }
}
