using ECommerceApi.Application.DTOS.Products;
using ECommerceApi.Application.Repositories;
using ECommerceApi.Application.RequestParameters;
using ECommerceApi.Application.Services;
using ECommerceApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ECommerceApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository  _productReadRepository;
        readonly IWebHostEnvironment _webHostEnvironment;
        readonly IFileService _fileService;


        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            if (pagination.Page < 1 || pagination.Size < 1)
                return BadRequest("Page and Size must be greater than 0");

            var products = await _productReadRepository.GetAll(false)
                .Skip((pagination.Page - 1) * pagination.Size) // Örneğin Page = 2, Size = 10 -> Skip(10) olur
                .Take(pagination.Size) // Sayfa başına ürün sayısı kadar al
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Stock,
                    p.Price,
                    p.CreatedDate,
                    p.UpdatedDate
                })
                .ToListAsync(); // Burayı ekledik

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id,false);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpPost]

        public async Task<IActionResult> Post(CreateProductDto createProductDto)
        {
            if(ModelState.IsValid)
            {

            }

            await _productWriteRepository.AddAsync(
                new()
                {
                    Name = createProductDto.Name,
                    Price = createProductDto.Price,
                    Stock = createProductDto.Stock
                });

            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put (UpdateProductDto updateProductDto)
        {
            Product product =  await _productReadRepository.GetByIdAsync(updateProductDto.Id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = updateProductDto.Name;
            product.Price = updateProductDto.Price;
            product.Stock = updateProductDto.Stock;
            //_productWriteRepository.Update(product); // tracke edildiği için gerek yok
            await _productWriteRepository.SaveAsync();



            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }


        //[HttpPost("[action]")]
        //public async Task<IActionResult> Upload()
        //{
        //    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/products-image");

        //    if (!Directory.Exists(uploadPath) )
        //        Directory.CreateDirectory(uploadPath);

        //    foreach (IFormFile file in Request.Form.Files)
        //    {
        //        string fullPath = Path.Combine(uploadPath, file.Name);

        //        using FileStream fileStream = new(fullPath, FileMode.Create,FileAccess.Write,FileShare.None,1024 * 1024,useAsync: false);
        //        await file.CopyToAsync(fileStream);
        //        await fileStream.FlushAsync();



        //    }


        //    return Ok();
        //}

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            List<(string fileName, string path)> files = await _fileService.UploadAsync("resource/products-image", Request.Form.Files);
            return Ok(new {files});

        }


    }
}
