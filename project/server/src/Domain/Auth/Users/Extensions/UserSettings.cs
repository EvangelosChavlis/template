namespace server.src.Domain.Auth.Users.Extensions;

public static class UserSettings
{
    public static int FirstNameLength { get; } = 50;
    public static int LastNameLength { get; } = 50;
    public static int UserNameLength { get; } = 50;
    public static int PasswordMinLength { get; } = 8;
    public static int PasswordMaxLength { get; } = 64;
    public static int AddressLength { get; } = 100;
    public static int BioLength { get; } = 500;
    public static int MinDateOfBirth { get; } = 18;
}