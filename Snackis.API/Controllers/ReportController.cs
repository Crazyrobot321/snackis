using Microsoft.AspNetCore.Mvc;
using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Application.DTOs;

namespace Snackis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportService reportService, ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        private static ReportDTO MapToDto(ApplicationReport r) => new()
        {
            Id = r.Id,
            Reason = r.Reason,
            ReportedAt = r.ReportedAt,
            PostId = r.PostId,
            TopicId = r.TopicId,
            ReporterUserId = r.ReporterUserId ?? string.Empty,
            ReporterUserName = r.Reporter?.UserName ?? "Unknown",
            IsResolved = r.IsResolved
        };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDTO>>> GetAllReports()
        {
            try
            {
                var reports = await _reportService.GetAllReportsAsync();
                return Ok(reports.Select(MapToDto));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting reports");
                return Problem();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDTO>> GetReportById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid report ID");

                var report = await _reportService.GetReportByIdAsync(id);
                if (report == null)
                    return NotFound($"Report with ID {id} not found");

                return Ok(MapToDto(report));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting report by id");
                return Problem();
            }

        }
        [HttpPost]
        public async Task<ActionResult<ReportDTO>> Create([FromBody] CreateReportDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Report cannot be null");
                if (string.IsNullOrWhiteSpace(dto.Reason))
                    return BadRequest("Reason is required");
                if (dto.PostId == null && dto.TopicId == null)
                    return BadRequest("Either PostId or TopicId must be provided");
                var userId = dto.ReporterUserId;
                if (string.IsNullOrWhiteSpace(userId))
                    return Unauthorized();

                var newReport = new ApplicationReport
                {
                    Reason = dto.Reason,
                    PostId = dto.PostId,
                    TopicId = dto.TopicId,
                    ReporterUserId = userId,
                    ReportedAt = DateTime.UtcNow
                };

                await _reportService.CreateReportAsync(newReport);
                return CreatedAtAction(nameof(GetReportById), new { id = newReport.Id }, MapToDto(newReport));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating new report");
                return Problem();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReportDTO dto)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid report ID");
                if (dto == null)
                    return BadRequest("Report cannot be null");
                if (dto.Id != id)
                    return BadRequest("Report ID mismatch");

                var existing = await _reportService.GetReportByIdAsync(id);
                if (existing == null)
                    return NotFound($"Report with ID {id} not found");

                existing.IsResolved = dto.IsResolved;
                existing.Reason = dto.Reason;

                await _reportService.UpdateReportAsync(existing);
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error Updating Report");
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid report ID");

                var report = await _reportService.GetReportByIdAsync(id);
                if (report == null)
                    return NotFound($"Report with ID {id} not found");

                await _reportService.DeleteReportAsync(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error Deleting Report");
                return Problem();
            }
        }
    }
}