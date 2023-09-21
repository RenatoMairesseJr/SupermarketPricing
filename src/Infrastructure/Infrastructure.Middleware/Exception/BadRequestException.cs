namespace Infrastructure.Middleware.Exception;

public class BadRequestException : System.Exception
{
    public BadRequestException(string name) : base($"{name}") { }
}
