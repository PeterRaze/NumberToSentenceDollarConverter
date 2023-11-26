using Microsoft.AspNetCore.Mvc;
using NumberToCurrencyConverter.Model;
using NumberToCurrencyConverter.Server.Core;
using System.ComponentModel.DataAnnotations;

namespace NumberToCurrencyConverter.Controllers
{
    [ApiController]
    [Route("api/converter")]
    public class ConverterController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCurrencySentence([FromQuery, Required]string input)
        {
            try
            {
                Currency currency = new Currency()
                {
                    WordRepresentation = Converter.ConvertNumberToSentece(input)
                };

                return Ok(currency);
            }
            catch (Exception e)
            {
                var problemDetails = new ProblemDetails
                {
                    Detail = e.Message,
                    Instance = null,
                    Status = 422,
                    Title = "Input not supported",
                    Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/422",
                };

                return UnprocessableEntity(problemDetails);
            }
        }

    }
}
