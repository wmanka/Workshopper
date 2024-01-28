namespace Workshopper.Application.Common.Abstractions;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}