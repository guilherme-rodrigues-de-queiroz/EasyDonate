using EasyDonate.Domain.Exceptions.Addresses;

namespace EasyDonate.Domain.Entities;

public sealed class Address : BaseEntity
{
    public string? Cep { get; set; }
    public string? StreetAddress { get; set; }
    public string? Number { get; set; }
    public string? Complement { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? GoogleLocation { get; set; }

    public int? DonorId { get; private set; }
    public Donor? Donor { get; private set; }

    public int? OngId { get; private set; }
    public Ong? Ong { get; private set; }

    public void AssignToDonor(int donorId)
    {
        if (OngId is not null)
            throw new AddressAlreadyAssignedToOngException();

        DonorId = donorId;
    }

    public void AssignToOng(int ongId)
    {
        if (DonorId is not null)
            throw new AddressAlreadyAssignedToDonorException();

        OngId = ongId;
    }
}
