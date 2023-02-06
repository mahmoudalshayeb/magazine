namespace magazine.Models
{
	public class JoinTable
	{
		public Category category { get; set; }
		public Contactu contactu { get; set; }

		public Orders1 orders1 { get; set; }

		public Product1 product1 { get; set; }

		public Roles1 roles1 { get; set; }
		public Testimonial testimonial { get; set; }

		public Users1 users1 { get; set; }
	}
}
