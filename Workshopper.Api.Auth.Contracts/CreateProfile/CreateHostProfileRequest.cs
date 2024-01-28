namespace Workshopper.Api.Auth.Contracts.CreateProfile;

public record CreateHostProfileRequest(string FirstName, string LastName, string? Title, string? Company, string? Bio);