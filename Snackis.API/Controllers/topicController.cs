using Microsoft.AspNetCore.Mvc;
using Snackis.Application.DTOs;
using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;

namespace Snackis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly ILogger<TopicController> _logger;

        public TopicController(ITopicService topicService, ILogger<TopicController> logger)
        {
            _topicService = topicService;
            _logger = logger;
        }
		private static TopicDTO MapToDto(ApplicationTopic t) => new()
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            AuthorName = t.Author?.UserName ?? "Unknown",
            UserId = t.AuthorId,
            Created = t.Created,
            SubCategoryId = t.SubCategoryId,
            PostCount = t.Posts?.Count ?? 0,
            ImageSource = t.ImageSource,
        };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicDTO>>> GetAllTopics()
        {
            try
            {
                var topics = await _topicService.GetAllTopicsAsync();
                return Ok(topics.Select(MapToDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving topics");
                return Problem();
            }
        }

        [HttpGet("subcategory/{subCategoryId}")]
        public async Task<ActionResult<IEnumerable<TopicDTO>>> GetAllTopicsBySubId(int subCategoryId)
        {
            try
            {
                if (subCategoryId <= 0)
                    return BadRequest("Invalid subcategory ID");

                var topics = await _topicService.GetAllTopicsBySubId(subCategoryId);
                return Ok(topics.Select(MapToDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving topics for subcategory");
                return Problem();
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TopicDTO>> GetTopicById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid topic ID");

                var topic = await _topicService.GetTopicByIdAsync(id);
                if (topic == null)
                    return NotFound($"Topic with ID {id} not found");

                return Ok(MapToDto(topic));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving topic");
                return Problem();
            }
        }

        [HttpGet("{id}/posts")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostsByTopicId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid topic ID");

                var posts = await _topicService.GetPostsByTopicIdAsync(id);
                return Ok(posts.Select(p => new PostDTO
                {
                    Id = p.Id,
                    Content = p.Content,
                    AuthorName = p.User?.UserName ?? "Unknown",
                    CreatedAt = p.CreatedAt,
                    TopicId = p.TopicId,
                    UserId = p.UserId
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving posts for topic");
                return Problem();
            }
        }

        [HttpPost]
        public async Task<ActionResult<TopicDTO>> CreateTopic([FromBody] CreateTopicDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Topic cannot be null");
                if (string.IsNullOrWhiteSpace(dto.Title)) 
                    return BadRequest("Topic title is required");
                if (string.IsNullOrWhiteSpace(dto.Description))
                    return BadRequest("Topic description is required");
                if (string.IsNullOrWhiteSpace(dto.AuthorId))
                    return BadRequest("AuthorId is required");
                if (dto.CategoryId <= 0) 
                    return BadRequest("Valid category ID is required");

                var topic = new ApplicationTopic
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    CategoryId = dto.CategoryId,
                    SubCategoryId = dto.SubCategoryId,
                    AuthorId = dto.AuthorId,
                    Created = dto.Created ?? DateTime.UtcNow,
                    ImageSource = dto.ImageSource
                };

                await _topicService.CreateTopicAsync(topic);
                var created = await _topicService.GetTopicByIdAsync(topic.Id);

                return CreatedAtAction(nameof(GetTopicById), new { id = topic.Id }, MapToDto(created));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating topic");
                return Problem();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateTopicDTO dto)
        {
            try
            {
                if (id <= 0) 
                    return BadRequest("Invalid topic ID");
                if (dto == null) 
                    return BadRequest("Topic cannot be null");
                if (string.IsNullOrWhiteSpace(dto.Title)) 
                    return BadRequest("Topic title is required");

                var existing = await _topicService.GetTopicByIdAsync(id);
                if (existing == null)
                    return NotFound($"Topic with ID {id} not found");

                existing.Title = dto.Title;
                existing.Description = dto.Description;
                existing.CategoryId = dto.CategoryId;

                await _topicService.UpdateTopicAsync(existing);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating topic");
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid topic ID");

                var topic = await _topicService.GetTopicByIdAsync(id);
                if (topic == null)
                    return NotFound($"Topic with ID {id} not found");

                await _topicService.DeleteTopicAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting topic");
                return Problem();
            }
        }
    }
}