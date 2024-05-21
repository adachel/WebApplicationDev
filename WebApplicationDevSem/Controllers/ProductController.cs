using Microsoft.AspNetCore.Mvc;
using WebApplicationDevSem.DB;
using WebApplicationDevSem.Models;

namespace WebApplicationDevSem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpPost(template: "addproduct")]
        public ActionResult AddProduct(string name, string description, float price, int productGroupId)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (ctx.Products.Count(x => x.Name.ToLower() == name.ToLower()) > 0)
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        if (ctx.ProductGroup.Count(x => x.Id == productGroupId) > 0)
                        {
                            ctx.Products.Add(new Product 
                            { 
                                Name = name, 
                                Description = description,
                                Price = price,
                                ProductGroupId = productGroupId 
                            });
                            ctx.SaveChanges();
                        }
                        else 
                        {
                            return StatusCode(428);
                        } 
                    }
                }

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpGet(template: "getproducts")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    var list = ctx.Products.Select(x => new Product { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
                    return list;
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch(template: "patchproduct")]
        public ActionResult PatchProduct(int id, float price)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (ctx.Products.Count(x => x.Id == id) > 0)
                    {
                        var x = ctx.Products.FirstOrDefault(x => x.Id == id);
                        x.Price = price;
                        ctx.SaveChanges();
                    }
                    else
                    {
                        return StatusCode(404);
                    }
                }

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

            [HttpDelete(template: "deleteproduct")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (ctx.Products.Count(x => x.Id == id) > 0)
                    {
                        var x = ctx.Products.FirstOrDefault(x => x.Id == id);
                        ctx.Products.Remove(x);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        return StatusCode(404);
                    }
                }

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
