using Microsoft.Extensions.Hosting;

namespace DependencyInjection
{
    internal class WorkerService : BackgroundService
    {
        private readonly IMessageWriter _messageWriter;

        public WorkerService(IMessageWriter messageWriter) => _messageWriter = messageWriter;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _messageWriter.Write($"Worker Service running at: {DateTimeOffset.Now}");
                await Task.Delay(1000, stoppingToken);
            }

        }
    }
}
