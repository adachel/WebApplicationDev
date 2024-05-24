namespace ASPExampleDBLec2.DB
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }  
    }
}
