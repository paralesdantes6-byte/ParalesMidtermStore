using Microsoft.AspNetCore.Mvc;
using ParalesMidtermStore.Data;
using ParalesMidtermStore.Models;

namespace ParalesMidtermStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        //  all items cart
        public IActionResult Index()
        {
            var cartItems = _db.CartItems.ToList();
            return View(cartItems);
        }

        // Add product 
        public IActionResult AddToCart(int id)
        {
            var product = _db.Products.Find(id);

            if (product != null)
            {
                var existingItem = _db.CartItems
                    .FirstOrDefault(c => c.ProductId == product.Id);

                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    var cartItem = new CartItem
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Price = product.Price,
                        Quantity = 1
                    };

                    _db.CartItems.Add(cartItem);
                }

                _db.SaveChanges();
            }

            return RedirectToAction("Index", "Products");
        }

        // Update quantity
        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var item = _db.CartItems.Find(id);

            if (item != null)
            {
                if (quantity > 0)
                {
                    item.Quantity = quantity;
                    _db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        // Remove item 
        public IActionResult Remove(int id)
        {
            var item = _db.CartItems.Find(id);

            if (item != null)
            {
                _db.CartItems.Remove(item);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}