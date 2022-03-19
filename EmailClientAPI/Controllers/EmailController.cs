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
        public async Task<IActionResult> GetAsync()
        {
            _emailClient.connect();
            bool emailSent = await _emailClient.sendAsync("jaredscruggs@outlook.com", "jdoggyx14@hotmail.com", "testing", "<p>test</p>");
            _emailClient.disconnect();

            if (emailSent)
            {
                return Ok("Email successfully sent");
            }
            else
            {
                return BadRequest("Email was unsuccessful");
            } 
        }
    }
}
