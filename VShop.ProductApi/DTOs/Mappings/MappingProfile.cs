using AutoMapper;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category,CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();

            //Logo após ir na classe Program e refrenciar quais assembles estão esses perfis de mapeamento


        }
    }
}
