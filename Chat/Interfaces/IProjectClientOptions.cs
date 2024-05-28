namespace ClientServer.ProgramOptions
{
    public interface IProgramClientOptions
    {
        string Username { get; }
        Task ProgramClient();
    }
}