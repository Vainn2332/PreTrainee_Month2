using Microsoft.EntityFrameworkCore.Storage;
using PreTrainee_Month2.CoreLayer.Product_Entities;

namespace PreTrainee_Month2.ApplicationLayer.ServiceInterfaces
{
    public interface ISearchService
    {
        public Task<Product?> SearchByNameAsync(string name);
        public Task<IEnumerable<Product>> SearchByPriceAsync(decimal price);
        public Task<IEnumerable<Product>> FilterByPriceAscendingAsync();
        public Task<IEnumerable<Product>> FilterByPriceDescendingAsync();
        public Task<IEnumerable<Product>> FilterByNameAscendingAsync();
        public Task<IEnumerable<Product>> FilterByNameDescendingAsync();

    }
}
