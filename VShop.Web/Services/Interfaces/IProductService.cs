using VShop.Web.Models;

namespace VShop.Web.Services.Interfaces
{
    public interface IProductService
    {//através desses contratos eu consumo o microsserviço
        Task<IEnumerable<ProductViewModel>> GetAllProducts();
        Task<ProductViewModel> FindProductById(int id);
        Task <ProductViewModel> CreateProduct(ProductViewModel productVM);
        Task <ProductViewModel> UpdateProduct(ProductViewModel productVM);
        Task<bool> DeleteProductById(int id);
    }
}
