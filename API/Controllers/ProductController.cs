using API.DTO;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _ProductRepo;
        private readonly IMapper _mapper;
        public ProductController(IGenericRepository<Product> ProductRepo, IMapper mapper)
        {
            _ProductRepo = ProductRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductReturnWithDTO>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecifications();
            var products = await _ProductRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductReturnWithDTO>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReturnWithDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecifications(id);
            var products= await _ProductRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Product, ProductReturnWithDTO>(products);
        }
    }
}
