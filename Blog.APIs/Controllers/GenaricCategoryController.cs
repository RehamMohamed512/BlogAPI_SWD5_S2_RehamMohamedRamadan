using Blog.Core.DTos;
using Blog.Core.Interfaces;
using Blog.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenaricCategoryController : ControllerBase
    {

        //DI
        private readonly IGenaricRepository<Category> _Category;
        public GenaricCategoryController(IGenaricRepository<Category> category)
        {
            _Category = category;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cat = await _Category.GetAllAsync();
                if (cat == null || !cat.Any())
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "No Category Found",
                        Data = new List<Category>()
                    });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Data retrived",
                    Data = cat
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An Error Occurred",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cat = await _Category.GetByIdAsync(id);
                if (cat == null || !cat.Any())
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Messge = "No Data Found",

                    });
                }
                return Ok(new
                {
                    StatusCode = 200,
                    Messge = " Data Found",
                    Data = cat
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An Error Occurred",
                    Error = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            try
            {
                var cat = new Category { Name = category.Name };
                await _Category.CreateAsync(cat);
                await _Category.SaveAsync();
                return StatusCode(201, new
                {
                    StatusCode = 201,
                    Message = "Data Added successfully",
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An Error Occurred",
                    Error = ex.Message
                });
            }
        }


    }
}