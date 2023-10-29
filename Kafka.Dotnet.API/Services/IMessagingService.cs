namespace Kafka.Dotnet.API.Services
{
    public interface IMessagingService
    {
        Task SendAsync<T>(T message);
    }
}
