// source
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Models;

namespace server.src.Domain.Weather.Notifications.Models;

/// <summary>
/// A notification about specific weather events, triggered by weather reports.
/// </summary>
public class Notification : BaseEntity
{
    /// <summary>
    /// The message or content of the notification.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// The date and time when the notification was sent.
    /// </summary>
    public DateTime SentDate { get; set; }

    /// <summary>
    /// The type of notification, e.g., "Temperature", "Rain", "Wind".
    /// </summary>
    public string NotificationType { get; set; }

    /// <summary>
    /// The ID of the user receiving the notification.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Navigation property to the User receiving this notification.
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// A flag indicating whether the user has read the notification.
    /// </summary>
    public bool IsRead { get; set; }
}