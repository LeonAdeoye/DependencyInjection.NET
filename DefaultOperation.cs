namespace DependencyInjection
{
    public sealed class DefaultOperation : ITransientOperation, IScopedOperation, ISingletonOperation, IDisposable
    {
        // Initializes the OperationId property to the last four characters of new globally unique identifier (GUID).
        public string OperationId { get; } = Guid.NewGuid().ToString()[^4..];

        // The container is responsible for cleanup of types it creates, and calls Dispose on IDisposable instances.
        // Services resolved from the container should never be disposed by the developer.
        // If a type or factory is registered as a singleton, the container disposes the singleton automatically.
        public void Dispose() => Console.WriteLine($"{nameof(DefaultOperation)}.Dispose() for OperationId: {OperationId}");
    }
}
