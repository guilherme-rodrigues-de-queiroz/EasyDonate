using EasyDonate.Application.Interfaces.Security;
using Microsoft.Extensions.Options;

namespace EasyDonate.Persistence.Security;

public class PasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 12;
    private readonly string _pepper;

    public PasswordHasher(IOptions<SecurityOptions> options)
    {
        _pepper = options.Value.Pepper;
    }

    public string Hash(string password)
    {
        var combined = password + _pepper;
        return BCrypt.Net.BCrypt.HashPassword(combined, WorkFactor);
    }

    public bool Verify(string password, string hash)
    {
        var combined = password + _pepper;
        return BCrypt.Net.BCrypt.Verify(combined, hash);
    }
}
