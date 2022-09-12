using System;
using System.Collections.Generic;

namespace SE1616_Group3_Project.Models
{
    public partial class OrderItem
    {
        public OrderItem()
        {
            DeliveryStatuses = new HashSet<DeliveryStatus>();
        }

        public int Id { get; set; }
        public int? OrderId { get; set; }
        public string ProductName { get; set; } = null!;
        public string PhotoLink { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime? BoughtDate { get; set; }

        public virtual Order? Order { get; set; }
        public virtual ICollection<DeliveryStatus> DeliveryStatuses { get; set; }
    }
}
