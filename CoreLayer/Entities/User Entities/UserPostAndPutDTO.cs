using System.ComponentModel.DataAnnotations;

namespace PreTrainee_Month2.CoreLayer.Entities.User_Entities
{
    public class UserPostAndPutDTO
    {
        [Required(ErrorMessage = "Не указано имя пользователя!")]
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage = "Не указан адрес")]
        [EmailAddress]
        public string EmailAddress { get; set; } = String.Empty;

        [Required]
        [RegularExpression("^(user|admin)$", ErrorMessage = "Роль может быть только admin и user")]
        public string Role { get; set; } = "user";
    }
}
