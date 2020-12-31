using API.DTO;
using AutoMapper;
using Core.PocoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductReturnWithDTO>()
                //Custome Way to Add Specific Data Into DTO
                .ForMember(p => p.ProductBrand, o => o.MapFrom(src => src.ProductBrand.Name))
                .ForMember(p => p.ProductType, o => o.MapFrom(src => src.ProductType.Name))
                .ForMember(p => p.PictureURL, o => o.MapFrom<ProductUrlResolver>());

        }

    }
}
