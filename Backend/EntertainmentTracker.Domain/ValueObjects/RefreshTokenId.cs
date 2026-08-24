namespace EntertainmentTracker.Domain.ValueObjects
{
    public sealed record RefreshTokenId(Guid Value)
    {
        public static RefreshTokenId Create()
        {
            return new RefreshTokenId(Guid.NewGuid());
        }
    }
}