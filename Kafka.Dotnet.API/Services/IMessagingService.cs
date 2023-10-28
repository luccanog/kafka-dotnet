namespace Kafka.Dotnet.API.Services
{
    public interface IMessagingService
    {
        void Send<T>(T message);
    }
}
