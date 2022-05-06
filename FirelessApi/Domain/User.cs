namespace FirelessApi.Domain;

public class User : AuditEntity
{
    public User()
    {
        Alerts = new HashSet<Alert>();
    }
    
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string ConfirmEmail { get; set; }
    public string Name { get; set; }
    public ICollection<Alert> Alerts { get; set; }
}