namespace ASPExampleDBLec2.DB
{
    public class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
