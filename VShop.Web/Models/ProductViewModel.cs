
using System.ComponentModel.DataAnnotations;

namespace VShop.Web.Models
{
    public class ProductViewModel
    {
            public int Id { get; set; }
            [Required]
            public string? Name { get; set; }
            [Required]
            public decimal Price { get; set; }
            public string? Description { get; set; }
            [Required]
            public long Stock { get; set; }
            [Required]
            public string? ImageURL { get; set; }
            public string? CategoryName { get; set; } //não vai precisar mais da propriedade category, mas sim exibir o nome da categoria.Como eu introduzi o CategoryName onde não tem no ProductDTO da minha api, vou precisar fazer uma ajuste na api, em productDTO terei que adicionar também
            [Display(Name="Categorias")]
            public int CategoryId { get; set; }
      
    }
}
