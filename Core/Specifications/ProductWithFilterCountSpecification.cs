using Core.PocoEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
   public class ProductWithFilterCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterCountSpecification( PaginationParameters paginationParameters)
            : base(s =>
            (string.IsNullOrEmpty(paginationParameters.Search) || s.Name.ToLower().Contains(paginationParameters.Search)) &&
            (!paginationParameters.BrandId.HasValue || s.ProductBrandId == paginationParameters.BrandId) &&
            (!paginationParameters.TypeId.HasValue || s.ProductTypeId == paginationParameters.TypeId)

            )
        {

        }
    }
}
