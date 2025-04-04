namespace server.src.Domain.Metrics.AuditLogs.Extensions;

public class AuditLogSettings
{
    public static int IPAddressLength { get; } = 500;
    public static int ReasonLength { get; } = 500;
    public static string AdditionalMetadataType { get; } = "jsonb";
    public static string BeforeValuesType { get; } = "jsonb";
    public static string AfterValuesType { get; } = "jsonb";
}