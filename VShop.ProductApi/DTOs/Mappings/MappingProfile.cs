using AutoMapper;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category,CategoryDTO>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<Product, ProductDTO>()
                .ForMember(x =>x.CategoryName, opt => opt.MapFrom(src => src.Category.Name)); //Tenho que modificar, pois, quando eu for mapear de product para productDTO tenho que atribuir uma valor a CategoryName
            //estou obtendo o CategoryName a partir da Category.Name, em seguida vou ter que fazer um ajuste no ProductRepository. Pq quando estou obtendo só os produtos no GetAll só está vindo os produtos, tenho que obter também os valores das categorias e também quando eu for obter um proudto por ID
            //Logo após, ir na classe Program e refrenciar quais assembles estão esses perfis de mapeamento


        }
    }
}
