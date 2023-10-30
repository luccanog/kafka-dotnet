using Kafka.Dotnet.Storage.Entities;

namespace Kafka.Dotnet.Storage
{
    public class ReadonlyNoteStorage : IReadonlyStorage<Note>
    {
        private readonly AppDbContext _context;

        public ReadonlyNoteStorage(AppDbContext context)
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
