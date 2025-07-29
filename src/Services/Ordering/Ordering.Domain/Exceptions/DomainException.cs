namespace Ordering.Domain.Exceptions;

public class DomainException(string message, string nameof) : Exception($"Domain Exception: \"{message}\" throws from Domain Layer in {nameof}.")
{

}
