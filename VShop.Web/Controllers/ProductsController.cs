using Microsoft.AspNetCore.Mvc;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index() //Vou consumir no meu serviço GetAllProduct()
        { //Aqui eu cliquei com o botão direito do mouse e adicionei uma exibição(Add View) dos produtos que é criado na pasta Views -> Products -> index.cshtml.
            var result = await _productService.GetAllProducts();
            //vai ter a lista dos produtos
            if (result is null)
                return View("Error");

            return View();
        }
    }
}
