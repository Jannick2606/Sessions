using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sessions.Controllers
{
    /// <summary>
    /// Controller with 2 Get methods
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingcartController : ControllerBase
    {
        /// <summary>
        /// Method for adding a product to the session
        /// It adds a product to a list
        /// If the session is not null it adds the session items to the same list
        /// At the end it adds that list to the session
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <returns product/>
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
        /// <summary>
        /// Method for getting items from the session
        /// </summary>
        /// <returns IEnumerable list of products/>
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
