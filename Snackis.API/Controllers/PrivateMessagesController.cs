using Microsoft.AspNetCore.Mvc;
using Snackis.Application.DTOs;
using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;

namespace Snackis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateMessagesController : ControllerBase
    {
        private readonly IPrivateMessageService _privateMessageService;
        private readonly ILogger<PrivateMessagesController> _logger;

        public PrivateMessagesController(IPrivateMessageService privateMessageService, ILogger<PrivateMessagesController> logger)
        {
            _privateMessageService = privateMessageService;
            _logger = logger;
        }

        private static PrivateMessageDTO MapToDto(ApplicationPrivateMessage m) => new()
        {
            Id = m.Id,
            Title = m.Title,
            Content = m.Content,
            SentAt = m.SentAt,
            SenderId = m.SenderId,
            ReceiverId = m.ReceiverId,
            SenderName = m.Sender?.UserName ?? "Unknown",
            ReceiverName = m.Receiver?.UserName ?? "Unknown"
        };

        [HttpGet("inbox/{userId}")]
        public async Task<ActionResult<IEnumerable<PrivateMessageDTO>>> GetInbox(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return BadRequest("Invalid user ID");

                var messages = await _privateMessageService.GetInboxAsync(userId);
                return Ok(messages.Select(MapToDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving inbox for user");
                return Problem();
            }
        }
        [HttpGet("sent/{userId}")]
        public async Task<ActionResult<IEnumerable<PrivateMessageDTO>>> GetSent(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return BadRequest("Invalid user ID");

                var messages = await _privateMessageService.GetSentMessagesAsync(userId);
                return Ok(messages.Select(MapToDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sent messages");
                return Problem();
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PrivateMessageDTO>> GetMessageById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid message ID");

                var message = await _privateMessageService.GetByIdAsync(id);
                if (message == null)
                    return NotFound($"Message with ID {id} not found");

                return Ok(MapToDto(message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving private message");
                return Problem();
            }
        }


        [HttpPost]
        public async Task<ActionResult<PrivateMessageDTO>> CreateMessage([FromBody] CreatePrivateMessageDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Message cannot be null");
                if (string.IsNullOrWhiteSpace(dto.Content))
                    return BadRequest("Message content is required");
                if (string.IsNullOrWhiteSpace(dto.SenderId))
                    return BadRequest("SenderId is required");
                if (string.IsNullOrWhiteSpace(dto.ReceiverId))
                    return BadRequest("ReceiverId is required");

                var message = new ApplicationPrivateMessage
                {
                    Title = dto.Title,
                    Content = dto.Content,
                    SenderId = dto.SenderId,
                    ReceiverId = dto.ReceiverId,
                    SentAt = dto.SentAt ?? DateTime.UtcNow
                };

                await _privateMessageService.AddAsync(message);
                var created = await _privateMessageService.GetByIdAsync(message.Id);

                return CreatedAtAction(nameof(GetMessageById), new { id = message.Id }, MapToDto(created!));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating private message");
                return Problem();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreatePrivateMessageDTO dto)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid message ID");
                if (dto == null)
                    return BadRequest("Message cannot be null");
                if (string.IsNullOrWhiteSpace(dto.Content))
                    return BadRequest("Message content is required");

                var existing = await _privateMessageService.GetByIdAsync(id);
                if (existing == null)
                    return NotFound($"Message with ID {id} not found");

                existing.Content = dto.Content;

                await _privateMessageService.UpdateAsync(existing);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating private message");
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid message ID");

                var message = await _privateMessageService.GetByIdAsync(id);
                if (message == null)
                    return NotFound($"Message with ID {id} not found");

                await _privateMessageService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting private message");
                return Problem();
            }
        }
    }
}