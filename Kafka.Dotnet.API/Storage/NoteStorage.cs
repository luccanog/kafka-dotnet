using Kafka.Dotnet.API.Storage.Entities;

namespace Kafka.Dotnet.API.Storage
{
    public class NoteStorage : IReadonlyStorage<Note>
    {
        private readonly AppDbContext _context;

        public NoteStorage(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Note> GetAll()
        {
            return _context.Notes.AsEnumerable();
        }

        public Note? GetValue(Guid id)
        {
           return _context.Notes.Find(id);
        }
    }
}
