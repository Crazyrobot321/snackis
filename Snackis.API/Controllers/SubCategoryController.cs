using Microsoft.AspNetCore.Mvc;
using Snackis.Application.DTOs;
using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;

namespace Snackis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly ILogger<SubCategoryController> _logger;

        public SubCategoryController(ISubCategoryService subCategoryService, ILogger<SubCategoryController> logger)
        {
            _subCategoryService = subCategoryService;
            _logger = logger;
        }

        private static SubCategoryDTO MapToDto(ApplicationSubCategory sc) => new()
        {
            Id = sc.Id,
            Name = sc.Name,
            Description = sc.Description,
            CategoryId = sc.CategoryId,
            Topics = sc.Topics.Select(t => new TopicDTO
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Created = t.Created,
                SubCategoryId = t.SubCategoryId
            }).ToList()
        };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryDTO>>> GetAll()
        {
            try
            {
                var subCategories = await _subCategoryService.GetAllAsync();
                return Ok(subCategories.Select(MapToDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subcategories");
                return Problem();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoryDTO>> GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid subcategory ID");

                var subCategory = await _subCategoryService.GetByIdAsync(id);
                if (subCategory == null)
                    return NotFound($"SubCategory with ID {id} not found");

                return Ok(MapToDto(subCategory));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subcategory");
                return Problem();
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<SubCategoryDTO>>> GetByCategoryId(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                    return BadRequest("Invalid category ID");

                var subCategories = await _subCategoryService.GetByCategoryIdAsync(categoryId);
                return Ok(subCategories.Select(MapToDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subcategories for category");
                return Problem();
            }
        }

        [HttpPost]
        public async Task<ActionResult<SubCategoryDTO>> Create([FromBody] CreateSubCategoryDTO dto)
        {
            try
            {
                if (dto == null) 
                    return BadRequest("SubCategory cannot be null");
                if (string.IsNullOrWhiteSpace(dto.Name)) 
                    return BadRequest("SubCategory name is required");
                if (dto.CategoryId <= 0) 
                    return BadRequest("Valid category ID is required");

                var subCategory = new ApplicationSubCategory
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    CategoryId = dto.CategoryId
                };

                await _subCategoryService.CreateAsync(subCategory);
                return CreatedAtAction(nameof(GetById), new { id = subCategory.Id }, MapToDto(subCategory));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating subcategory");
                return Problem();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateSubCategoryDTO dto)
        {
            try
            {
                if (id <= 0) 
                    return BadRequest("Invalid subcategory ID");
                if (dto == null) 
                    return BadRequest("SubCategory cannot be null");
                if (string.IsNullOrWhiteSpace(dto.Name))
                    return BadRequest("SubCategory name is required");

                var existing = await _subCategoryService.GetByIdAsync(id);
                if (existing == null)
                    return NotFound($"SubCategory with ID {id} not found");

                existing.Name = dto.Name;
                existing.Description = dto.Description;
                existing.CategoryId = dto.CategoryId;

                await _subCategoryService.UpdateAsync(existing);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating subcategory");
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid subcategory ID");

                var existing = await _subCategoryService.GetByIdAsync(id);
                if (existing == null)
                    return NotFound($"SubCategory with ID {id} not found");

                await _subCategoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting subcategory");
                return Problem();
            }
        }
    }
}