using VShop.ProductApi.DTOs;

namespace VShop.ProductApi.Services
{
    public interface ICategoryService //definição do contrato para implementar o serviço de categoria
    { //serviço sempre vai retornar DTOs
        Task<IEnumerable<CategoryDTO>> GetCategories(); //retorna a lista e categorias
        Task<IEnumerable<CategoryDTO>> GetCategoriesProducts();//retorna a lista e categorias com os produtos
        Task<CategoryDTO> GetCategoriesById(int id);
        Task AddCategory(CategoryDTO categoryDto);
        Task UpdateCategory(CategoryDTO categoryDto);
        Task RemoveCategory(int id);


    }
}
