using Blog.Core.DTos;
using Blog.Core.Interfaces;
using Blog.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blog.APIs.Controllers
{
    [Route("api/[controller]")] //http://localhost:5292/api/Categories
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        //category services
        //Di
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                if (categories == null || !categories.Any())
                    return NotFound(new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "No categories found",
                        Data = new List<Category>()
                    });
                else
                    return Ok(new
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Categories retrieved successfully",
                        Data = categories
                    });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while retrieving categories",
                    Error = ex.Message
                });

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = _categoryService.GetByIdAsync(id);
                if (category is null)
                    return NotFound(new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Category with {id} not found",
                        Data = new Category()
                    });
                else
                    return Ok(new
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Category retrieved successfully",
                        Data = category
                    });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while retrived categories",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Message = "Invalid category data",
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage),
                        StatusCode = StatusCodes.Status400BadRequest,

                    });

                var cat = await _categoryService.CreateAsync(categoryDTO);
                return StatusCode(StatusCodes.Status201Created, new
                {
                    Message = "Category created successfully",
                    Data = cat,
                    StatusCode = StatusCodes.Status201Created
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while creating categories",
                    Error = ex.Message
                });
                }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,CategoryDTO catDTO)
        {
            try
            {
                var oldcat = await _categoryService.GetByIdAsync(id);
                if (oldcat is null)
                { return NotFound(new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Category with {id} not found",
                        Data = new Category()
                    });
                }
                if (await _categoryService.UpdateAsync(id,catDTO))
                {
                    return Ok(new
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Category updated successfully",
                        Data = await _categoryService.GetByIdAsync(id)
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Failed to update category",
                        Data = catDTO
                    });
                }


            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while updating categories",
                    Error = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CategoryDTO catDTO)
        {
            try
            {
                var oldcat = await _categoryService.GetByIdAsync(catDTO.Id);
                if (oldcat is null)
                {
                    return NotFound(new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Category with {catDTO.Id} not found",
                        Data = new Category()
                    });
                }
                if (await _categoryService.UpdateAsync(catDTO))
                {
                    return Ok(new
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Category updated successfully",
                        Data = await _categoryService.GetByIdAsync(catDTO.Id)
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Failed to update category",
                        Data = catDTO
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while updating categories",
                    Error = ex.Message
                });
            } }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){


            try
            {
                var cat = await _categoryService.GetByIdAsync(id);
                if (cat is null)
                    return NotFound(new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "category not found",
                        
                    });

                if (await _categoryService.DeleteAsync(id))
                {
                    return Ok(new
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "category Deleted successfully",
                        oldData= cat

                    });
                }

                else
                    return BadRequest(new
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "category not Deleted",
                    });
            }
            catch(Exception ex) {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while retrieving categories",
                    Error = ex.Message
                });
            
            }
        }


    }
}
