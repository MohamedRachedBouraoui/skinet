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
        (string.IsNullOrWhiteSpace(productParams.Search) || p.Name.ToLower().Contains(productParams.Search))// Search by Name
        &&
        (productParams.BrandId.HasValue == false || p.ProductBrandId == productParams.BrandId) // Search By BrandId
        &&
        (productParams.TypeId.HasValue == false || p.ProductTypeId == productParams.TypeId)) // Search By TypeId
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
            AddOrderBy(p => p.Name); //by default
            AddOrderByPrice(productParams.Sort);
            ApplyPaging(productParams.PageIndex, productParams.PageSize);
        }

        private void AddOrderByPrice(string sort)
        {
            if (string.IsNullOrWhiteSpace(sort))
            {
                return;
            }
            switch (sort.ToLower())
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