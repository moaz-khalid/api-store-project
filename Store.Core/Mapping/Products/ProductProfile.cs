using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Core.Mapping.Products
{
    public class ProductProfile: Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Entities.Product, Dtos.Products.ProductDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.Name))
                //.ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => $"{configuration["BASEURL"]}{src.PictureUrl}"));
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(new PictureUrlResolver(configuration)));     //another way to map the picture url

            CreateMap<Entities.ProductType, Dtos.Products.TypeBrandDto>();
            CreateMap<Entities.ProductBrand, Dtos.Products.TypeBrandDto>();
        } 
    }
}
