using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sessions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteController : ControllerBase
    {
        [HttpDelete]
        public string Delete(string name)
        {
            if (HttpContext.Session.GetObjectFromJson<IEnumerable<Product>>("ShoppingCart") != null)
            {
                bool wasRemoved = false;
                Product productToRemove = new Product();
                List<Product> products = (List<Product>)HttpContext.Session.GetObjectFromJson<IEnumerable<Product>>("ShoppingCart")!;
                foreach (Product product in products)
                {
                    if(product.Name == name.ToLower()) productToRemove = product;
                    wasRemoved = true;
                }
                if (wasRemoved)
                {
                    products.Remove(productToRemove);
                    HttpContext.Session.SetObjectAsJson("ShoppingCart", products);
                    return $"{productToRemove.Name} was removed";
                }
                else return "Couldn't find product";
            }
            return "Session is empty";
        }


        [HttpDelete]
        [Route("[Action]")]
        public void DeleteAllItems()
        {
            if (HttpContext.Session.GetObjectFromJson<IEnumerable<Product>>("ShoppingCart") != null)
            {
                HttpContext.Session.Clear();
            }
        }
    }
}
