namespace server.src.Domain.Support.ChangeLogs.Extensions;

public class ChangeLogSettings
{
    public static int VersionLogLength { get; } = 5;
    public static int NameLength { get; } = 100;
    public static int DescriptionLength { get; } = 500;
}