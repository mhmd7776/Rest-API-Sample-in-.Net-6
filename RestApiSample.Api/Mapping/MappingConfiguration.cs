using AutoMapper;
using RestApiSample.Api.Data.DTOs;
using RestApiSample.Api.Data.Models;

namespace RestApiSample.Api.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
        }
    }
}
