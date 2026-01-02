namespace EasyDonate.Domain.Exceptions;

public class EasyDonateDomainException : Exception
{
    protected EasyDonateDomainException(string message) : base(message) { }
}