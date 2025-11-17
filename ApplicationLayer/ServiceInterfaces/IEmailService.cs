namespace PreTrainee_Month2.ApplicationLayer.ServiceInterfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string receiverEmail, string subject, string body);
        public Task SendConfirmRegistrationEmailAsync(string receiverEmail, string confirmationLink);
        public Task SendResetPasswordEmailAsync(string receiverEmail, string confirmationLink);
        Task SendUserActivationEmailAsync(string receiverEmail, string confirmationLink);
        
    }
}
