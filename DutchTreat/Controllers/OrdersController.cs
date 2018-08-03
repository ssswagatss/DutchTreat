using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController:Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_repository.GetAllOrders()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An Error occurred {ex}");
                return BadRequest("Not Found");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);
                if (order != null)
                    return Ok(_mapper.Map<Order,OrderViewModel>(order));
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An Error occurred {ex}");
                return BadRequest("Not Found");
            }
        }

        [HttpPost]
        public ActionResult Post(Order model)
        {
            //Add to database
            return Ok(model);
        }
    }
}
