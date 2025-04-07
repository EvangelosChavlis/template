namespace server.src.Domain.Auth.Roles.Extensions;

public static class RoleSettings
{
    public static int NameLength { get; } = 100;
    public static int NormalizedName { get; } = 100;
    public static int Description { get; set; } = 250;
}