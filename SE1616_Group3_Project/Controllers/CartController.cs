using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SE1616_Group3_Project.Models;

namespace SE1616_Group3_Project.Controllers
{
    public class CartController : Controller
    {
        private readonly BakingIngredientsContext _context;
        public CartController(BakingIngredientsContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
           
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "";
            if(userEmail == "")
            {
                return RedirectToAction("Login", "User");
            }
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            var cart = _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.UserEmailNavigation)
                .Where(c => c.UserEmail == userEmail);
            var payment = _context.PaymentMethods;
            ViewData["payment"] = payment;
            ViewData["CartItems"] = cart;
            ViewBag.user = user;
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var cart = await _context.CartItems.FirstAsync(c => c.ProductId == id);
            _context.CartItems.Remove(cart);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            string message = "";
            var cart =  _context.CartItems.First(c => c.ProductId == id);
            var product =  _context.Products.First(p => p.Id == id);
            if (quantity < product.Quantity)
            {
                cart.Quantity = quantity;
                _context.CartItems.Update(cart);
                _context.SaveChanges();
            }
            else
            {
                 message = "Out of stock";
            }

            
            return Json(new {Message = message});
        }
        
         /*public JsonResult UpdateQuantity(int id, int quantity)
        {
            var cart =  _context.CartItems.First(c => c.ProductId == id);
            var product =  _context.Products.First(p => p.Id == id);
            if (quantity < product.Quantity)
            {
                cart.Quantity = quantity;
                _context.CartItems.Update(cart);
                _context.SaveChanges();
            }
            else
            {
                ViewBag.msg = "Out of stock!";
            }
            return Json("Response from update");
        }*/
        public IActionResult Add(int id)
        {
            string email = HttpContext.Session.GetString("userEmail")??"";
            if (email == "")
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                CartItem cartItem = new CartItem();
                cartItem.ProductId = id;
                cartItem.AddedDate = DateTime.Now;
                cartItem.Quantity = 1;
                cartItem.UserEmail = email;
                
                if (!exist(id))
                {
                   
                    _context.CartItems.Add(cartItem);

                }
                else
                {
                    var cart = cartExist(cartItem.ProductId);
                    cart.Quantity += 1;
                    _context.CartItems.Update(cart);
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Product");
            }

        }
        private CartItem cartExist(int productId)
        {
            return _context.CartItems.First(c => c.ProductId == productId);
        }

        private Boolean exist(int productId)
        {
            return _context.CartItems.FirstOrDefault(c => c.ProductId == productId) != null ? true : false;
        }

    }
}
