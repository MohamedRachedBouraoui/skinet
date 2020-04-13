using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRep;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        public ProductsController(IMapper mapper, IGenericRepository<Product> productRepo
        , IGenericRepository<ProductBrand> productBrandRepo
        , IGenericRepository<ProductType> productTypeRepo
        )
        {
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
            _productRep = productRepo;
            _productBrandRepo = productBrandRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpec();
            IReadOnlyList<Product> productsFromRepo = await _productRep.ListBySpecAsync(spec);
            var productsToReturn = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(productsFromRepo);

            return Ok(productsToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpec(id);
            Product productFromRepo = await _productRep.GetBySpecAsync(spec);
            var productToReturn = _mapper.Map<ProductToReturnDto>(productFromRepo);
            return Ok(productToReturn);
        }


        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}