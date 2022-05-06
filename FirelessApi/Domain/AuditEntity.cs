namespace FirelessApi.Domain;

public abstract class AuditEntity
{
    protected AuditEntity()
    {
        CreatedAt = DateTimeOffset.Now;
        ModifiedAt = DateTimeOffset.Now;
    }

    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
}