using AutoMapper;
using Store.Core;
using Store.Core.Dtos.Products;
using Store.Core.Entities;
using Store.Core.Helper;
using Store.Core.Services.Contract;
using Store.Core.Specifications;
using Store.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
        }

        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpec)
        {
            var spec = new ProductSpecifications(productSpec);

            var products = await _UnitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec);
            var mappedProducts = _Mapper.Map<IEnumerable<ProductDto>>(products);

            var countSpec = new ProductWithCountSpecifications(productSpec);

            var count = await _UnitOfWork.Repository<Product, int>().GetCountAsync(countSpec);

            return new PaginationResponse<ProductDto>(productSpec.PageSize, productSpec.PageIndex, count, mappedProducts);
        }


        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await _UnitOfWork.Repository<Product, int>().GetWithSpecAsync(id, spec);
            return _Mapper.Map<ProductDto>(product);        
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
            return _Mapper.Map<IEnumerable<TypeBrandDto>>(await _UnitOfWork.Repository<ProductType, int>().GetAllAsync());
        }



        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            return _Mapper.Map<IEnumerable<TypeBrandDto>>(await _UnitOfWork.Repository<ProductBrand, int>().GetAllAsync());

        }


    }
}
