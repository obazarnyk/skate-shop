using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using educational_practice5.Models;
using educational_practice5.Repositories.Implimentations;
using educational_practice5.Repositories.Interfaces;
using educational_practice5.Services.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Castle.Core.Logging;
using educational_practice5.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using educational_practice4.Extensions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace educational_practice5.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController: ControllerBase
    {
        private IProductRepository _repository;
        private IMapper _mapper;
        private IDescriptionRepository _descRepository;
        private IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        public ProductController(IProductRepository repository,  
            IMapper mapper, 
            IDescriptionRepository descRepository,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            _repository = repository;
            _mapper = mapper;
            _descRepository = descRepository;
            _configuration = configuration;
            _environment = environment;
        }
        
        [HttpGet]
        public IActionResult GetProducts([FromQuery] ProductParameters productParameters)// ,[FromQuery]string brands, [FromQuery]string sizes, [FromQuery]string seasons)
        {
            try
            {
                //var products = _repository.GetProducts(productParameters, brands.Split(","), sizes.Split(","), seasons.Split(","));
                var products = _repository.GetProducts(productParameters);
                var result = _mapper.Map<IEnumerable<ProductDto>>(products);
                return Ok(new PagedResponse(result,_repository.FindAll().Count()));
            }
            catch (Exception ex) { return StatusCode(500, $"Internal server error  - {ex.Message}"); }
        }
        
        [HttpGet("{id}", Name = "ProductById")]
        public IActionResult GetProductById(Guid id)
        {
            try
            {
                var product = _repository.GetProductById(id);
                if (product == null)
                {
                    var mes = new JsonResult($"Product with id: {id}, hasn't been found in db.");
                    mes.StatusCode = 404;
                    return mes;
                }
                else
                {
                    var result = _mapper.Map<ProductDto>(product);
                    return Ok(result);
                }
            }
            catch (Exception ex) { return StatusCode(500, $"Internal server error {ex.Message}"); }
        }
        
        
        //[MyAuthorize(RoleEnum=UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(IFormCollection form)
        {
            var dict = form.FormToDictionary();
            var entity = dict.ToObject<ProductForCreationDto>();
            var files = form.Files;
            var links = await _repository.UploadImages(files, _configuration, _environment.ContentRootPath);
            if (links != null)
            {
                entity.SrcFirst = links.First();
                entity.SrcSecond = links.Last();
            }
            if (!TryValidateModel(entity)) return BadRequest(ModelState);
             
            _repository.CreateProduct(_mapper.Map<Product>(entity));

            return Ok();
        }
        
        
        [MyAuthorize(RoleEnum = UserRoles.Admin)]
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(Guid id, [FromBody]ProductForUpdateDto product)
        {
            try
            {
                if (product == null) { return BadRequest("Product object is null"); }
                if (!ModelState.IsValid) { return BadRequest("Invalid model object"); }
                
                var productById = _repository.GetProductById(id);
                
                if (productById == null) { return NotFound(); }
                _mapper.Map(product, productById);
                _repository.UpdateProduct(productById);
                return NoContent();
            }
            catch (Exception ex) { return StatusCode(500, $"Internal server error {ex.Message}"); }
        }
        
        
        // [MyAuthorize(RoleEnum = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            try
            {
                var product = _repository.GetProductById(id);
                if(product == null) { return NotFound(); }
                _repository.DeleteProduct(product);

                return NoContent();
            }
            catch (Exception ex) { return StatusCode(500, $"Internal server error {ex.Message}"); }
        }
        
        [HttpGet("brands")]
        public IActionResult GetBrands()
        {
            try
            {
                var brands = _descRepository.GetBrands();
                return Ok(new {
                    Brands = brands
                });
            }
            catch (Exception ex) { return StatusCode(500, $"Internal server error  - {ex.Message}"); }
        }
        
        [HttpGet("sizes")]
        public IActionResult GetSizes()
        {
            try
            {
                var sizes = _descRepository.GetSizes();
                return Ok(new {
                    Sizes = sizes
                });
            }
            catch (Exception ex) { return StatusCode(500, $"Internal server error  - {ex.Message}"); }
        }
        
        [HttpGet("seasons")]
        public IActionResult GetSeasons()
        {
            try
            {
                var seasons = _descRepository.GetSeasons();
                return Ok(new {
                    Seasons = seasons
                });
            }
            catch (Exception ex) { return StatusCode(500, $"Internal server error  - {ex.Message}"); }
        }
    }
}   
