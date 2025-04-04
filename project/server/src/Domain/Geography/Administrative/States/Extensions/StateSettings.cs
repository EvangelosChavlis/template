namespace server.src.Domain.Geography.Administrative.States.Extensions;

public static class StateSettings
{
    public static int NameLength { get; } = 100;
    public static int CodeLength { get; } = 10;
    public static int DescriptionLength { get; } = 500;
    public static int CapitalLength { get; } = 100;
}