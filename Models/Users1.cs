using System;
using System.Collections.Generic;

#nullable disable

namespace magazine.Models
{
    public partial class Users1
    {
        public Users1()
        {
            Orders1s = new HashSet<Orders1>();
            Testimonials = new HashSet<Testimonial>();
        }

        public decimal Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Review { get; set; }
        public decimal? Phonenumber { get; set; }
        public decimal? Roleid { get; set; }

        public virtual Roles1 Role { get; set; }
        public virtual ICollection<Orders1> Orders1s { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
