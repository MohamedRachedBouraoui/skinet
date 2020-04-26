using System.Linq;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsByIdsSpec : BaseSpecification<Product>
    {
        public ProductsByIdsSpec(IEnumerable<int> ids)
      : base(x => ids.ToList().Contains(x.Id))
        { }
    }
}