using ASPDepLec2.Util;
using Microsoft.AspNetCore.Mvc;

namespace ASPDepLec2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        private readonly IFibonacci _fibonacci;

        public MathController(IFibonacci fibonacci)
        {
            _fibonacci = fibonacci;
        }

        [HttpGet(template: "fibonacci")]
        public ActionResult<int> Fibonacci(int number)
        {
            return Ok(_fibonacci.CalculateFibonacci(number));
        }



    }
}
