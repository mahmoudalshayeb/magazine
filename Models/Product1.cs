using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace magazine.Models
{
    public partial class Product1
    {
        public Product1()
        {
            Orders1s = new HashSet<Orders1>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? Sale { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Catid { get; set; }
        public string Imgpath { get; set; }
        public decimal? Ispublished { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual Category Cat { get; set; }
        public virtual ICollection<Orders1> Orders1s { get; set; }
    }
}
