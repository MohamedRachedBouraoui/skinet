using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpec : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpec()
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }

        public ProductsWithTypesAndBrandsSpec(int id)
        : base(x => x.Id == id)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }
    }
}