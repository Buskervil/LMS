namespace Lms.Core.Application.Connections;

public interface IConnectionStringProvider
{
    string DefaultConnectionString { get; }
}

public class ConnectionStringProvider : IConnectionStringProvider
{
    public string DefaultConnectionString { get; }

    public ConnectionStringProvider(string defaultConnectionString)
    {
        DefaultConnectionString = defaultConnectionString;
    }
}