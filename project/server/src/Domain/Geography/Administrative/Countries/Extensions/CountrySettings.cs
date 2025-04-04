namespace server.src.Domain.Geography.Administrative.Countries.Extensions;

public static class CountrySettings
{
    public static int NameLength { get; } = 100;
    public static int CodeLength { get; } = 5;
    public static int DescriptionLength { get; } = 500;
    public static int CapitalLength { get; } = 100;
    public static int PhoneCodeLength { get; } = 7;
    public static int TLDLength { get; } = 5;
    public static int CurrencyLength { get; } = 5;
}