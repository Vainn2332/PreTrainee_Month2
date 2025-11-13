using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PreTrainee_Month2.CoreLayer.Entities.Static_Entities
{
    public static class AuthOptions
    {
        public const string ISSUER = "myServer";
        public const string AUDIENCE = "myClient";
        private const string secretKey="superSecretKeyThatOnlyIKnow";
        public static SymmetricSecurityKey GetSymmetricSecurityKey()=>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
           
        
         
        
    }
}
