namespace SportSquad.Core.Interfaces;

public interface IUnitOfWork
{
    Task<bool> Commit();
}