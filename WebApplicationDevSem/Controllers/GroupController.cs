using Microsoft.AspNetCore.Mvc;
using WebApplicationDevSem.Abstraction;
using WebApplicationDevSem.DB;
using WebApplicationDevSem.DTO;
using WebApplicationDevSem.Models;
using WebApplicationDevSem.Repo;

namespace WebApplicationDevSem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IProductGroupRepo _groupRepo = new ProductGroupRepo();

        //public GroupController(IProductGroupRepo productGroupRepo)
        //{
        //    _groupRepo = productGroupRepo;
        //}


        [HttpPost(template: "addgroup")]
        public ActionResult AddGroup(/*string name, string description = ""*/ ProductGroupViewModel productGroupViewModel)
        {
            //try
            //{
            //    using (var ctx = new ProductContext())
            //    {
            //        if (ctx.ProductGroup.Count(x => x.Name.ToLower() == name.ToLower()) > 0)
            //        {
            //            return StatusCode(409);
            //        }
            //        else
            //        {
            //            ctx.ProductGroup.Add(new ProductGroup { Name = name, Description = description });
            //            ctx.SaveChanges();
            //        }
            //    }

            //    return Ok();
            //}
            //catch
            //{
            //    return StatusCode(500);
            //}




            try
            {
                _groupRepo.AddProductGroup(productGroupViewModel);
                return Ok();
            }
            catch
            {
                return StatusCode(409);
            }
        }

        //[HttpGet(template: "getgroups")]
        //public ActionResult<IEnumerable<ProductGroup>> GetGroups()
        //{
        //    try
        //    {
        //        using (var ctx = new ProductContext())
        //        {
        //            var list = ctx.ProductGroup.Select(x => new ProductGroup { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
        //            return list;
        //        }
        //    }
        //    catch
        //    {
        //        return StatusCode(500);
        //    }
        //}

        //[HttpDelete(template: "deletegroup")]
        //public ActionResult DeleteGroup(int id)
        //{
        //    try
        //    {
        //        using (var ctx = new ProductContext())
        //        {
        //            if (ctx.ProductGroup.Count(x => x.Id == id) > 0)
        //            {
        //                var x = ctx.ProductGroup.FirstOrDefault(x => x.Id == id);
        //                ctx.ProductGroup.Remove(x);
        //                ctx.SaveChanges();
        //            }
        //            else
        //            {
        //                return StatusCode(404);
        //            }
        //        }

        //        return Ok();
        //    }
        //    catch
        //    {
        //        return StatusCode(500);
        //    }
        //}
    }
}
