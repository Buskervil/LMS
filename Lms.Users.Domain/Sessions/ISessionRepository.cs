namespace Lms.Users.Domain.Sessions;

public interface ISessionRepository
{
    Task<Session?> GetById(Guid id);
    void Add(Session session);
    void Update(Session session);
}