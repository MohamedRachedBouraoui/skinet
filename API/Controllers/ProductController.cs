using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{

    public class ProductController : SkinetBaseController
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRep;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        public ProductController(IMapper mapper, IGenericRepository<Product> productRepo
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
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            var countSpec = new ProductsSpecForCount(productParams);
            var totalItemsInRepo = await _productRep.CountBySpecAsync(countSpec);

            var spec = new ProductsWithTypesAndBrandsSpec(productParams);
            IReadOnlyList<Product> productsFromRepo = await _productRep.ListBySpecAsync(spec);

            var dataToReturn = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(productsFromRepo);

            return Ok(new Pagination<ProductToReturnDto>
           (
                dataToReturn,
                 totalItemsInRepo,
                productParams.PageSize,
                 productParams.PageIndex
            ));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpec(id);
            Product productFromRepo = await _productRep.GetBySpecAsync(spec);
            if (productFromRepo == null)
            {
                return NotFound(new ApiResponse(404));
            }

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