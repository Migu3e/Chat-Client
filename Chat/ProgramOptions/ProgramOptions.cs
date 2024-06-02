using Chat.MainOperations;
using Client.Firestore;
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
            Console.WriteLine(ConstMasseges.MenuProgramOptions);
            FirestoreHelper.SetEnvironmentVariable();
            int option = int.Parse(Console.ReadLine());
            
            await (option switch
                    {
                        1 => Login.LoginUserAsync(),
                        2 => Register.RegisterUserAsync(),
                        _ => throw new InvalidOperationException("Invalid option")
                    }
                );
            
        }




    }
}
