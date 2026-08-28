using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Core.Specifications.Products
{
    public class ProductSpecifications:BaseSpecifications<Entities.Product, int>
    {
        public ProductSpecifications(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }

        public ProductSpecifications(ProductSpecParams productSpec) : base(
            p =>
            (!string.IsNullOrEmpty(productSpec.Search) && p.Name.ToLower().Contains(productSpec.Search))
            &&
            (!productSpec.BrandId.HasValue || p.BrandId == productSpec.BrandId) 
            &&
            (!productSpec.TypeId.HasValue || p.TypeId == productSpec.TypeId))
        {
            if(!string.IsNullOrEmpty(productSpec.Sort))
            {



                switch (productSpec.Sort)
                {
                        case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                        case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
            

            ApplyIncludes();


            //page size and index
            ApplyPagination((productSpec.PageIndex - 1) * productSpec.PageSize, productSpec.PageSize);       
        }

        private void ApplyIncludes()
        {
            Include.Add(p => p.Brand);
            Include.Add(p => p.Type);
        }

    }
}
