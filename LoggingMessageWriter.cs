using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DependencyInjection
{
    internal class LoggingMessageWriter : IMessageWriter
    {
        private readonly ILogger<LoggingMessageWriter> _logger;

        public LoggingMessageWriter(ILogger<LoggingMessageWriter> logger) => _logger = logger;

        public void Write(string message) => _logger.LogInformation(message);
    }
}
