using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var productDto = await _productService.GetProducts();
            if (productDto == null)
                return NotFound("Products not Found");

            return Ok(productDto);
        }
        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var produtoDto = await _productService.GetProductById(id);
            if (produtoDto == null)
                return NotFound("Product not found");

            return Ok(produtoDto);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDto)
        {
            if (productDto == null)
                return BadRequest("Data Invalid");

            await _productService.AddProduct(productDto);

            return new CreatedAtRouteResult("GetProduct", new { id = productDto.Id }, productDto);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductDTO productDto)
        {
            if (productDto == null) 
                return BadRequest("Data invalid");

            await _productService.UpdateProduct(productDto); //verifica se os dados são validos e faz a atualização do produto

            return Ok(productDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var productDto = await _productService.GetProductById(id);

            if (productDto == null)
                return NotFound("Product not found");

            await _productService.RemoveProduct(id);
            return Ok(productDto);
        }
    }
}
