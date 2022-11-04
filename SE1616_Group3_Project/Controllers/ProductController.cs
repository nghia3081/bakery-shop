using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE1616_Group3_Project.Models;

namespace SE1616_Group3_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly BakingIngredientsContext _context;
        public ProductController(BakingIngredientsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories;
                
            var product = _context.Products;
            ViewData["product"] = product.ToList();
            ViewData["categories"] = categories.ToList();

            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            
            var product = await _context.Products
                
                .FirstOrDefaultAsync(p => p.Id == id);
                
           
            

            return View(product);
        }
        public async Task<IActionResult> Admin()
        {
            string userEmail = HttpContext.Session.GetString("userEmail")?? "";
            if(userEmail == "")
            {
                return RedirectToAction("Login", "User");
            }
            var user = await _context.Users.FirstAsync(u => u.Email == userEmail);
            if (user.RoleId == 3)
            {
                return RedirectToAction("Index");
            }
            else
            {

                var categories = _context.Categories.ToList();

                var product = _context.Products.ToList();
                ViewData["product"] = product;
                ViewData["categories"] = categories;

                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([Bind("Id,Name")] Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Admin");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory([Bind("Id,Name")] Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Admin");
        }
        
        public async Task<IActionResult> DeleteCategory(int id)
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "";
            if (userEmail == "")
            {
                return RedirectToAction("Login", "User");
            }
            var user = await _context.Users.FirstAsync(u => u.Email == userEmail);
            if (user.RoleId == 1)
            {
                var category = await _context.Categories
                .Include(c => c.Products)
                .FirstAsync(u => u.Id == id);
                foreach (Product p in category.Products)
                {
                    _context.Products.Remove(p);
                }
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return RedirectToAction("Admin");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file,[Bind("Id", "Name", "Detail", "PhotoLink", "Price", "Quantity", "CategoryId")] Product product)
        {
            product.Quantity = 50;
            if (file != null)
            {
                if (file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                    using (var fileSrteam = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileSrteam);
                    }
                    product.PhotoLink = "img/" + fileName;
                }
            }
             _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Admin");
        }
        [HttpPost]
        public async Task<IActionResult> Update(IFormFile file, [Bind("Id", "Name", "Detail", "PhotoLink", "Price", "Quantity", "CategoryId")] Product product)
        {
            if (file != null)
            {


                if (file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                    using (var fileSrteam = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileSrteam);
                    }
                    product.PhotoLink = "img/" + fileName;
                }
            }
            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction("Admin");
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "";
            if (userEmail == "")
            {
                return RedirectToAction("Login", "User");
            }
            var user = await _context.Users.FirstAsync(u => u.Email == userEmail);
            if (user.RoleId == 1)
            {
                var product = await _context.Products
                .Include(p => p.CartItems)
                .Include(p => p.ProductQuantities)
                .FirstAsync(p => p.Id == id);

                foreach (CartItem c in product.CartItems)
                {
                    _context.CartItems.Remove(c);
                }
                foreach (ProductQuantity pq in product.ProductQuantities)
                {
                    _context.ProductQuantities.Remove(pq);
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
                return RedirectToAction("Admin");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
            
        }
        //[HttpPost]
        /*public IActionResult UpdateQuantity([Bind("ProductId", "ShopId", "Quantity", "UpdateDate")] ProductQuantity productQuantity)
        {
            int quantity = 0;
            productQuantity.UpdateDate = DateTime.Now;
            _context.Add(productQuantity);
            _context.SaveChanges();
            var quantityInShop = _context.ProductQuantities
                
                
                .Where(p => p.ProductId == productQuantity.ProductId);
                
                
            foreach(ProductQuantity pq in quantityInShop)
            {
                quantity += 0;
            }
                
        }*/
    }
}
