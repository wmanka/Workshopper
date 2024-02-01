namespace Workshopper.Infrastructure.Authentication;

public class JwtOptions
{
    public const string SectionName = "JwtSettings";

    public string Audience { get; init; } = null!;

    public string Issuer { get; init; } = null!;

    public string SigningKey { get; init; } = null!;

    public int TokenExpiration { get; init; }
}