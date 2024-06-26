﻿using Sem3GraphQL.Abstraction;
using Sem3GraphQL.DTO;
using Sem3GraphQL.Repo;

namespace Sem3GraphQL.GraphServises.Query
{
    public class Query
    {
        public IEnumerable<ProductViewModel> GetProducts([Service] ProductRepo product) 
            => product.GetProduts();
        public IEnumerable<ProductGroupViewModel> GetProductGroups([Service] ProductGroupRepo groups)
            => groups.GetProdutGroups();
        public IEnumerable<StorageViewModel> GetStorages([Service] StorageRepo storage) 
            => storage.GetStorages();
        public IEnumerable<ProductStorageViewModel> GetProductsFromStorage([Service] ProductStorageRepo productStorage) 
            => productStorage.GetProductsFromStorage();
    }
}
