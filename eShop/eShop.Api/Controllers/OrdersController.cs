using System;
using eShop.Domain;
using eShop.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Api.Controllers
{
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly IRepository<Order> repository;

        public OrdersController(IRepository<Order> repository)
        {
            this.repository = repository;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(repository.GetById(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Order order)
        {
            repository.Create(order);
            return Ok();
        }
    }
}
