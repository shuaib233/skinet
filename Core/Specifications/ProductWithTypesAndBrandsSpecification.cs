using System;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesAndBrandsSpecification(ProductSpecParams prodParams)
              : base(
              x=>
              (string.IsNullOrEmpty(prodParams.Search) || x.Name.ToLower().Contains(prodParams.Search))
              &&
              (!prodParams.BrandId.HasValue || x.ProductBrandId == prodParams.BrandId) &&
              (!prodParams.TypeId.HasValue || x.ProductTypeId == prodParams.TypeId)
              )

        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);
            AddOrderByAsc(x=>x.Name);
            ApplyPaging(prodParams.PageSize * (prodParams.PageIndex-1),prodParams.PageSize);

            if(!string.IsNullOrEmpty(prodParams.sort))
            {
                switch(prodParams.sort)
                {
                    case "priceAsc":
                    AddOrderByAsc(x=>x.Price);
                    break;
                    case "priceDesc":
                    AddOrderByDesc(x=>x.Price);
                    break;
                    default:
                    AddOrderByAsc(x=>x.Name);
                    break;
                }

            }

        }

        public ProductWithTypesAndBrandsSpecification(int id)
              : base(x=>x.Id== id)
        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);
        }
    }
}