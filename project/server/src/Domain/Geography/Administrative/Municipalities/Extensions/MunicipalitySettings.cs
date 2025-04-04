namespace server.src.Domain.Geography.Administrative.Municipalities.Extensions;

public static class MunicipalitySettings
{
    public static int NameLength { get; } = 100;
    public static int CodeLength { get; } = 30;
    public static int DescriptionLength { get; } = 500;
}
