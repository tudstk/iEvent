namespace IEvent.Data.Entities
{
  public class TestEntity
  {
    public int Id { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;
  }
}
