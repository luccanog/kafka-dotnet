namespace Kafka.Dotnet.API.Storage
{
    public interface IReadonlyStorage<T> where T : class
    {
        public IEnumerable<T> GetAll();

        public T? GetValue(Guid id);
    }
}
