using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services
{
    public class ProductService : IProductService
    {// Lembrar de configurar o serviço antes de incrementar os métodos
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndpoint = "/api/products";//vai formar com o localhost no appsettings no serviceApi
        private readonly JsonSerializerOptions _options;
        private ProductViewModel productVM; //aqui um product
        private IEnumerable<ProductViewModel> productsVM; // aqui uma lista de products

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; //aqui diz que não vou considerar na deserelização ou serilização caixa alta ou caixa baixa
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            var client = _clientFactory.CreateClient("ProductApi");

            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productsVM = await JsonSerializer
                        .DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return productsVM;
        }
        public async Task<ProductViewModel> FindProductById(int id)
        {
            var client = _clientFactory.CreateClient("ProductApi");

            using (var response = await client.GetAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productVM = await JsonSerializer
                        .DeserializeAsync<ProductViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return productVM;
        }
        public async Task<ProductViewModel> CreateProduct(ProductViewModel productVM)
        {
            var client = _clientFactory.CreateClient("ProductApi");

            StringContent content = new StringContent(JsonSerializer.Serialize(productVM),
                                    Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productVM = await JsonSerializer
                                .DeserializeAsync<ProductViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return productVM;

        }
        public async Task<ProductViewModel> UpdateProduct(ProductViewModel productVM)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            ProductViewModel productUpdated = new ProductViewModel();

            using (var response = await client.PutAsJsonAsync(apiEndpoint, productVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productVM = await JsonSerializer
                                .DeserializeAsync<ProductViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return productVM;
        }
        public async Task<bool> DeleteProductById(int id)
        {
            var client = _clientFactory.CreateClient("ProductApi");

            using (var response = await client.DeleteAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }
    }
}
