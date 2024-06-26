using Google.Apis.Http;

namespace Chat.Const;

public class ConstMasseges
{
    public const string EnterUsername = "Enter Username";
    public const string EnterPassword = "Enter Password";
    public const string EnterEmail = "Enter Email";

    
    
    public const string EmptyString = "Cannot Enter Empty Stirng";
    public const string PasswordIsIncorrect = "Password Dosent Match, Password Is Incorrect";
    public const string EmailAlreadyExist = "Email Already Exist, Write A Diffrent Email";
    public const string UsernameDoesntExist = "Username Dosent Exists";
    public const string OneOfFieldEmpty = "One Of The Fields Is Empty";
    
    public const string RegisteredSuccess = "User Registered Successfully";
    public const string LoginSuccess = "Successfully Logged in";
    
    
    public const string UsernameToShort = "Username Is To Short, Use More Then 3 Charecters";
    public const string PasswordNeedsToContainNumbers = "Password needs to contain at least 7 characters and at least one number(or only numbers)";
    public const string PasswordTooShort = "Password needs at least 7 characters";
    public const string MenuProgramOptions = "--------------------------------------------------\n1 - Login\n2 - Register\n--------------------------------------------------\n";
    public const string MenuProgramClient = "--------------------------------------------------\n1 - Connect & Message\n--------------------------------------------------\n";

    public const string ConnectedToServerSuccess = "Successfully Connected To The Server";
    
    public const string EnterFiveDigits = "\n-----------------------------------\nEnter The Full IP digits:\n----->";
    public const string AfterIp = "-----------------------------------\n";

    public const string HelpMessage = "The commands are:\n" +
                                      "/list - Display the connected users\n" +
                                      "/logout - Disconnect from the server\n" +
                                      "/croom <roomname> - Create a new room with the specified name\n" +
                                      "/jroom <roomname> - Join an existing room with the specified name\n" +
                                      "/iroom <username> <roomname> - Invite a user to a specified room\n" +
                                      "/leave - Leave the current room and return to the main room\n" +
                                      "/list rooms - List all rooms and their members\n" +
                                      "/whisper <username> <message> - Send a private message to a specific user\n" +
                                      "/help - Display this list of commands";
    



    public const int ipPort = 1234;
    
    public const string DatabaseConnection = "mongodb+srv://pc:123123gg123123@cluster0.tjadqzu.mongodb.net/";
    public const string DatabaseName = "chats";
    public const string CollectionDataClient = "dataclient";    
    public const string CollectionChats = "chats";

    public const string CreationConditions = "To Create A User It Must Have:\n<-> Username Is At Least 3 Charecters Long\n<-> Password Be At Least 7 Long\n<-> Password Must Inclode At Least One Number\n<-> Email Must Have '@gmail.com'";
}
