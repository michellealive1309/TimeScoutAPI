using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TimeScout.Infrastructure.Provider;

public class CurrentUser : ICurrentUserProvider
{
    private readonly int? _currentUserId;
    public CurrentUser(IHttpContextAccessor accessor)
    {
        var userId = accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return;
        }

        _currentUserId = int.TryParse(userId, out var parsedUserId) ? parsedUserId : null;
    }

    public int? GetUserId()
    {
        return _currentUserId;
    }
}
