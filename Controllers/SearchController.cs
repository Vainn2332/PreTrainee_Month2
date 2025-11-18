using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer.Product_Entities;

namespace PreTrainee_Month2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("filterByNameAscending")]
        [Authorize]
        public async Task<IEnumerable<Product>> FilterByNameAscendingAsync()
        {
            return await _searchService.FilterByNameAscendingAsync();
        }

        [HttpGet("filterByNameDescending")]
        [Authorize]
        public async Task<IEnumerable<Product>> FilterByNameDescendingAsync()
        {
            return await _searchService.FilterByNameDescendingAsync();
        }

        [HttpGet("filterByPriceAscending")]
        [Authorize]
        public async Task<IEnumerable<Product>> FilterByPriceAscendingAsync()
        {
            return await _searchService.FilterByPriceAscendingAsync();
        }

        [HttpGet("filterByPriceDescending")]
        [Authorize]
        public async Task<IEnumerable<Product>> FilterByPriceDescendingAsync()
        {
            return await _searchService.FilterByPriceDescendingAsync();
        }

        [HttpPost("searchByPrice={price}")]
        [Authorize]
        public async Task<IEnumerable<Product>> SearchByPriceAsync(decimal price)
        {
            if ((int)price<=0)
            {
                throw new ArgumentException("Некорректно введена цена!");
            }
            return await _searchService.SearchByPriceAsync(decimal.Round(price, 2));
        }

        [HttpPost("searchByName={name}")]
        [Authorize]
        public async Task<Product> SearchByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Некорректно введена цена!");
            }

            char[] charName = name.ToCharArray();
            char.ToUpper(charName[0]);
            string editedName = new string(charName);

            return await _searchService.SearchByNameAsync(editedName);
        }
    }
}
