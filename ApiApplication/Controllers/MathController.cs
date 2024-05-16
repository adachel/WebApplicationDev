using ApiApplication.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [LogActionFilter] // закоментил, когда добавил метод с Task
    public class MathController : ControllerBase
    {

        //[HttpGet(Name = "Square")]
        //public int Square(int x)
        //{
        //    return x * x;
        //}

        //[HttpGet(Name = "Divide")] // в таком виде ошибка, два метода по одному адресу
        //public int Divide(int x)
        //{
        //    return x / x;
        //}


        [HttpGet(template: "Square")]
        public int Square(int x)
        {
            return x * x;
        }

        //[HttpGet(template: "Divide")] // так работает
        //public int Divide(int x, int y)
        //{
        //    return x / y;
        //}

        [HttpGet(template: "Divide")] // так работает
        public ActionResult<int> Divide(int x, int y)
        {
            try
            {
                var z = x / y;
                return Ok(z);
            }
            catch (DivideByZeroException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); // можно задать любой статусный код
            }
        }

        [HttpGet(template: "Calc")] // так работает
        public async Task<ActionResult<int>> Calc(int x, int y)
        {
            var t1 = Task.Run<int>(() =>
            { 
                Task.Delay(100).Wait();
                return 10;
            });
            var t2 = Task.Run<int>(() =>
            {
                Task.Delay(100).Wait();
                return 20;
            });

            var x1 = await t1;
            var x2 = await t2;

            return Ok(x1 + x2);
        }


    }
}
