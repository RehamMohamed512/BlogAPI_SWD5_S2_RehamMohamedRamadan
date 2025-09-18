using Blog.Core.DTos;
using Blog.Core.Interfaces;
using Blog.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        //DI
        private readonly IGenaricRepository<Comment> _comment;

        public CommentController(IGenaricRepository<Comment> comment)
        {
            _comment = comment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Data = await _comment.GetAllAsync();
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
                var Data = await _comment.GetByIdAsync(id);
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
        public async Task<IActionResult> Add(CommentDTos commentdtos)
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
                var data = new Comment
                {
                    
                    Content = commentdtos.Content,
                    CreatedAt = DateTime.UtcNow,
                    UserId = commentdtos.UserId,
                    PostId = commentdtos.PostId,
                };
                //add/

                await _comment.CreateAsync(data);
                //save
                await _comment.SaveAsync();
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
        public async Task<IActionResult> Update(CommentDTos postdto)
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

                var olditem = await _comment.GetByIdAsync(postdto.Id);
                if (olditem == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "No Data found",

                    });
                }
                
                olditem.Content = postdto.Content;
                
                _comment.Update(olditem);
                await _comment.SaveAsync();

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
                var item = await _comment.GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "No data found",
                        Data = new List<Post>()
                    });
                }

                _comment.Delete(item);
                await _comment.SaveAsync();
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
