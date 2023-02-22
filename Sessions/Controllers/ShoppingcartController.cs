using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sessions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingcartController : ControllerBase
    {

        [HttpGet]
        public Product Get(string name, int price)
        {
            List<Product> products = new List<Product>();
            Product product = new Product() { Name = name.ToLower(), Price = price };
            products.Add(product);
            if (HttpContext.Session.GetObjectFromJson<IEnumerable<Product>>("ShoppingCart") != null)
            {
                products.AddRange(HttpContext.Session.GetObjectFromJson<IEnumerable<Product>>("ShoppingCart")!);
            }
            HttpContext.Session.SetObjectAsJson("ShoppingCart", products);
            return product;
        }
        [HttpGet]
        [Route("[Action]")]
        public IEnumerable<Product> GetFromSession()
        {
            if (HttpContext.Session.GetObjectFromJson<IEnumerable<Product>>("ShoppingCart") != null)
            {
                return HttpContext.Session.GetObjectFromJson<IEnumerable<Product>>("ShoppingCart")!;
            }
            else return Enumerable.Empty<Product>();
        }
    }
}
