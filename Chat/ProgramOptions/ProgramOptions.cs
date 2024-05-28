using Chat.MainOperations;
using Client.Firestore;
using ClientServer.MainOperations;
using System;
using System.Threading.Tasks;

namespace ClientServer.ProgramOptions
{
    public class ProgramOptions : IProgramOptions
    {
        public async Task Program()
        {
            Console.WriteLine("--------------------------------------------------\n1 - Login\n2 - Register\n--------------------------------------------------\n");
            FirestoreHelper.SetEnvironmentVariable();
            int option = int.Parse(Console.ReadLine());
            
            await (option switch
                    {
                        1 => LoginAndRegister.LoginUserAsync(),
                        2 => LoginAndRegister.RegisterUserAsync(),
                        _ => throw new InvalidOperationException("Invalid option")
                    }
                );
            
        }




    }
}
