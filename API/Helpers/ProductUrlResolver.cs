using API.DTO;
using AutoMapper;
using Core.PocoEntities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductReturnWithDTO, string>
    {
        private readonly IConfiguration  _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductReturnWithDTO destination, string destMember, ResolutionContext context)
        {
           if(!string.IsNullOrEmpty(source.PictureURL))
            {
                return _config["ApiUrl"] + source.PictureURL;
            }
            return null;
        }
    }
}
