namespace BuildingBlocks.Exceptions;

public class BadRequestException : Exception
{
    public string Details { get; set; } = string.Empty;
    public BadRequestException(string message, string details) : base(message)
    {
        Details = details;
    }
    public BadRequestException(string name, object key) : base($"Entity \"{name}\" ({key})")
    {

    }
}
