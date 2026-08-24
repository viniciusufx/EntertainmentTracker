namespace EntertainmentTracker.Application.Abstractions.Security
{
    public interface IAuthenticationCookieService
    {
        void SetAccessToken(string token);

        void SetRefreshToken(string token);

        void RemoveAuthenticationCookies();
    }
}