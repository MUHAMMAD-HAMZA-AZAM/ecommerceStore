using API.DTO;
using API.Helpers;
using AutoMapper;
using Core.Interfaces;
using Core.PocoEntities;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{

    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _ProductRepo;
        private readonly IGenericRepository<ProductBrand> _BrandRepo;
        private readonly IGenericRepository<ProductType> _TypeRepo;
        private readonly IMapper _mapper;
        public ProductController(IGenericRepository<Product> ProductRepo,IGenericRepository<ProductBrand> BrandRepo, IGenericRepository<ProductType> TypeRepo, IMapper mapper)
        {
            _ProductRepo = ProductRepo;
            _BrandRepo = BrandRepo;
            _TypeRepo = TypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductReturnWithDTO>>> GetProducts([FromQuery] PaginationParameters paginationParameters)
        {
            var spec = new ProductsWithTypesAndBrandsSpecifications(paginationParameters);
            var countspec = new ProductWithFilterCountSpecification(paginationParameters);
            var totalitems = await _ProductRepo.CountAsync(countspec);
            var products = await _ProductRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductReturnWithDTO>>(products);
            return Ok(new Pagination<ProductReturnWithDTO>(paginationParameters.PageIndex,paginationParameters.PageSize,totalitems,data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReturnWithDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecifications(id);
            var products= await _ProductRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Product, ProductReturnWithDTO>(products);
        }

        [HttpGet("ProductBrands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _BrandRepo.GetAllListAsync());
        }

        [HttpGet("ProductTypes")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _TypeRepo.GetAllListAsync());
        }


    }
}
