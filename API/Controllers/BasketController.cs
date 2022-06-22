using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepository repository;

        public BasketController(IBasketRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string basketId){
          var basket = await repository.GetBasketAsync(basketId);
          return Ok(basket ?? new CustomerBasket(basketId));
        }


        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasket basket){
          var Updatedbasket = await repository.CreatOrUpdateBasketAsync(basket);
          return Ok(Updatedbasket);
        }

         [HttpDelete]
        public async Task Delete(string basketId){
           await repository.DeleteBasketAsync(basketId);
        }


    }
}