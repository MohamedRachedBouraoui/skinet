using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpec : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpec(int id)
      : base(x => x.Id == id)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }

        public ProductsWithTypesAndBrandsSpec(ProductSpecParams productParams)
        : base(p =>
        (string.IsNullOrWhiteSpace(productParams.SearchBy) || p.Name.ToLower().Contains(productParams.SearchBy))// Search by Name
        &&
        (productParams.BrandId.HasValue == false || p.ProductBrandId == productParams.BrandId) // Search By BrandId
        &&
        (productParams.TypeId.HasValue == false || p.ProductTypeId == productParams.TypeId)) // Search By TypeId
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
            AddOrderBy(p => p.Name); //by default
            AddOrderByPrice(productParams.SortBy);
            ApplyPaging(productParams.PageIndex, productParams.PageSize);
        }

        private void AddOrderByPrice(string SortBy)
        {
            if (string.IsNullOrWhiteSpace(SortBy))
            {
                return;
            }
            switch (SortBy.ToLower())
            {
                case "priceasc":
                    AddOrderBy(p => p.Price);
                    break;
                case "pricedesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
        }


    }
}