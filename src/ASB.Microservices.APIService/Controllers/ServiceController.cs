using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ASB.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ASB.Microservices.APIService.Controllers
{
    [ApiController]
    [Route("api/service")]
    public class ServiceController : ControllerBase
    {
        private readonly IMessageSource _messageSource;

        public ServiceController(IMessageSource messageSource)
        {
            _messageSource = messageSource;
        }
        
        [HttpPost]
        public async Task<ActionResult> Post()
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var requestBody = await reader.ReadToEndAsync();
            var token = new CancellationTokenSource().Token;
            var result = await _messageSource.Post(requestBody, token);

            if (result.Status == CommandExecutionStatus.Success)
            {
                Console.WriteLine("Details found, sending response.");
                return Ok(result.Object);
            }

            Console.WriteLine($"Error occured: {result.ErrorMessage}, exception: {result.ErrorReason}");
            return BadRequest(new
            {
                result.Status,
                result.ErrorMessage,
            });
        }
    }
}