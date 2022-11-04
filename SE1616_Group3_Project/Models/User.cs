using System;
using System.Collections.Generic;

namespace SE1616_Group3_Project.Models
{
    public partial class User
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
            CartItems = new HashSet<CartItem>();
            Orders = new HashSet<Order>();
            Shops = new HashSet<Shop>();
        }

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? Age { get; set; }
        public bool? Gender { get; set; }
        public string? PhotoLink { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
    }
}
