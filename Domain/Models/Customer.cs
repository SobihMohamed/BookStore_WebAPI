namespace Domain.Models
{
    public class Customer : BaseEntity<int>
    {
        public string FullName { get; set; }
        // ربط ملف العميل بحساب الـ Identity (Foreign Key)
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
