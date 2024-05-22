using Client.Firestore;
using Client.Net;
using Google.Cloud.Firestore;
using System.Transactions;
using System.Xml;

namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int Option = 0;

            Console.WriteLine("1 - Login" +
                            "\n2 - Register");

            Option = int.Parse(Console.ReadLine());
            string username;
            string password;
            FirestoreHelper.SetEnviornmentVariable();
            switch (Option)
            {
                case 1:
                    Console.WriteLine("Enter Username");
                    username = Console.ReadLine();
                    Console.WriteLine("Enter Password");
                    password = Console.ReadLine();

                    if (username == "" || password == "")
                    {
                        Console.WriteLine("Error No Fields");
                    }

                    else
                    {

                        var db = FirestoreHelper.database;
                        DocumentReference docRef = db.Collection("UserData").Document(username);
                        UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
                        if (data != null)
                        {
                            if (password == data.Password)
                            {
                                // Fetch the email from Firebase database based on the username
                                // For example:
                                var emaildb = FirestoreHelper.database;
                                DocumentReference docRefEmail = emaildb.Collection("UserData").Document(username);
                                UserData dataEmail = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();

                                if (data != null)
                                {
                                    Console.WriteLine("");
                                }
                                else
                                {
                                    return;
                                }
                                Console.WriteLine("Yes");
                            }
                            else
                            {
                                Console.WriteLine("Password Dosent Much");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Username Dosent Exist");
                        }
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter Username");
                    username = Console.ReadLine();
                    bool condition = true;


                    Console.WriteLine(username);
                    Console.WriteLine("Enter Password");
                    password = Console.ReadLine();
                    Console.WriteLine("Enter Email");
                    string email = Console.ReadLine();



                    break;

            }


        }

    }
}