namespace server.src.Domain.Metrics.LogErrors.Extensions;

public class LogErrorSettings
{
    public static int ErrorLength { get; } = 500;
    public static int InstanceLength { get; } = 500;
    public static int ExceptionTypeLength { get; } = 100;
    public static int StackTraceLength { get; } = 10000;
}