using System.Text.RegularExpressions;

namespace server.src.Persistence.Extensions;

public static class PluralizedName
{
    public static string GetPluralizedName<T>()
    {
        string name = typeof(T).Name;

        // Simple pluralization rules (handles common cases)
        if (name.EndsWith("y") && !Regex.IsMatch(name, "[aeiou]y$", RegexOptions.IgnoreCase))
        {
            return Regex.Replace(name, "y$", "ies"); // Role → Roles (already correct), but City → Cities
        }
        else if (name.EndsWith("s") || name.EndsWith("x") || name.EndsWith("z") ||
                 name.EndsWith("sh") || name.EndsWith("ch"))
        {
            return name + "es"; // Match English pluralization
        }
        return name + "s"; // Default rule (Role → Roles)
    }
}