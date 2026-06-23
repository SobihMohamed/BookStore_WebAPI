namespace Domain.Models
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}