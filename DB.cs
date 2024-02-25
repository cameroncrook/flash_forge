using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore.V1;

public class DB
{
    private FirestoreDb _db;

    public DB()
    {
        FirestoreDb db = FirestoreDb.Create("flashforge-414918");
        _db = db;
    }

    public async Task<string> GetUserPassword(string username)
    {
        Query user_query = _db.Collection("users").WhereEqualTo("username", username);
        QuerySnapshot snapshot = await user_query.GetSnapshotAsync();

        string password = "";
        foreach (DocumentSnapshot document in snapshot.Documents) {
            if (document.Exists) {
                password = document.GetValue<string>("password");
            }
        }

        return password;
    }

    public async Task CreateUser(string username, string hashedPassword)
    {
        DocumentReference docRef = _db.Collection("users").Document(username);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "username", username },
            { "password", hashedPassword }
        };

        await docRef.SetAsync(user);
    }

    public async Task<string> CreateFolder(string name, string? parentId, string username)
    {
        DocumentReference user = _db.Collection("users").Document(username);

        DocumentReference docRef = _db.Collection("folders").Document();
        Dictionary<string, object> newFolder = new Dictionary<string, object>
        {
            {"user_id", user},
            {"name", name}
        };

        if (parentId != null)
        {
            DocumentReference parent = _db.Collection("folders").Document(parentId);
            newFolder["parent_folder"] = parent;
        }

        await docRef.SetAsync(newFolder);

        return docRef.Id;
    }

    public async Task<string> GetRootFolder(string username)
    {
        Console.WriteLine(username);
        DocumentReference user = _db.Collection("users").Document(username);

        Query root_folder = _db.Collection("folders").WhereEqualTo("user_id", user).WhereEqualTo("name", "root");
        QuerySnapshot snapshot = await root_folder.GetSnapshotAsync();

        string root_id = "";
        if (snapshot.Documents.Count == 0) {
            root_id = await CreateFolder("root", null, username);
        } else {
            foreach (DocumentSnapshot document in snapshot.Documents) {
                root_id = document.Id;
                
                break; 
            }
        }

        return root_id;
    }

    public async Task<Dictionary<string, string>> GetFolder(string targetId)
    {
        DocumentReference targetFolder = _db.Collection("folders").Document(targetId);

        DocumentSnapshot snapshot = await targetFolder.GetSnapshotAsync();
        if (snapshot.Exists)
        {
            string parentId = "";
            if (snapshot.TryGetValue("parent_folder", out DocumentReference referencedParent))
            {
                parentId = referencedParent.Id;
            }

            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"parent_id",  parentId},
                {"folder_id", targetFolder.Id},
                {"name", snapshot.GetValue<string>("name")}
            };

            return data;
        }
        else
        {
            return new Dictionary<string, string>();
        }

    }

    public async Task<Dictionary<string, string>> GetChildrenFolders(string targetId)
    {
        DocumentReference targetFolder = _db.Collection("folders").Document(targetId);

        Query folders = _db.Collection("folders").WhereEqualTo("parent_folder", targetFolder);
        QuerySnapshot snapshot = await folders.GetSnapshotAsync();

        Dictionary<string, string> data = new Dictionary<string, string>();

        foreach(DocumentSnapshot document in snapshot.Documents)
        {
            string name = document.GetValue<string>("name");
            data.Add(name, document.Id);
        }

        return data;
    }

    public async Task<Dictionary<string, string>> GetChildSets(string targetId)
    {
        DocumentReference targetFolder = _db.Collection("folders").Document(targetId);

        Query sets = _db.Collection("study_sets").WhereEqualTo("parent_folder", targetFolder);
        QuerySnapshot snapshot = await sets.GetSnapshotAsync();

        Dictionary<string, string> data = new Dictionary<string, string>();

        foreach(DocumentSnapshot document in snapshot.Documents)
        {
            string name = document.GetValue<string>("name");
            data.Add(name, document.Id);
        }

        return data;
    }

    public async Task<string> CreateSet(string name, string parentId, string username)
    {
        DocumentReference parent = _db.Collection("folders").Document(parentId);
        DocumentReference user = _db.Collection("users").Document(username);

        DocumentReference docRef = _db.Collection("study_sets").Document();
        Dictionary<string, object> newSet = new Dictionary<string, object>
        {
            {"parent_folder", parent},
            {"user_id", user},
            {"name", name}
        };

        await docRef.SetAsync(newSet);

        return docRef.Id;
    }
}