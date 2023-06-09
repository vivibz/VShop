using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {// Tem que registrar os serviços no Program.cs se não a controller não vai reconhecer os serviços
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService; 
        }
        [HttpGet] //Vou utilizar o ActionResult pois posso ter mais um tipo de retorno, posso retornar um objeto, um statusCode, isso trás felixibilidade de retrono tanto DTOs quanto statusCode 200 ou not found 
        public async Task <ActionResult<IEnumerable<CategoryDTO>>> Get() //estamos definindo primeiro o retorno das categorias
        { //Vou retornar oq? Um lista de categorias categoriesDto, o categoryService é atráves dele que acesso os métódos do repositório
            var categoriesDto = await _categoryService.GetCategories();
            if (categoriesDto == null)
            {
                return NotFound("Categories not found");
            }
            return Ok(categoriesDto);
        }
        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts() 
        { 
            var categoriesDto = await _categoryService.GetCategoriesProducts();
            if (categoriesDto == null)
            {
                return NotFound("Categories not found");
            }
            return Ok(categoriesDto);
        }
        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetByIdCategorie( int id)
        {
            var categoriesDto = await _categoryService.GetCategoriesById(id);
            if (categoriesDto == null)
            {
                return NotFound("Categories not found");
            }
            return Ok(categoriesDto);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto) //FromBody significa que estou passando no body do request os dados da categoria que estou incluindo
        {
            if (categoryDto == null)
            {
                return BadRequest("Invalid Data");
            }
            await _categoryService.AddCategory(categoryDto);
            return new CreatedAtRouteResult("GetCategory", new { id= categoryDto.CategoryId },
            categoryDto); //aqui me retonar o nome da categoria com o id da categoria que foi incluida
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
        {
            if(id != categoryDto.CategoryId)
                return BadRequest();

            if (categoryDto == null)
                return BadRequest();

            await _categoryService.UpdateCategory(categoryDto);
            return Ok(categoryDto);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var categoryDto = await _categoryService.GetCategoriesById(id);
            if (categoryDto == null)
                return NotFound("Category not found");

            await _categoryService.RemoveCategory(id);
            return Ok(categoryDto);
        }
    }
}
