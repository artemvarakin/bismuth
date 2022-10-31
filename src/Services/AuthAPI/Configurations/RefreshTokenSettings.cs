namespace AuthAPI.Configurations;

public class RefreshTokenSettings
{
    public const string SectionName = "RefreshTokenSettings";
    public string Secret { get; init; } = null!;
    public int ExpirationPeriodInDays { get; init; }
}