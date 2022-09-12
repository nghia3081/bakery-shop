using System;
using System.Collections.Generic;

namespace SE1616_Group3_Project.Models
{
    public partial class Shop
    {
        public Shop()
        {
            ProductQuantities = new HashSet<ProductQuantity>();
        }

        public int Id { get; set; }
        public string? StaffEmail { get; set; }
        public string Address { get; set; } = null!;

        public virtual User? StaffEmailNavigation { get; set; }
        public virtual ICollection<ProductQuantity> ProductQuantities { get; set; }
    }
}
