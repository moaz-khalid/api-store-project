using api_course_project.Errors;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Dtos.Baskets;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;

namespace api_course_project.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            if(id is null) return BadRequest(new ApiErrorResponse(400,"invalid id"));

            var basket = await _basketRepository.GetBasketAsync(id);

            if (basket is null) basket = new CustomerBasket() { Id = id };

            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasketDto model)
        {


            var Basket = await _basketRepository.UpdateBasketAsync(_mapper.Map<CustomerBasket>(model));

            if (model is null) return BadRequest(new ApiErrorResponse(400));


            return Ok(Basket);
        }


        [HttpDelete]
        public async Task DeleteBasket(string id)
        {

             await _basketRepository.DeleteBasketAsync(id);
        }

    }
}
 