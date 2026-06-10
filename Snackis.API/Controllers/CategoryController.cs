using Microsoft.AspNetCore.Mvc;
using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Snackis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        private static CategoryDTO MapToDto(ApplicationCategory AppCat) => new()
        {
            Id = AppCat.Id,
            Name = AppCat.Name,
            Description = AppCat.Description
        };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories.Select(MapToDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving categories");
                return Problem();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid category ID");

                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                    return NotFound($"Category with ID {id} not found");

                return Ok(MapToDto(category));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving category");
                return Problem();
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create([FromBody] CreateCategoryDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Category cannot be null");
                if (string.IsNullOrWhiteSpace(dto.Name))
                    return BadRequest("Category name is required");

                var newCategory = new ApplicationCategory
                {
                    Name = dto.Name,
                    Description = dto.Description
                };

                await _categoryService.CreateCategoryAsync(newCategory);
                return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Id }, MapToDto(newCategory));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                return Problem();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDTO dto)
        {
            try
            {
                if (id <= 0) 
                    return BadRequest("Invalid category ID");
                if (dto == null)
                    return BadRequest("Category cannot be null");
                if (dto.Id != id)
                    return BadRequest("Category ID mismatch");
                if (string.IsNullOrWhiteSpace(dto.Name)) 
                    return BadRequest("Category name is required");

                var existing = await _categoryService.GetCategoryByIdAsync(id);
                if (existing == null)
                    return NotFound($"Category with ID {id} not found");

                existing.Name = dto.Name;
                existing.Description = dto.Description;

                await _categoryService.UpdateCategoryAsync(existing);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category");
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid category ID");

                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                    return NotFound($"Category with ID {id} not found");

                await _categoryService.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category");
                return Problem();
            }
        }
    }
}