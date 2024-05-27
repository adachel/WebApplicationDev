namespace Lec3LibraryApi.DB
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
