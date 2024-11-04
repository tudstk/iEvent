namespace IEvent.Data.Infrastructure
{
  public interface IUnitOfWork
  {
    void Commit();

    Task CommitAsync();
  }
}
