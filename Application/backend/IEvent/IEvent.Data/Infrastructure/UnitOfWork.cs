namespace IEvent.Data.Infrastructure
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly IEventContext iEventContext;

    public UnitOfWork(IEventContext iEventContext)
    {
      this.iEventContext = iEventContext;
    }

    public void Commit()
    {
      iEventContext.SaveChanges();
    }

    public async Task CommitAsync()
    {
      await iEventContext.SaveChangesAsync();
    }
  }
}
