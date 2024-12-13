using Microsoft.Extensions.Options;
using ToDoList.Application.Abstractions.Providers;
using ToDoList.Infrastructure.Options;

namespace ToDoList.Infrastructure.Providers;

public class RefreshSessionOptionsProvider : IRefreshSessionOptionsProvider
{
    private readonly IOptionsMonitor<RefreshSessionOptions> _options;

    public RefreshSessionOptionsProvider(IOptionsMonitor<RefreshSessionOptions> options)
    {
        _options = options;
    }

    public int GetExpireInDays()
    {
        return _options.CurrentValue.ExpiresInDays;
    }
}