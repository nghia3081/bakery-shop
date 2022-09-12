using System;
using System.Collections.Generic;

namespace SE1616_Group3_Project.Models
{
    public partial class Product
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            ProductQuantities = new HashSet<ProductQuantity>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Detail { get; set; }
        public string PhotoLink { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<ProductQuantity> ProductQuantities { get; set; }
    }
}
