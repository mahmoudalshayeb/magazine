using System;
using System.Collections.Generic;

#nullable disable

namespace magazine.Models
{
    public partial class Roles1
    {
        public Roles1()
        {
            Users1s = new HashSet<Users1>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Users1> Users1s { get; set; }
    }
}
