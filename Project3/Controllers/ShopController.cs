using Flights.Domain.Entities;
using Flights.ReadModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Data;
using Project3.ReadModels;

namespace Flights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly Entities _entities;

        public ShopController(Entities entities)
        {
            _entities = entities;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public IEnumerable<Product> Search()
        {
            var productList = _entities.Products;

            var productTypeList = _entities.ProductTypes.ToDictionary(p => p.Id);

            foreach (var product in productList)
            {
                product.Type = productTypeList[product.ProductTypeId];
            }

            return productList;
        }
    }
}
