using Microsoft.AspNetCore.Mvc;

namespace PreTrainee_Month2.ApplicationLayer.ServiceInterfaces
{
    public interface IAdminService
    {
        public Task DeactivateUserAsync(int id);
        public Task ActivateUserAsync(int id);
    }
}
