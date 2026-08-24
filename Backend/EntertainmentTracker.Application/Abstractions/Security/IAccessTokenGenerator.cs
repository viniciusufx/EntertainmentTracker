using EntertainmentTracker.Domain.Entities;

namespace EntertainmentTracker.Application.Abstractions.Security
{
    public interface IAccessTokenGenerator
    {
        string Generate(User user);
    }
}