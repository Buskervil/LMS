using Lms.Users.Application.Core;

namespace Lms.Users.Application.Sessions.LoginByPassword;

public sealed class LoginByPasswordCommand : ICommand<Guid>
{
    public string Login { get; }
    public string Password { get; }
    
    public LoginByPasswordCommand(string login, string password)
    {
        Login = login;
        Password = password;
    }
}