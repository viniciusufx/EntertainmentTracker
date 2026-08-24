namespace EntertainmentTracker.Domain.Exceptions
{
    public sealed class InvalidRefreshTokenHashException
        : DomainException
    {
        public InvalidRefreshTokenHashException(string message)
            : base(message)
        {
        }
    }
}