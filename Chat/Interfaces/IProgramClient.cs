namespace Chat.Interfaces;

public interface IProgramClient
{
    string username { get; }

    void ProgramClientOptions(string username);
    Task ProgramClient();

}