using System;
using System.Collections.Generic;

namespace SE1616_Group3_Project.Models
{
    public partial class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public string PhotoLink { get; set; } = null!;
        public bool? EnableStatus { get; set; }
        public string? Owner { get; set; }

        public virtual User? OwnerNavigation { get; set; }
    }
}
