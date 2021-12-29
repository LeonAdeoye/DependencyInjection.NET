namespace DependencyInjection
{
    public class DefaultOperation : ITransientOperation, IScopedOperation, ISingletonOperation
    {
        // Initializes the OperationId property to the last four characters of new globally unique identifier (GUID).
        public string OperationId { get; } = Guid.NewGuid().ToString()[^4..];
    }
}
