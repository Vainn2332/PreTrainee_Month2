using System.ComponentModel.DataAnnotations;

namespace PreTrainee_Month2.CoreLayer.Entities.User_Entities
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Не указано имя пользователя!")]
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage = "Не указан адрес")]
        [EmailAddress]
        public string EmailAddress { get; set; } = String.Empty;

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
    }
}
