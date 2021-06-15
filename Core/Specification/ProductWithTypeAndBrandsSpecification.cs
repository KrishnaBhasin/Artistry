using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductWithTypeAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandsSpecification(ProductsSpecParams productsSpecParams)
            :base(x=>
                (string.IsNullOrEmpty(productsSpecParams.Search) || x.Name.ToLower()
                .Contains(productsSpecParams.Search))&&
                (!productsSpecParams.BrandId.HasValue||x.ProductBrandId==productsSpecParams.BrandId)&& //filteration
                (!productsSpecParams.TypeId.HasValue||x.ProductTypeId==productsSpecParams.TypeId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderby(x => x.Name);
            ApplyPaging(productsSpecParams.PageSize * (productsSpecParams.PageIndex - 1), productsSpecParams.PageSize);

            if(!string.IsNullOrEmpty(productsSpecParams.Sort)) //orderby
            {
                switch(productsSpecParams.Sort)
                {
                    case "priceAsc":
                        AddOrderby(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderbyDesc(p => p.Price);
                        break;
                    default:
                        AddOrderby(x => x.Name);
                        break;
                }
            }
        }

        public ProductWithTypeAndBrandsSpecification(int id) : base(x=>x.Id==id)
        {

            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
