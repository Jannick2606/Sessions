using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sessions.Controllers
{
    /// <summary>
    /// Controller with 2 delete methods to delete items from the session
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteController : ControllerBase
    {
        /// <summary>
        /// Removes a single item from a session
        /// First it checks if the session is null
        /// If session is not null it creates a list of the products from the session
        /// If the session is not empty it iterates through it and tries to find the product with the name from the parameter
        /// It then removes that item from the list and adds that list to the session, overwriting the previous list
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete]
        public string Delete(string name)
        {
            bool wasRemoved = false;
            if (HttpContext.Session.GetObjectFromJson<IEnumerable<Product>>("ShoppingCart") != null)
            {
                
                Product productToRemove = new Product();
                List<Product> products = (List<Product>)HttpContext.Session.GetObjectFromJson<IEnumerable<Product>>("ShoppingCart")!;
                if (products.Count > 0)
                {
                    foreach (Product product in products)
                    {
                        if (product.Name == name.ToLower())
                        {
                            productToRemove = product;
                            wasRemoved = true;
                        }
                    }
                    if (wasRemoved)
                    {
                        products.Remove(productToRemove);
                        HttpContext.Session.SetObjectAsJson("ShoppingCart", products);
                        return $"{productToRemove.Name} was removed";
                    }
                    else return "Couldn't find product";
                }
            }
            return "ShoppingCart is empty";
        }

        /// <summary>
        /// Deletes all items from a session
        /// </summary>
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
