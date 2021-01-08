using Core.PocoEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecifications : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecifications(PaginationParameters paginationParameters)
            :base( s=>
            ( string.IsNullOrEmpty(paginationParameters.Search) || s.Name.ToLower().Contains(paginationParameters.Search)) &&
            (!paginationParameters.BrandId.HasValue || s.ProductBrandId == paginationParameters.BrandId) &&
            (!paginationParameters.TypeId.HasValue || s.ProductTypeId == paginationParameters.TypeId)
            
            )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(s => s.Name);
            ApplyPagination(paginationParameters.PageSize * (paginationParameters.PageIndex - 1),paginationParameters.PageSize);

            if(!string.IsNullOrEmpty(paginationParameters.Sort))
            {
                switch (paginationParameters.Sort)
                {

                    case "priceAsc":
                        AddOrderBy(s=> s.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDescinding(s => s.Price);
                        break;
                     
                       default:
                        AddOrderBy(s => s.Name);
                        break;
                }

            }
        }

        public ProductsWithTypesAndBrandsSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
