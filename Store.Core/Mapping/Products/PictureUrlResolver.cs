using AutoMapper;
using AutoMapper.Execution;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Core.Mapping.Products
{
    public class PictureUrlResolver: IValueResolver<Entities.Product, Dtos.Products.ProductDto, string>
    {
        private readonly IConfiguration _configuration;
        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Entities.Product source, Dtos.Products.ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["BASEURL"]}{source.PictureUrl}";
            }
            return string.Empty;
        }
    
    }
}
