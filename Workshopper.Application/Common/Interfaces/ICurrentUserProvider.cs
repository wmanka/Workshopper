using Workshopper.Application.Common.Models;

namespace Workshopper.Application.Common.Interfaces;

public interface ICurrentUserProvider
{
    CurrentUser? GetCurrentUser();
}