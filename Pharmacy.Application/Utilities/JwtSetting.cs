namespace Pharmacy.Application.Utilities;

public class JwtSetting
{
    public string? ValidIssuer { get; set; }
    public string? ValidAudience { get; set; }
    public double ExpireOn { get; set; }
    public double RefreshTokenExpireDays { get; set; }
    public string SecretKey { get; set; } = string.Empty;
}
