using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using EmailClientAPI.Models;
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

        [HttpPost]
        public async Task<IActionResult> SendEmailAsync([FromBody] Email email)
        {
            try
            {
                if(email == null)
                {
                    return BadRequest("Email structure was expected in request body");
                }
                bool connected = _emailClient.connect();
                if (connected)
                {
                    bool emailSent = await _emailClient.sendAsync(email.from, email.to, email.subject, email.body);
                    _emailClient.disconnect();

                    if (emailSent)
                    {
                        return Ok("Email successfully sent");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                        "Email failed to send");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Unable to connect to SMTP server");
                }
            }catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error occured while attempting to send email");
            }
        }
    }
}
