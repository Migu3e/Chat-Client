using Chat.MainOperations;
using Client.MongoDB;
using ClientServer.MainOperations;
using System;
using System.Threading.Tasks;
using Chat.Const;

namespace ClientServer.ProgramOptions
{
    public class ProgramOptions : IProgramOptions
    {
        public async Task Program()
        {
            bool exit = false;
    
            while (!exit)
            {
                Console.WriteLine(ConstMasseges.MenuProgramOptions);
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await Login.LoginUserAsync();
                        break;
                    case "2":
                        await Register.RegisterUserAsync();
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}