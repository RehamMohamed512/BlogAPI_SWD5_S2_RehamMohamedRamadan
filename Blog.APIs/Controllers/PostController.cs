using Blog.Core.DTos;
using Blog.Core.Interfaces;
using Blog.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        //DI
        private readonly IGenaricRepository<Post> _post;
     
        public PostController(IGenaricRepository<Post> post)
        {
            _post = post;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Data = await _post.GetAllAsync();
                if (Data == null || !Data.Any())
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
                    Data = Data
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
                var Data = await _post.GetByIdAsync(id);
                if (Data == null || !Data.Any())
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
                    Data = Data
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
        public async Task<IActionResult> Add(PostDTos post)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        Message = "Invalid Data",
                        Error = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage),
                        StatusCode = 400
                    });
                }

                //convert to model
                var data = new Post { Title = post.Title, Content = post.Content,
                CreatedAt = DateTime.UtcNow, UserId = post.UserId, CategoryId = post.CategoryId,
                };
                //add/

                await _post.CreateAsync(data);
                //save
                await _post.SaveAsync();
                //return
                return Ok(new
                {
                    StatusCode = 201,
                    Message = "Post Craeted successfully",
                    Data = data
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

        [HttpPut]
        public async Task<IActionResult>Update (PostDTos postdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        Message = "Invalid Data",
                        Error = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage),
                        StatusCode = 400
                    });
                }

                var olditem = await _post.GetByIdAsync(postdto.Id);
                if (olditem == null )
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "No Data found",
                        
                    });
                }
                olditem.Title = postdto.Title;
                olditem.Content = postdto.Content;
                olditem.CategoryId = postdto.CategoryId;
                _post.Update(olditem);
                await _post.SaveAsync();

                return NoContent();
            } 
            catch (Exception e) 
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An Error Occurred",
                    Error = e.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _post.GetByIdAsync(id);
                if (item == null) {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "No data found",
                        Data = new List<Post>()
                    });
                }

                _post.Delete(item);
                await _post.SaveAsync();
                return NoContent();

            }

            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An Error Occurred",
                    Error = e.Message
                });
            }
        }
    }
}
