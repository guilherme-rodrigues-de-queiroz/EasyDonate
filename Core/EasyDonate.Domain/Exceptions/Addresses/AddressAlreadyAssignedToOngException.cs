namespace EasyDonate.Domain.Exceptions.Addresses;

public sealed class AddressAlreadyAssignedToOngException : EasyDonateDomainException
{
    public AddressAlreadyAssignedToOngException() : base("Este endereço já está associado a uma ONG.") { }
}