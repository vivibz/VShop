using System.ComponentModel.DataAnnotations;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")] //Aqui estou dizendo que o nome deve ser fornecido
        [MinLength(3)]  //quantidade minimo de caracteres é 3 e o máximo é 100
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage ="the Price is Required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="This Description is Required")]
        [MinLength (5)]
        [MaxLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage ="The Stock is Required")]
        [Range(1,9999)]
        public long Stock { get; set; }
        public string? ImageURL { get; set; }

        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
