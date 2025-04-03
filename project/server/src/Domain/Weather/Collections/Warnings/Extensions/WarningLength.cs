namespace server.src.Domain.Weather.Collections.Warnings.Extensions;

public class WarningLength
{
    public static int NameLength { get; } = 100;
    public static int RecommendedActionsLength { get; } = 250;
    public static int DescriptionLength { get; } = 500;
    public static int CodeLength { get; } = 5;
}