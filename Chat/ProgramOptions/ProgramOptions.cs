using Client.Firestore;
using ClientServer.MainOperations;

namespace ClientServer.ProgramOptions
{
    public class ProgramOptions
    {
        public async Task Program()
        {
            Console.WriteLine("--------------------------------------------------\n1 - Login\n2 - Register\n--------------------------------------------------\n");
            FirestoreHelper.SetEnvironmentVariable();
            int option = int.Parse(Console.ReadLine());
            string username = null;
            string password;

            switch (option)
            {
                case 1:

                    bool loginCondition = false;

                    while (!loginCondition)
                    {
                        Console.WriteLine("Enter Username");
                        username = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        password = Console.ReadLine();
                        LoginFeature actionLogin = new LoginFeature();
                        loginCondition = LoginFeature.LoginNewUser(username, password);

                        

                    }
                    ProgramClientOptions client = new ProgramClientOptions(username);
                    await client.ProgramClient();

                    break;

                case 2:
                    bool registerCondition = false;
                    while (!registerCondition)
                    {
                        Console.WriteLine("Enter Username");
                        username = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        password = Console.ReadLine();
                        Console.WriteLine("Enter Email");
                        string email = Console.ReadLine();
                        RegisterFeature actionRegister = new RegisterFeature();
                        registerCondition = actionRegister.RegisterNewUser(username, password, email);
                    }
                    break;
            }
        }

    }
}
