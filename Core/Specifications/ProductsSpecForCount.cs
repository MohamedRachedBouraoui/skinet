using Core.Entities;

namespace Core.Specifications
{
    public class ProductsSpecForCount : BaseSpecification<Product>
    {

        public ProductsSpecForCount(ProductSpecParams productParams)
        : base(p =>
        (string.IsNullOrWhiteSpace(productParams.SearchBy) || p.Name.ToLower().Contains(productParams.SearchBy))// Search by Name
        &&
        (productParams.BrandId.HasValue == false || p.ProductBrandId == productParams.BrandId) // Search By BrandId
        &&
        (productParams.TypeId.HasValue == false || p.ProductTypeId == productParams.TypeId)) // Search By TypeId
        { }
    }
}