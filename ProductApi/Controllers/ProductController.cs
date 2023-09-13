using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ProductApi.Dtos;

namespace ProductApi.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private static readonly List<ProductDto> products = new ()
        {
            new ProductDto (Guid.NewGuid(),"Termék1",3500,DateTimeOffset.UtcNow),
            new ProductDto (Guid.NewGuid(),"Termék2",2500,DateTimeOffset.UtcNow),
            new ProductDto (Guid.NewGuid(),"Termék3",1500,DateTimeOffset.UtcNow),
        };

        [HttpGet]

        public IEnumerable<ProductDto> GetAll()
        {
            return products;
        }

        [HttpGet("{id}")]
        public ProductDto GetById(Guid id)
        {
            var product = products.Where(x => x.Id == id).FirstOrDefault();

            return product;
        }

        [HttpPost]

        public ProductDto PostProduct(CreateProductDto createProduct)
        {
            var product = new ProductDto(Guid.NewGuid(),
                createProduct.ProductName,
                createProduct.ProductPrice,
                DateTimeOffset.UtcNow);

            products.Add(product);

            return product; //CreatedAtAction(nameof(GetAll), new ProductDto());
        }

        [HttpPut ("{id}")]

        public ProductDto PullProduct (Guid id, UpdateProductDto updateProduct)
        {
            var existingProduct = products.Where(x => x.Id != id).FirstOrDefault();

            var product = existingProduct with
            {
                ProductName = updateProduct.ProductName,
                ProductPrice = updateProduct.ProductPrice
            };

            var index = products.FindIndex(x => x.Id == id);

            products[index] = product;

            return products[index];
        }

        [HttpDelete("{id}")]

        public string DeleteProduct(Guid id)
        {
            var choosenProduct = products.FindIndex((x) => x.Id == id);
            products.RemoveAt(choosenProduct);

            return "Az elem törölve!";
        }
    }
}
