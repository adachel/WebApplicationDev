using WebApplicationDevSem.DTO;

namespace WebApplicationDevSem.Abstraction
{
    public interface IProductGroupRepo
    {
        public void AddProductGroup(ProductGroupViewModel productGroupViewModel);
        public IEnumerable<ProductGroupViewModel> GetProdutGroups();
    }
}
