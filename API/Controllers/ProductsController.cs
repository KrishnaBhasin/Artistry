using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private IGenericRespository<Product> _productsRepo;
        private IGenericRespository<Product> _productsTypeRepo;
        private IGenericRespository<Product> _productsBrandsRepo;
        private IMapper _mapper;

        public ProductsController(IGenericRespository<Product> productRepository,
            IGenericRespository<ProductBrand> productBrandsRepository,
            IGenericRespository<ProductType> productTypesRepository,IMapper mapper )
        {
            _productsRepo = productRepository;
            _productsTypeRepo = productRepository;
            _productsBrandsRepo = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProduct()
        {
            var spec = new ProductWithTypeAndBrandsSpecification();
            var products=await _productsRepo.ListAsync(spec);

            return Ok(_mapper
                .Map<IReadOnlyList<Product>, 
                IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductWithTypeAndBrandsSpecification(id);

            var product=await _productsRepo.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(product);

        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productsBrandsRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productsTypeRepo.ListAllAsync());
        }
    }
}
