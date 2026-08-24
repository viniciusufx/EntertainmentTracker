namespace EntertainmentTracker.Domain.ValueObjects;

public sealed record UserId(Guid Value)
{
    public static UserId Create()
    {
        return new UserId(Guid.NewGuid());
    }
}