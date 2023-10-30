
using Kafka.Dotnet.Storage.Entities;

namespace Kafka.Dotnet.Storage
{
    public interface IStorage<T> where T : class
    {
        public void Add(Note note);
    }
}

