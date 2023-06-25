using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index() //Vou consumir no meu serviço GetAllProduct()
        { //Aqui eu cliquei com o botão direito do mouse e adicionei uma exibição(Add View) dos produtos que é criado na pasta Views -> Products -> index.cshtml.
            var result = await _productService.GetAllProducts();
            //vai ter a lista dos produtos
            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> CreateProduct(int id) //cria o produto
        {
            ViewBag.CategoryId = new SelectList(await 
                _categoryService.GetAllCategories(), "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(productVM);
                
                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
            }
            return View(productVM);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id) //pego o id e mostro o resulta na minha view(result), a View UpdateProduct
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name"); //carregamento da lista de categorias, tipo um dropdown
            //depois da implementação do httpget e httpPost clica com o botão direito aqui e adiciona uma View
            var result = await _productService.FindProductById(id); //localizar o produto no meu serviço

            if (result is null) return View("Error");
           
            return View(result); //apresentação do produto em uma View
        }// esse método vai apresentar a página para atualizar os dados do produto, primieor vamos ter o método http get acima, que vai precisar obter o produto pelo id
        //Ápós realizar essa ações e clicar no botão Save, vamos acionar o método httpost abaixo para tualizar esse produto.

        [HttpPost]
        public async Task<IActionResult> UpdateProduct (ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.UpdateProduct(productVM); // se tudo estiver ok vou usar microserviço, e vou atualizar o produto no meu método do productservice


                if (result != null) return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }

        [HttpGet]
        public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)//vai apresentar os dados do produto selecionado
        {
            var result = await _productService.FindProductById(id);//depois da implemnetação do httpGet e post adiconarr view com botão direito

            if (result is null) return View("Error");

            return View(result);
        }

        [HttpPost(), ActionName("DeleteProduct")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _productService.DeleteProductById(id);

            if (!result) return View("Error");

            return RedirectToAction("Index");
        }

    }
}
