using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace magazine.Models
{
    public partial class Category
    {
        public Category()
        {
            Product1s = new HashSet<Product1>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Imgpath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual ICollection<Product1> Product1s { get; set; }
    }
}
