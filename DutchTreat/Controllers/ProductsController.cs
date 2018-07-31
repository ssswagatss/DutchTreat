using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    //[Produces("application/json")]
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {

        private readonly IDutchRepository _repository;
        public readonly ILogger<ProductsController> _logger;

        public ProductsController(IDutchRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"An Error occurred {ex}");
                return BadRequest("Not Found");
            }
        }
    }
}