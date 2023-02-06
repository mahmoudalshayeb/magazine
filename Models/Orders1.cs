using System;
using System.Collections.Generic;

#nullable disable

namespace magazine.Models
{
    public partial class Orders1
    {
        public decimal Id { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? Datefrom { get; set; }
        public DateTime? Dateto { get; set; }
        public decimal? Stats { get; set; }
        public decimal? Proid { get; set; }
        public decimal? Userid { get; set; }

        public virtual Product1 Pro { get; set; }
        public virtual Users1 User { get; set; }
    }
}
