namespace FirelessApi.Domain;

public class Alert : AuditEntity
{
    public Alert()
    {
        Users = new HashSet<User>();
    }
    
    public Region Region { get; set; }
    public Data Data { get; set; }
    public ICollection<User> Users { get; set; }
}