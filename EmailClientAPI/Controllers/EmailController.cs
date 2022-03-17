using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailLibrary;

namespace EmailClientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly ILogger<EmailController> _logger;
        private readonly IEmailClient _emailClient;
        public EmailController(IEmailClient emailClient, ILogger<EmailController> logger)
        {
            _logger = logger;
            _emailClient = emailClient;
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            _emailClient.connect();
            _logger.LogInformation("entered get method");
            return Ok("Completed");
        }


    }
}
