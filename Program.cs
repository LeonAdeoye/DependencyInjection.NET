// See https://aka.ms/new-console-template for more information

using DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Creating Worker Service...");

static Task Main(string[] args) => CreateHostBuilder(args).Build().RunAsync();


// The way you add one or multiple IHostedServices into your WebHost or Host is by registering them up through the AddHostedService extension method.
// Since .NET Core 2.0, the framework provides a new interface named IHostedService helping you to easily implement hosted services.
// The basic idea is that you can register multiple background tasks (hosted services) that run in the background while your host is running.

// The IHostedService background task execution is coordinated with the lifetime of the application (host or micro-service).
// You register tasks when the application starts and you have the opportunity to do some graceful action or clean-up when the application is shutting down.

// Without using IHostedService, you could always start a background thread to run any task.
// The difference is precisely at the app's shutdown time when that thread would simply be killed without having the opportunity to run graceful clean-up actions.

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
            services.AddHostedService<WorkerService>()
                .AddScoped<IMessageWriter, ConsoleMessageWriter>() // Register multiple service instances of the4 same service type.
                .AddScoped<IMessageWriter, LoggingMessageWriter>()); 
// The second call to AddScoped overwrites the first one when resolved as IMessageWriter and adds to the first one when
// multiple services are resolved via IEnumerable<IMessageWriter>. Services appears in the order they are registered.
                