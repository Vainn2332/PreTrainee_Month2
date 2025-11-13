using PreTrainee_Month2.CoreLayer.Product_Entities;
using System.ComponentModel.DataAnnotations;

namespace PreTrainee_Month2.CoreLayer.User_Entities
{
    public class UserDTO
    {
        [Required(ErrorMessage ="Не указано имя пользователя!")]
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage ="Не указан адрес")]
        [EmailAddress]
        public string EmailAddress { get; set; } = String.Empty;
       
        [Required]
        [RegularExpression("^(user|admin)$",ErrorMessage ="Роль может быть только admin и user")]
        public string Role { get; set; } = string.Empty;
        
   
        public List<ProductDTO> Products { get; set; } = [];
    }
}
