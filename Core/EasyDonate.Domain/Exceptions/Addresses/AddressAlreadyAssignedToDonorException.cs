namespace EasyDonate.Domain.Exceptions.Addresses;

public sealed class AddressAlreadyAssignedToDonorException : EasyDonateDomainException
{
    public AddressAlreadyAssignedToDonorException() : base("Este endereço já está associado a um doador.") { }
}