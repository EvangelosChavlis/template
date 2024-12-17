// source
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Common;

namespace server.src.Application.Includes.Auth;

public class UserIncludes
{
    public static IncludeThenInclude<User>[] GetUsersIncludes()
    {
        return [];
    }
}