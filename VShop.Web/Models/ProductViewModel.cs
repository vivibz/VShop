
namespace VShop.Web.Models
{
    public class ProductViewModel
    {
            public int Id { get; set; }
            public string? Name { get; set; }
            public decimal Price { get; set; }
            public string? Description { get; set; }
            public long Stock { get; set; }
            public string? ImageURL { get; set; }
            public string? CategoryName { get; set; } //não vai precisar mais da propriedade category, mas sim exibir o nome da categoria.Como eu introduzi o CategoryName onde não tem no ProductDTO da minha api, vou precisar fazer uma ajuste na api, em productDTO terei que adicionar também
            public int CategoryId { get; set; }
      
    }
}
