namespace WebApplicationDevSem.Models
{
    public class ProductStorage
    {
        public virtual Product? Product { get; set; }
        public virtual Storage? Storage { get; set; }
        public int ProductId { get; set; }
        public int StorageId { get; set; }
        public int Count { get; set; }
    }
}
