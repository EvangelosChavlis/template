// packages
using System.Globalization;

namespace server.src.Domain.Extensions;

/// <summary>
/// Provides extension methods for DateTime and nullable DateTime types
/// to format them into various local time and date string representations.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Converts a nullable DateTime to local time and formats it as "dd/MM/yyyy HH:mm".
    /// </summary>
    /// <param name="d">The nullable DateTime to format.</param>
    /// <returns>A formatted string representing the local date and time.</returns>
    public static string GetLocalDateTimeString(this DateTime? d)
    {
        return d?.ToLocalTime().ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)!;
    }

    /// <summary>
    /// Converts a nullable DateTime to local time and formats it as "dd/MM/yyyy".
    /// </summary>
    /// <param name="d">The nullable DateTime to format.</param>
    /// <returns>A formatted string representing the local date.</returns>
    public static string GetLocalDateString(this DateTime? d)
    {
        return d?.ToLocalTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)!;
    }

    /// <summary>
    /// Converts a nullable DateTime to local time and formats it as "HH:mm" (time only).
    /// </summary>
    /// <param name="d">The nullable DateTime to format.</param>
    /// <returns>A formatted string representing the local time.</returns>
    public static string GetLocalTimeString(this DateTime? d)
    {
        return d?.ToLocalTime().ToString("HH:mm", CultureInfo.InvariantCulture)!;
    }

    /// <summary>
    /// Converts a nullable DateTime to local time and formats it as "dddd dd/MM/yyyy HH:mm" (full date and time).
    /// </summary>
    /// <param name="d">The nullable DateTime to format.</param>
    /// <returns>A formatted string representing the full local date and time.</returns>
    public static string GetFullLocalDateTimeString(this DateTime? d)
    {
        return d?.ToLocalTime().ToString("dddd dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)!;
    }

    /// <summary>
    /// Converts a nullable DateTime to local time and formats it as "yyyy-MM-dd" (view-friendly date format).
    /// </summary>
    /// <param name="d">The nullable DateTime to format.</param>
    /// <returns>A formatted string representing the local date in a view-friendly format.</returns>
    public static string GetLocalDateStringView(this DateTime? d)
    {
        return d?.ToLocalTime().ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)!;
    }

    // Non-nullable DateTime versions of the same methods

    /// <summary>
    /// Converts a DateTime to local time and formats it as "dd/MM/yyyy HH:mm".
    /// </summary>
    /// <param name="d">The DateTime to format.</param>
    /// <returns>A formatted string representing the local date and time.</returns>
    public static string GetLocalDateTimeString(this DateTime d)
    {
        return d.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts a DateTime to local time and formats it as "dd/MM/yyyy".
    /// </summary>
    /// <param name="d">The DateTime to format.</param>
    /// <returns>A formatted string representing the local date.</returns>
    public static string GetLocalDateString(this DateTime d)
    {
        return d.ToLocalTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts a DateTime to local time and formats it as "HH:mm" (time only).
    /// </summary>
    /// <param name="d">The DateTime to format.</param>
    /// <returns>A formatted string representing the local time.</returns>
    public static string GetLocalTimeString(this DateTime d)
    {
        return d.ToLocalTime().ToString("HH:mm", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts a DateTime to local time and formats it as "dddd dd/MM/yyyy HH:mm" (full date and time).
    /// </summary>
    /// <param name="d">The DateTime to format.</param>
    /// <returns>A formatted string representing the full local date and time.</returns>
    public static string GetFullLocalDateTimeString(this DateTime d)
    {
        return d.ToLocalTime().ToString("dddd dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts a DateTime to local time and formats it as "yyyy-MM-dd" (view-friendly date format).
    /// </summary>
    /// <param name="d">The DateTime to format.</param>
    /// <returns>A formatted string representing the local date in a view-friendly format.</returns>
    public static string GetLocalDateStringView(this DateTime d)
    {
        return d.ToLocalTime().ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts a DateTime to local time and formats it as "dd-MM-yyyy".
    /// </summary>
    /// <param name="d">The DateTime to format.</param>
    /// <returns>A formatted string representing the local date in "dd-MM-yyyy" format.</returns>
    public static string GetLocalDateStringView2(this DateTime d)
    {
        return d.ToLocalTime().ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts a DateTime to its equivalent local time.
    /// </summary>
    /// <param name="d">The DateTime to convert to local time.</param>
    /// <returns>A DateTime object representing the local time.</returns>
    public static DateTime GetLocalDate(this DateTime d)
    {
        return d.ToLocalTime();
    }

    /// <summary>
    /// Converts a DateTime to its equivalent UTC time.
    /// </summary>
    /// <param name="d">The DateTime to convert to UTC.</param>
    /// <returns>A DateTime object representing the UTC time.</returns>
    public static DateTime GetUtcDate(this DateTime d)
    {
        return d.ToUniversalTime();
    }
}
