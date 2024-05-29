using Sem3GraphQL.DTO;

namespace Sem3GraphQL.Abstraction
{
    public interface IProductGroupRepo
    {
        public int AddProductGroup(ProductGroupViewModel productGroupViewModel);
        public IEnumerable<ProductGroupViewModel> GetProdutGroups();
        public int DeleteProductGroup(int id);
    }
}
