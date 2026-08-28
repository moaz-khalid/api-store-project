using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Core.Entities
{
    public class Product : BaseEntity<int>
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int? BrandId { get; set; }   // Foreign key to ProductBrand

        public ProductBrand Brand { get; set; }

        public int? TypeId { get; set; }  // Foreign key to ProductType

        public ProductType Type { get; set; }

    }
}
