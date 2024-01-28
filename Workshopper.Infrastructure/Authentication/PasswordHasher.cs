using System.Text.RegularExpressions;
using Workshopper.Application.Common.Abstractions;

namespace Workshopper.Infrastructure.Authentication;

public partial class PasswordHasher : IPasswordHasher
{
    private readonly static Regex PasswordRegex = StrongPasswordRegex();

    public string HashPassword(string password)
    {
        if (!PasswordRegex.IsMatch(password))
        {
            throw new Exception("Password too weak");
        }

        return
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool IsCorrectPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
    }

    [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled)]
    private static partial Regex StrongPasswordRegex();
}