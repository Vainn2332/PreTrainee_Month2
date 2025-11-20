using PreTrainee_Month2.CoreLayer.Entities.User_Entities;

namespace PreTrainee_Month2.ApplicationLayer.ServiceInterfaces
{
    public interface IAuthService
    {
        public const int tokenExpirationTimeInMinutes = 90;
        public string GenerateJWT(int userId,string userEmail);
        public UserJWTInfo ParseJWT(string jwt);
        public string GetJWTFromHeader(HttpRequest request);
    }
}
