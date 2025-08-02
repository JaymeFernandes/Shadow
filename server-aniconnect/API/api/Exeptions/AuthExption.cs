namespace Api.Exeptions;

public class AuthExption : Exception
{
    public AuthExption(string message) : base(message) { }
}