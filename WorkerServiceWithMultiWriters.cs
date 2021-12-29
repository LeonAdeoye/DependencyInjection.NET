﻿using System.Diagnostics;
using Microsoft.Extensions.Hosting;

namespace DependencyInjection
{
    internal class WorkerServiceWithMultiWriters : BackgroundService
    {
        private readonly IMessageWriter[] _dependencyArray;

        public WorkerServiceWithMultiWriters(IEnumerable<IMessageWriter> messageWriters)
        {
            var dependencyArray = messageWriters.ToArray();
            Trace.Assert(_dependencyArray[0] is ConsoleMessageWriter);
            Trace.Assert(_dependencyArray[1] is LoggingMessageWriter);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _dependencyArray[0].Write($"Worker running at: {DateTimeOffset.Now}");
                _dependencyArray[1].Write($"Worker running at: {DateTimeOffset.Now}");
                await Task.Delay(1000, stoppingToken);
            }

        }
    }
}
