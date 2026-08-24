namespace EntertainmentTracker.Application.Abstractions.Security
{
    public interface IJweService
    {
        string Encrypt(string token);

        string Decrypt(string encryptedToken);
    }
}