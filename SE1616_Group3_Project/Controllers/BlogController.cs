using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SE1616_Group3_Project.Models;

namespace SE1616_Group3_Project.Controllers
{
    public class BlogController : Controller
    {
        private readonly BakingIngredientsContext _context;
        public BlogController(BakingIngredientsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {


            var blog = _context.Blogs
            .Include(b => b.OwnerNavigation)
            .Where(b => b.EnableStatus == true);
            ViewData["blogs"] = blog;

            return View();


        }
        public async Task<IActionResult> Detail(int id)
        {


            var blog = await _context.Blogs
            .Include(b => b.OwnerNavigation)
            .FirstOrDefaultAsync(b => b.Id == id);


            return View(blog);


        }
        public async Task<IActionResult> Admin()
        {
            string userEmail = HttpContext.Session.GetString("userEmail")??"";
            if(userEmail == "")
            {
                return RedirectToAction("Login", "User");
            }
            var user = await _context.Users.Include(u => u.Role).FirstAsync(u => u.Email == userEmail);
            if (user.RoleId == 3)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var blog = _context.Blogs
            .Include(b => b.OwnerNavigation);

                ViewData["blogs"] = blog;
                return View();
            }

        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            var blog = _context.Blogs
                .Include(b => b.OwnerNavigation)
                .First(b => b.Id == id);
            return View(blog);
        }
       
        public async Task<IActionResult> ChangeStatus(int id)
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "";
            if (userEmail == "")
            {
                return RedirectToAction("Index", "Blog");
            }
            var user = await _context.Users.FirstAsync(u => u.Email == userEmail);
            if (user.RoleId == 1)
            {
                var blog = await _context.Blogs.FirstAsync(b => b.Id == id);
                if (blog.EnableStatus == true)
                {
                    blog.EnableStatus = false;
                }
                else
                {
                    blog.EnableStatus = true;
                }
                _context.Blogs.Update(blog);
                _context.SaveChanges();
                return RedirectToAction("Admin");
            } else
            {
                return RedirectToAction("Index", "Blog");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file,[Bind("Id", "Title", "Detail", "PhotoLink", "EnableStatus", "Owner")] Blog blog)
        {
            blog.PhotoLink = "";
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
                    blog.PhotoLink = "img/" + fileName;
                }

            }
            
            blog.Owner = HttpContext.Session.GetString("userEmail");
            blog.EnableStatus = true;
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Admin");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(IFormFile file,[Bind("Id", "Title", "Detail", "PhotoLink", "EnableStatus", "Owner")] Blog blog)
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
                    blog.PhotoLink = "img/" + fileName;
                }
            }
            if(blog.PhotoLink == null)
            {
                blog.PhotoLink = "";
            }
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
            return View(blog);
        }
       
        public async Task<IActionResult> Delete(int id)
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "";
            if (userEmail == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _context.Users.FirstAsync(u => u.Email == userEmail);
            if (user.RoleId == 1)
            {
                var blog = await _context.Blogs.FirstAsync(b => b.Id == id);
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Admin");
            } else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
    }
}
