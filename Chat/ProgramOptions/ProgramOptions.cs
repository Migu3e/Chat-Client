using Client.Firestore;

using Chat.MainOperations;

namespace Chat.ProgramOptions
{
    public class ProgramOptions
    {
        public void Program()
        {
            Console.WriteLine("1 - Login\n2 - Register");
            FirestoreHelper.SetEnvironmentVariable();
            int option = int.Parse(Console.ReadLine());
            string username;
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
