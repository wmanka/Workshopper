namespace Workshopper.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}