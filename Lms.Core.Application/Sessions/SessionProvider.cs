namespace Lms.Core.Application.Sessions;

public interface ISessionProvider
{
    Guid UserId { get; }
}