using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.DTOs;
using Talabat.Helper;
using TalabatCore.Entities;
using TalabatCore.Repositories;
using TalabatCore.Specification;

namespace Talabat.Controllers
{

    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _product;
        private readonly IGenericRepository<ProductBrand> brand;
        private readonly IGenericRepository<ProductType> type;
        private readonly IMapper mapper;

        public ProductController(IGenericRepository<Product> product, IGenericRepository<ProductBrand> brand, IGenericRepository<ProductType> Type, IMapper mapper)
        {
            _product = product;
            this.brand = brand;
            type = Type;
            this.mapper = mapper;
        }
        [Authorize]
        [HttpGet()]
        public async Task<ActionResult<Pagination<ProductToDto>>> GetProducts([FromQuery] SpecificationParams specificationParams)
            {
            // this for ProductWithBrandTypeSpecification inside it Inhereates BaseSpecification >>>>>>go<<<<<<
            var spec = new ProductWithBrandTypeSpecification(specificationParams);
            var products = await _product.GetAllWithSpecificationAsync(spec);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToDto>>(products);
            var productcountspec = new ProductWithFilterForCountSpecification(specificationParams);
            var productCount = await _product.GetAcountAsync(productcountspec);
            return Ok(new Pagination<ProductToDto>(specificationParams.PageIndex, specificationParams.PageSize, productCount, data));
           }
        [Authorize]
        [HttpGet("{id}")]
        //{{baseUrl}}/Api/Product/1
            public async Task<ActionResult<ProductToDto>> GetById(int id)
            {
            var spec=new ProductWithBrandTypeSpecification(id);
                var product = await _product.GetByIdWithSpecificaionAsync(spec);
                return Ok(mapper.Map<Product, ProductToDto>(product));
            }
        [HttpGet("GetBrand")]
        public async Task<ActionResult <IReadOnlyList<BrandToReturnDTO>>> GetBrands()
        {

            var brands = await brand.GetAllAsync();
            var Brands = mapper.Map<IReadOnlyList<ProductBrand>, IReadOnlyList<BrandToReturnDTO>>(brands);
            return Ok(Brands);
        }
        [HttpGet("GetTypes")]
        public async Task<ActionResult <IReadOnlyList<TypeToReturnDTo>>> GetTypes()
        {
            var Types = await type.GetAllAsync();
            var model = mapper.Map<IReadOnlyList<ProductType>, IReadOnlyList<TypeToReturnDTo>>(Types);

            return Ok(model);
        }
    }
}
