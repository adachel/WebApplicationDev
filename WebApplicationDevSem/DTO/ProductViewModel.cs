namespace WebApplicationDevSem.DTO
{
    public class ProductViewModel
    {
        public int Id { get; set; } = 0;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ProductGroupId { get; set; }
        public float Price { get; set; }   
    }
}
