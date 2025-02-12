namespace TimeScout.Infrastructure.Provider;

public interface ICurrentUserProvider
{
    int? GetUserId();
}
