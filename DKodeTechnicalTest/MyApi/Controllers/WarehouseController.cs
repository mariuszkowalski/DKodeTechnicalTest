using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApi.Helpers;
using MyApi.Helpers.Interfaces;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly ILogger<WarehouseController> _logger;
        private readonly IRequestProcessor _requestProcessor;
        public WarehouseController(ILogger<WarehouseController> logger, IRequestProcessor requestProcessor)
        {
            _logger = logger;
            _requestProcessor = requestProcessor;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            _requestProcessor.GetAllData();

            return Ok();
        }

        [HttpGet]
        [Route("GetBySku")]
        public IActionResult Get(string sku)
        {
            return Ok();
        }
    }
}
