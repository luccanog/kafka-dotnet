using Kafka.Dotnet.Storage.Entities;

namespace Kafka.Dotnet.Storage
{
    public class NoteStorage : IStorage<Note>
    {
        private readonly AppDbContext _context;

        public NoteStorage(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
        }
    }
}
