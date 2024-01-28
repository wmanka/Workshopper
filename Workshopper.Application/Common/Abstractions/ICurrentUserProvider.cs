using Workshopper.Application.Common.Models;

namespace Workshopper.Application.Common.Abstractions;

public interface ICurrentUserProvider
{
    CurrentUser? GetCurrentUser();
}