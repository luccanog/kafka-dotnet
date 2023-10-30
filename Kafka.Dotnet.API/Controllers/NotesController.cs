using Confluent.Kafka;
using Kafka.Dotnet.API.Services;
using Kafka.Dotnet.Storage;
using Kafka.Dotnet.Storage.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.Dotnet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly IReadonlyStorage<Note> _storage;
        private readonly IMessagingService _messagingService;

        public NotesController(IReadonlyStorage<Note> storage, IMessagingService messagingService)
        {
            _storage = storage;
            _messagingService = messagingService;
        }

        [HttpGet]
        public IEnumerable<Note> GetAll()
        {
            return _storage.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Note> Get([FromRoute] Guid id)
        {
            var note = _storage.GetValue(id);

            if (note is null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NoteDTO dto)
        {
            _messagingService.Send(new Note(dto.Content));
            return NoContent();
        }
    }

    public record NoteDTO
    {
        public string Content { get; set; } = string.Empty;
    }
}