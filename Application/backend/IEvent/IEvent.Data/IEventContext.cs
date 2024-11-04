namespace IEvent.Data
{
  using IEvent.Data.Entities;
  using Microsoft.EntityFrameworkCore;

  public class IEventContext : DbContext
  {
    public IEventContext(DbContextOptions<IEventContext> options) : base(options)
    {

    }

    public DbSet<TestEntity> TestEntities { get; set; }
  }
}
