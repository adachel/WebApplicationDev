using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrudController : ControllerBase
    {
        static Dictionary<string, string> data = new Dictionary<string, string>();

        [HttpPost(template: "post")]
        public ActionResult Post(string key, string value) // Post предназначен для добавления данных
        {
            try
            {
                data.Add(key, value);
                return Ok();
            }
            catch
            {
                return StatusCode(409); // конфликт
            }
        }

        [HttpPost(template: "get")]
        public ActionResult Get(string key) // Get предназначен для получения данных
        {
            if (data.ContainsKey(key)) // проверяем, содержится ли ключ
            {
                return Ok(data[key]);
            }
            else 
            {
                //return StatusCode(404);
                return NotFound(); // можно так
            }
        }

        [HttpPost(template: "put")]
        public ActionResult Put(string key, string value) // Put - обновление данных, если элем нет, то создает
        {
            if (data.ContainsKey(key)) // проверяем, содержится ли ключ
            {
                data[key] = value;
            }
            else
            {
                data.Add(key, value);
            }
            return Ok();
        }

        [HttpPost(template: "patch")]
        public ActionResult Patch(string key, string value) // Patch - обновление данных, если элем нет, то ошибка
        {
            if (data.ContainsKey(key)) // проверяем, содержится ли ключ
            {
                data[key] = value;
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost(template: "delete")]
        public ActionResult Delete(string key) // Delete - удаление данных, если они есть, или ошибка
        {
            if (data.ContainsKey(key)) // проверяем, содержится ли ключ
            {
                data.Remove(key);
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
