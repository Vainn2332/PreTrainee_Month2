namespace PreTrainee_Month2.ApplicationLayer.ServiceInterfaces
{
    public interface IAuthService
    {
        public const int tokenExpirationTimeInMinutes = 90;
        public string GenerateJWT(int userId,string userEmail);
    }
}
