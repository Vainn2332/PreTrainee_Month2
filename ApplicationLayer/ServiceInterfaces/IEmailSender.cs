namespace PreTrainee_Month2.ApplicationLayer.ServiceInterfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string receiverEmail,string senderEmail,string body);
    }
}
