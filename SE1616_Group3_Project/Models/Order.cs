using System;
using System.Collections.Generic;

namespace SE1616_Group3_Project.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public string? UserName { get; set; }
        public string? Phone { get; set; }
        public string? ShipAddress { get; set; }
        public decimal Amount { get; set; }
        public int? PaymentMethod { get; set; }
        public bool PaymentStatus { get; set; }

        public virtual PaymentMethod? PaymentMethodNavigation { get; set; }
        public virtual User? UserEmailNavigation { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
