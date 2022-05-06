namespace FirelessApi.Domain.Repository;

public interface IUnitOfWork
{
    Task CompleteAsync();
}