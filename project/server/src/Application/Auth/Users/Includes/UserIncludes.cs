// source
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.Users.Includes;

public class UserIncludes
{
    public static IncludeThenInclude<User>[] GetUsersIncludes()
    {
        return [];
    }
}