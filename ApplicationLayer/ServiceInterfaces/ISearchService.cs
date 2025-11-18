using Microsoft.EntityFrameworkCore.Storage;
using PreTrainee_Month2.CoreLayer.Product_Entities;

namespace PreTrainee_Month2.ApplicationLayer.ServiceInterfaces
{
    public interface ISearchService
    {
        public Task<Product> SearchByNameAsync(string name);
        public Task<Product> SearchByPriceAsync(decimal price);
        public Task<Product> SearchByDateOfCreationAsync(DateTime date);
        public Task<IEnumerable<Product>> FilterByDateOfCreationAsync();
        public Task<IEnumerable<Product>> FilterByDateOfCreationPriceAsync();
        public Task<IEnumerable<Product>> FilterByNameAsync();

    }
}
