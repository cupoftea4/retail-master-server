namespace RetailMaster.Authorization;

public class JwtSettings
{
    public string Key { get; set; } // Secret key used for signing the JWT
    public string Issuer { get; set; } // The issuer of the JWT
    public string Audience { get; set; } // The audience of the JWT
}