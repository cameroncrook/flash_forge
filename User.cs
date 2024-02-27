using BCrypt.Net;
using Google.Cloud.Firestore;

public class User
{
    private DB _db;
    private string _userId;

    public User() 
    {
        _db = new DB();
    }

    public async Task Login() 
    {
        bool validated = false;
        do
        {
            Console.Write("username: ");
            string username = Console.ReadLine();

            Console.WriteLine("");
            Console.Write("password: ");
            string password = Console.ReadLine();

            validated = await Validate(username, password);

            if (validated) 
            {
                _userId = username;
            }

        } while (!validated);

        Console.WriteLine("Logged in...");

        return;
    }

    public async Task<bool> Validate(string username, string password)
    {
        string hashedPassword = await _db.GetUserPassword(username);

        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public async Task CreateUser()
    {
        Console.Write("username: ");
        string username = Console.ReadLine();

        Console.Write("\npassword: ");
        string password = Console.ReadLine();

        string hashedPassword = HashPassword(password);

        await _db.CreateUser(username, hashedPassword);

        _userId = username;
        Console.WriteLine("User Created");
        
        return;
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public string GetUserId()
    {
        return _userId;
    }


    // Folder specific functions
    public async Task<Folder> CreateFolder(string currentFolderId)
    {
        Console.Write("Folder Name: ");
        string folderName = Console.ReadLine();

        string folderId = await _db.CreateFolder(folderName, currentFolderId, _userId);

        Folder newFolder = new Folder(folderName, currentFolderId, folderId);

        return newFolder;
    }

    public async Task<Folder> GetRoot()
    {
        string rootId = await _db.GetRootFolder(_userId);

        Folder root = new Folder("root", null, rootId);

        return root;
    }

    public async Task<Folder> MoveDirectory(string folderId)
    {
        Dictionary<string, string> data = await _db.GetFolder(folderId);

        Folder folder = new Folder(data["name"], data["parent_id"], data["folder_id"]);

        return folder;
    }

    public async Task<List<Dictionary<string, string>>> GetDirectoryContents(string folderId)
    {
        List<Dictionary<string, string>> children = new List<Dictionary<string, string>>();

        Dictionary<string, string> folders = await _db.GetChildrenFolders(folderId);

        foreach (KeyValuePair<string, string> folder in folders)
        {
            Dictionary<string, string> folderData = new Dictionary<string, string> {
                {"name", folder.Key},
                {"id", folder.Value},
                {"type", "folder"}
            };

            children.Add(folderData);
        }

        Dictionary<string, string> sets = await _db.GetChildSets(folderId);

        foreach (KeyValuePair<string, string> set in sets)
        {
            Dictionary<string, string> setData = new Dictionary<string, string> {
                {"name", set.Key},
                {"id", set.Value},
                {"type", "set"}
            };

            children.Add(setData);
        }

        return children;
    }
}