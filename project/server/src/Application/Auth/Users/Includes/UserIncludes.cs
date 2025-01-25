// source
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Common;

namespace server.src.Application.Auth.Users.Includes;

public class UserIncludes
{
    public static IncludeThenInclude<User>[] GetUsersIncludes()
    {
        return [];
    }
}