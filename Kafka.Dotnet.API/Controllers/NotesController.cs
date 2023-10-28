using Confluent.Kafka;
using Kafka.Dotnet.API.Services;
using Kafka.Dotnet.API.Storage;
using Kafka.Dotnet.API.Storage.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.Dotnet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly IReadonlyStorage<Note> _storage;
        private readonly IMessagingService _messagingService;

        public NotesController(IReadonlyStorage<Note> storage)
        {
            _storage = storage;
        }

        [HttpGet]
        public IEnumerable<Note> GetAll()
        {
            return _storage.GetAll();   
        }

        [HttpGet]
        [Route("{id}")]
        public Note Get([FromRoute] Guid id)
        {
            return _storage.GetValue(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Note note)
        {
            _messagingService.Send(note);
            return NoContent();
        }

    }
}