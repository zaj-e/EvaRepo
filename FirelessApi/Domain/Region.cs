namespace FirelessApi.Domain;

public class Region : AuditEntity
{
    public Region()
    {
        Alerts = new HashSet<Alert>();
    }
    
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public ICollection<Alert> Alerts { get; set; }
}