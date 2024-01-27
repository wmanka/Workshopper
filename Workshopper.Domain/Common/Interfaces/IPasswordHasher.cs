namespace Workshopper.Domain.Common.Interfaces;

public interface IPasswordHasher
{
    public string HashPassword(string password);

    bool IsCorrectPassword(string password, string hash);
}