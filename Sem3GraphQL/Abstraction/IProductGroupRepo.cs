using Sem3GraphQL.DTO;

namespace Sem3GraphQL.Abstraction
{
    public interface IProductGroupRepo
    {
        public void AddProductGroup(ProductGroupViewModel productGroupViewModel);
        public IEnumerable<ProductGroupViewModel> GetProdutGroups();
        public void DeleteProductGroup(int id);
    }
}
