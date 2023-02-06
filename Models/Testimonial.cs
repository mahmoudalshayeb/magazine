using System;
using System.Collections.Generic;

#nullable disable

namespace magazine.Models
{
    public partial class Testimonial
    {
        public decimal Id { get; set; }
        public string Text { get; set; }
        public decimal? State { get; set; }
        public decimal? Userid { get; set; }

        public virtual Users1 User { get; set; }
    }
}
