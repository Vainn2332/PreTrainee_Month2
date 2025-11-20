using System.ComponentModel.DataAnnotations;

namespace PreTrainee_Month2.CoreLayer.Entities.User_Entities
{
    public class UserJWTInfo
    {
        public string EmailAddress { get; set; }

        public int UserId { get; set; }
    }
}
