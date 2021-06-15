using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductWithFilterForCountSpecification:BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductsSpecParams productsSpecParams) : base(x =>
                  (string.IsNullOrEmpty(productsSpecParams.Search) || x.Name.ToLower()
                    .Contains(productsSpecParams.Search))&&
                  (!productsSpecParams.BrandId.HasValue || x.ProductBrandId == productsSpecParams.BrandId) && //filteration
                  (!productsSpecParams.TypeId.HasValue || x.ProductTypeId == productsSpecParams.TypeId)
            )
        {

        }
    }
}
