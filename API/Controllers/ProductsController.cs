using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core.Specifications;
using API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using API.Errors;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseAPIController
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IGenericRepository<Product> prodRepo;
        private readonly IGenericRepository<ProductBrand> prodBrandRepo;
        private readonly IGenericRepository<ProductType> prodTypeRepo;
        private readonly IMapper mapper;

        public ProductsController(ILogger<ProductsController> logger,IGenericRepository<Product> prodRepo,
        IGenericRepository<ProductBrand> prodBrandRepo,IGenericRepository<ProductType> prodTypeRepo,
        IMapper mapper)
        {
            _logger = logger;
            this.prodRepo = prodRepo;
            this.prodBrandRepo = prodBrandRepo;
            this.prodTypeRepo = prodTypeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts()
        {
            var spec = new ProductWithTypesAndBrandsSpecification();
            var products = await prodRepo.GetAllAsyncWithSpec(spec);

            return (mapper.Map<List<Product>,List<ProductDTO>>(products));
//            return OK(mapper.Map<List<Product>,List<ProductDTO>>(products));
        }

         [HttpGet("{id}")]
         [ProducesResponseType(StatusCodes.Status200OK)]
         [ProducesResponseType(typeof(APIResponse),StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);
            var product = await prodRepo.GetEntityAsyncWithSpec(spec);
          //  var product = await prodRepo.GetByIdAsync(id);
            if(product==null) return NotFound(new APIResponse(404));
            return mapper.Map<Product,ProductDTO>(product); 
          //  return product;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var brands = await prodBrandRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var types = await prodTypeRepo.GetAllAsync();
            return Ok(types);
        }

    }
}