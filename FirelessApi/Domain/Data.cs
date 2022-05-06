namespace FirelessApi.Domain;

public class Data : AuditEntity
{
    public Data()
    {
        Alerts = new HashSet<Alert>();
    }
    
    public double AtmCo { get; set; }
    public decimal Temperature { get; set; }
    public int Humidity { get; set; }
    public decimal BarometicPressure { get; set; }
    public ICollection<Alert> Alerts { get; set; }
}