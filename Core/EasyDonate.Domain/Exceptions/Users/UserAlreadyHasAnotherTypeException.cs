namespace EasyDonate.Domain.Exceptions.Users;

public sealed class UserAlreadyHasAnotherTypeException : EasyDonateDomainException
{
    public UserAlreadyHasAnotherTypeException() : base("Usuário já possui outro tipo.") { }
}