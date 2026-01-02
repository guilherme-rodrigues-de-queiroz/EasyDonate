using EasyDonate.Domain.Enums;
using EasyDonate.Domain.Exceptions.Users;

namespace EasyDonate.Domain.Entities;

public sealed class User : BaseEntity
{
    public bool Active { get; set; } = true;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public EUserType UserType { get; private set; } = EUserType.None;
    public DateTimeOffset ConsentDate { get; set; } = DateTimeOffset.UtcNow;

    public void AssignAsDonor()
    {
        if (UserType != EUserType.None)
            throw new UserAlreadyHasAnotherTypeException();

        UserType = EUserType.Donor;
    }

    public void AssignAsOng()
    {
        if (UserType != EUserType.None)
            throw new UserAlreadyHasAnotherTypeException();

        UserType = EUserType.Ong;
    }

    public void AssignAsAdmin()
    {
        if (UserType != EUserType.None)
            throw new UserAlreadyHasAnotherTypeException();

        UserType = EUserType.Admin;
    }
}
