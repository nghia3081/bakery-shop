using System;
using System.Collections.Generic;

namespace SE1616_Group3_Project.Models
{
    public partial class DeliveryStatus
    {
        public int OrderItem { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string DeliveryUnit { get; set; } = null!;
        public string ShippingStatus { get; set; } = null!;
        public bool ShippingCompleted { get; set; }

        public virtual OrderItem OrderItemNavigation { get; set; } = null!;
    }
}
