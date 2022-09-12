using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SE1616_Group3_Project.Models;

namespace SE1616_Group3_Project.Controllers
{
    public class OrderController : Controller
    {
        private readonly BakingIngredientsContext _context;
        public OrderController(BakingIngredientsContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "";

            if (userEmail == "")
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var order = _context.Orders
                    .Include(o => o.PaymentMethodNavigation)
                    .Include(o => o.OrderItems)
                    .ThenInclude(ot => ot.DeliveryStatuses)
                    

                    .Where(o => o.UserEmail == userEmail).ToList();

                
             



                ViewData["orders"] = order;
                return View();
            }

        }
        public async Task<IActionResult> OrderComplete(IFormCollection value)
        {
            int orderId = int.Parse(value["Id"]);
            var orderItem = _context.OrderItems.Where(ot => ot.OrderId == orderId);
            foreach(OrderItem ot in orderItem)
            {
                DeliveryStatus ds = new DeliveryStatus();
                ds.OrderItem = ot.Id;
                ds.DeliveryUnit = "";
                ds.ShippingStatus = "Order Complete Confirmed";
                ds.ShippingCompleted = true;
                ds.UpdatedTime = DateTime.Now;
                await _context.AddAsync(ds);
               
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Admin()
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "";
            if (userEmail == "")
            {
                return RedirectToAction("Login", "User");
            }
            else
            {

                var user = _context.Users.First(u => u.Email == userEmail);
                if (user.RoleId ==2)
                {
                    var order = _context.Orders

                        .Include(o => o.PaymentMethodNavigation)
                        .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.DeliveryStatuses)
                       
                        .ToList();
                        

                    ViewData["orders"] = order;
                    return View();

                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus([Bind("OrderItem", "UpdatedTime", "DeliveryUnit", "ShippingStatus", "ShippingCompleted")] DeliveryStatus delivery)
        {
            delivery.UpdatedTime = DateTime.Now;
            await _context.AddAsync(delivery);
            _context.SaveChanges();
            return RedirectToAction("Admin");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatusForAll(IFormCollection value)
        {
            int orderId = int.Parse(value["Id"]);
            string deliveryUnit = value["DeliveryUnit"];
            string shipStatus = value["ShippingStatus"];
            var orderItem = _context.OrderItems.Where(ot => ot.OrderId == orderId);
            foreach (OrderItem ot in orderItem)
            {
                DeliveryStatus ds = new DeliveryStatus();
                ds.OrderItem = ot.Id;
                ds.DeliveryUnit = deliveryUnit;
                ds.ShippingStatus = shipStatus;
                ds.ShippingCompleted = false;
                ds.UpdatedTime = DateTime.Now;
                await _context.AddAsync(ds);
            }
          
            
            _context.SaveChanges();
            return RedirectToAction("Admin");
        }
        [HttpPost]
        public IActionResult Add([Bind("Id", "UserEmail", "UserName", "Phone", "ShipAddress", "Amount", "PaymentMethod")] Order order)
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "";
            if (userEmail == "")
            {
                return RedirectToAction("Login", "User");
            }
            else
            {

                if (order.PaymentMethod == 1)
                {
                    order.PaymentStatus = false;
                }
                else
                {
                    order.PaymentStatus = true;
                }
                _context.Orders.Add(order);
                _context.SaveChanges();
                int orderId = order.Id;
                var orderItem = _context.CartItems
                    .Include(c => c.Product)
                    .Where(c => c.UserEmail == userEmail);
                decimal amount = 0;
                foreach (CartItem c in orderItem)
                {
                    OrderItem item = new OrderItem();
                    item.OrderId = orderId;
                    item.ProductName = c.Product.Name;
                    item.PhotoLink = c.Product.PhotoLink;
                    item.Price = c.Product.Price;
                    item.Quantity = c.Quantity;
                    item.BoughtDate = DateTime.Now;
                    _context.OrderItems.Add(item);
                    amount += item.Quantity * item.Price;

                }
                _context.SaveChanges();
                order.Amount = amount;
                _context.Orders.Update(order);
                _context.SaveChanges();
                foreach (CartItem c in orderItem)
                {

                    c.Product.Quantity -= c.Quantity;
                    _context.Products.Update(c.Product);

                }
                _context.SaveChanges();
                foreach (CartItem c in orderItem)
                {

                    _context.CartItems.Remove(c);

                }
                _context.SaveChanges();

            }


            return RedirectToAction("Index", "Order");

        }



    }
}
