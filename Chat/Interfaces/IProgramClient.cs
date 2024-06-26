namespace Chat.Interfaces;

public interface IProgramClient
{

    void ProgramClientOptions(string username);
    Task ProgramClient();

}