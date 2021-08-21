using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RateSetter.Domain;
using RateSetter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateSetter.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {        
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
