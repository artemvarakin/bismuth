namespace AuthAPI.Configurations;

public class IdTokenSettings
{
    public const string SectionName = "IdTokenSettings";
    public string Secret { get; set; } = null!;
    public int ExpirationPeriodInMinutes { get; set; }
}