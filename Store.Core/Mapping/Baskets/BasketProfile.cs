using Store.Core.Dtos.Baskets;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Core.Mapping.Baskets
{
    public class BasketProfile : AutoMapper.Profile
    {
        public BasketProfile()
        {

            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();

        }


    }
}
