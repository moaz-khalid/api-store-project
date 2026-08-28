using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Core.Dtos.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int? BrandId { get; set; }   // Foreign key to ProductBrand

        public string BrandName { get; set; }

        public int? TypeId { get; set; }  // Foreign key to ProductType

        public string TypeName { get; set; }

    }
}
