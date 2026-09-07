using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Core.Entities
{
    public class CustomerBasket
    {
        public String Id { get; set; }

        public List<BasketItem> Items { get; set; }


    }
}
