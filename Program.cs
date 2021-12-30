// See https://aka.ms/new-console-template for more information

using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Net;
using DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Creating Worker Service...");

// The way you add one or multiple IHostedServices into your WebHost or Host is by registering them up through the AddHostedService extension method.
// Since .NET Core 2.0, the framework provides a new interface named IHostedService helping you to easily implement hosted services.
// The basic idea is that you can register multiple background tasks (hosted services) that run in the background while your host is running.

// The IHostedService background task execution is coordinated with the lifetime of the application (host or micro-service).
// You register tasks when the application starts and you have the opportunity to do some graceful action or clean-up when the application is shutting down.

// Without using IHostedService, you could always start a background thread to run any task.
// The difference is precisely at the app's shutdown time when that thread would simply be killed without having the opportunity to run graceful clean-up actions.

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        //services.AddHostedService<WorkerService>()
        services.AddHostedService<WorkerServiceWithMultiWriters>()
            .AddScoped<IMessageWriter, ConsoleMessageWriter>() // Register multiple service instances of the4 same service type.
            .AddScoped<IMessageWriter, LoggingMessageWriter>()
            .AddTransient<ITransientOperation, DefaultOperation>() // Transient operations are always different, a new instance is created with every retrieval of the service.
            .AddScoped<IScopedOperation, DefaultOperation>() // Scoped operations change only with a new scope, but are the same instance within a scope.
            .AddSingleton<ISingletonOperation, DefaultOperation>() // Singleton operations are always the same, a new instance is only created once.
            .AddTransient<OperationLogger>()).Build();

// Creates an IHostBuilder instance with the default binder settings.
// Configures services and adds them with their corresponding service lifetime.
// Calls Build() and assigns an instance of IHost.
// Calls ExemplifyScoping, passing in the IHost.Services.

static void ExemplifyScoping(IServiceProvider services, string scope)
{
    using IServiceScope serviceScope = services.CreateScope();
    var provider = serviceScope.ServiceProvider;
    var logger = provider.GetRequiredService<OperationLogger>();
    logger.LogOperations($"{scope}-Call 1 . GetRequiredService<OperationLogger>()");
    Console.WriteLine("..."); 
    logger = provider.GetRequiredService<OperationLogger>();
    logger.LogOperations($"{scope}-Call 1 . GetRequiredService<OperationLogger>()");
    Console.WriteLine();
}
ExemplifyScoping(host.Services, "Scope 1");
ExemplifyScoping(host.Services, "Scope 2");
await host.RunAsync();

