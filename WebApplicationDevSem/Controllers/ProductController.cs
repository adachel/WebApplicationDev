﻿using Microsoft.AspNetCore.Mvc;
using WebApplicationDevSem.Abstraction;
using WebApplicationDevSem.DB;
using WebApplicationDevSem.DTO;
using WebApplicationDevSem.Models;
using WebApplicationDevSem.Repo;

namespace WebApplicationDevSem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IPoductRepo _productRepo;

        public ProductController(IPoductRepo productRepo)
        {
            _productRepo = productRepo;
        }


        [HttpPost(template: "addproduct")]
        public ActionResult AddProduct(ProductViewModel productViewModel)
        {
            try
            {
                _productRepo.AddProduct(productViewModel);
                return Ok();
            }
            catch
            {
                return StatusCode(409);
                //return StatusCode(500);
            }
        }


        [HttpGet(template: "getproducts")]
        public ActionResult<IEnumerable<ProductViewModel>> GetProducts()
        {
            return Ok(_productRepo.GetProduts());
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
