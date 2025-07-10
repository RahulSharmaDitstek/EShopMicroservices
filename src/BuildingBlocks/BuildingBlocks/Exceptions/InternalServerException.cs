namespace BuildingBlocks.Exceptions;

public class InternalServerException : Exception
{
    public string Details { get; set; } = string.Empty;
    public InternalServerException(string message, string details) : base(message)
    {
        Details = details;
    }
    public InternalServerException(string name, object key) : base($"Entity \"{name}\" ({key})")
    {

    }
}
