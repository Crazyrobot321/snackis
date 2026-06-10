using Microsoft.AspNetCore.Mvc;
using Snackis.Application.DTOs;
using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;

namespace Snackis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostService postService, ILogger<PostController> logger)
        {
            _postService = postService;
            _logger = logger;
        }

        private static PostDTO MapToDto(ApplicationPost p) => new()
        {
            Id = p.Id,
            Content = p.Content,
            AuthorName = p.User?.UserName ?? "Unknown",
            CreatedAt = p.CreatedAt,
            TopicId = p.TopicId,
            UserId = p.UserId
        };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts()
        {
            try
            {
                var posts = await _postService.GetAllPostsAsync();
                return Ok(posts.Select(MapToDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving posts");
                return Problem();
            }
        }

        [HttpGet("topic/{topicId}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostsByTopic(int topicId)
        {
            try
            {
                if (topicId <= 0)
                    return BadRequest("Invalid topic ID");

                var posts = await _postService.GetAllPostsByTopicAsync(topicId);
                return Ok(posts.Select(MapToDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving posts for topic {TopicId}", topicId);
                return Problem();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPostById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid post ID");

                var post = await _postService.GetPostByIdAsync(id);
                if (post == null)
                    return NotFound($"Post with ID {id} not found");

                return Ok(MapToDto(post));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving post");
                return Problem();
            }
        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> Create([FromBody] CreatePostDTO dto)
        {
            try
            {
                if (dto == null) 
                    return BadRequest("Post cannot be null");
                if (string.IsNullOrWhiteSpace(dto.Content)) 
                    return BadRequest("Post content is required");
                if (dto.TopicId <= 0) 
                    return BadRequest("Valid topic ID is required");

                var newPost = new ApplicationPost
                {
                    Content = dto.Content,
                    TopicId = dto.TopicId,
                    UserId = dto.UserId,
                    CreatedAt = DateTime.UtcNow
                };

                await _postService.CreatePostAsync(newPost);
                var created = await _postService.GetPostByIdAsync(newPost.Id);

                return CreatedAtAction(nameof(GetPostById), new { id = newPost.Id }, MapToDto(created!));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating post");
                return Problem();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PostDTO dto)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid post ID");
                if (dto == null)
                    return BadRequest("Post cannot be null");
                if (dto.Id != id)
                    return BadRequest("Post ID mismatch");
                if (string.IsNullOrWhiteSpace(dto.Content)) 
                    return BadRequest("Post content is required");

                var existing = await _postService.GetPostByIdAsync(id);
                if (existing == null)
                    return NotFound($"Post with ID {id} not found");

                existing.Content = dto.Content;

                await _postService.UpdatePostAsync(existing);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating post");
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid post ID");

                var post = await _postService.GetPostByIdAsync(id);
                if (post == null)
                    return NotFound($"Post with ID {id} not found");

                await _postService.DeletePostAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting post");
                return Problem();
            }
        }
    }
}