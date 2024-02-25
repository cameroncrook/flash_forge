using System;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Clear();
        User user = new User();
        Console.WriteLine("1. Create user");
        Console.WriteLine("2. Login");

        Console.Write("\nOption: ");
        string userAction = Console.ReadLine();

        if (userAction == "1")
        {
            await user.CreateUser();
        } 
        else if (userAction == "2")
        {
            await user.Login();
        }
        else {
            return;
        }

        Folder activeDirectory = await user.GetRoot();
        StudySet activeSet = null;
        bool isStudyingSet = false;

        bool isLearning = true;
        do
        {
            Console.Clear();

            // PWD(activeDirectory, activeSet);
            activeDirectory.DisplayData();
            if (activeSet == null)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Create folder");
                Console.WriteLine("2. Create study set");
                Console.WriteLine("3. Delete folder");
                Console.WriteLine("4. Delete study set");
                Console.WriteLine("5. Exit");

                // activeDirectory.DisplayContents(6);

                Console.Write("\nOption: ");
                int response = int.Parse(Console.ReadLine()!);

                if (response == 1)
                {
                    activeDirectory = await user.CreateFolder(activeDirectory.GetFolderId());
                    // Console.Write("Folder name: ");
                    // string folderName = Console.ReadLine()!;

                    // if (!string.IsNullOrEmpty(folderName))
                    // {
                    //     Folder newFolder = new Folder(folderName, activeDirectory);
                    //     activeDirectory.AddFolder(newFolder);

                    //     activeDirectory = newFolder;
                    // }
                }
        //         else if (response == 2)
        //         {
        //             Console.Write("Set name: ");
        //             string setName = Console.ReadLine()!;

        //             if (!string.IsNullOrEmpty(setName))
        //             {
        //                 StudySet newSet = new StudySet(setName, activeDirectory);
        //                 activeDirectory.AddSet(newSet);

        //                 activeSet = newSet;
        //             }   
        //         }
        //         else if (response == 3)
        //         {
        //             activeDirectory.DisplayDirectories();
        //             Console.Write("\nFolder to delete: ");
        //             int index = int.Parse(Console.ReadLine()!) - 1;

        //             activeDirectory.RemoveFolder(index);
        //         }
        //         else if (response == 4)
        //         {
        //             activeDirectory.DisplayStudySets();
        //             Console.Write("\nSet to delete: ");
        //             int index = int.Parse(Console.ReadLine()!) - 1;

        //             activeDirectory.RemoveSet(index);
        //         }
                else if (response == 5)
                {
                    if (activeDirectory.GetParentId() != null)
                    {
                        activeDirectory = await user.MoveDirectory(activeDirectory.GetParentId()!);
                    }
                    else
                    {
                        isLearning = false;
                    }
                }
        //         else if (response > 5)
        //         {
        //             // Change directories
        //             int index = response - 5;

        //             if (index > activeDirectory.GetDirectoryCount())
        //             {
        //                 // Study Set
        //                 index -= activeDirectory.GetDirectoryCount();
        //                 activeSet = activeDirectory.GetStudySet(index - 1);
        //             }
        //             else if (index <= activeDirectory.GetDirectoryCount())
        //             {
        //                 // Folder
        //                 activeDirectory = activeDirectory.GetDirectory(index - 1);
        //             }
        //         }
            }
        //     else
        //     {
        //         if (isStudyingSet)
        //         {
        //             if (activeSet.GetTermCount() < 1)
        //             {
        //                 Console.WriteLine("No items to study in this set yet.");
        //                 Console.WriteLine("Add study items to study");
        //                 Console.ReadLine();
        //                 isStudyingSet = false;
        //             }
        //             else
        //             {
        //                 Console.WriteLine("\nSelect a study activity:");
        //                 Console.WriteLine("1. Flashcards");
        //                 Console.WriteLine("2. Write");
        //                 Console.WriteLine("3. Multiple Choice");
        //                 Console.WriteLine("4. Test");
        //                 Console.WriteLine("5. View progress");
        //                 Console.WriteLine("6. Exit");


        //                 // settings
        //                 bool studySetting = activeSet.GetStudySetting();
        //                 string setting = "";
        //                 if (studySetting)
        //                 {
        //                     setting = "TERM";
        //                 }
        //                 else
        //                 {
        //                     setting = "DEFINITION";
        //                 }
        //                 Console.WriteLine("\nSETTINGS:");
        //                 Console.WriteLine($"7. Answer with: {setting}");

        //                 Console.Write("\nSelect an option: ");
        //                 string studyOption = Console.ReadLine()!;

        //                 if (studyOption == "1")
        //                 {
        //                     Flashcard flashcard = new Flashcard(activeSet);
        //                     Dictionary<string, bool> results = flashcard.PlaySession();

        //                     activeSet.UpdateTermClassifactions(results);
        //                 }
        //                 else if (studyOption == "2")
        //                 {
        //                     Write write = new Write(activeSet);
        //                     Dictionary<string, bool> results = write.PlaySession();

        //                     activeSet.UpdateTermClassifactions(results);
        //                 }
        //                 else if (studyOption == "3")
        //                 {
        //                     MultipleChoice multipleChoice = new MultipleChoice(activeSet);
        //                     Dictionary<string, bool> results = multipleChoice.PlaySession();

        //                     activeSet.UpdateTermClassifactions(results);
        //                 }
        //                 else if (studyOption == "4")
        //                 {
        //                     Console.WriteLine("NOTE: the results of this test will not effect mastery of terms.");
        //                     Console.Write("Press 'Enter' to begin...");
        //                     Console.ReadLine();

        //                     Test test = new Test(activeSet);
        //                     test.PlaySession();
        //                 }
        //                 else if (studyOption == "5")
        //                 {
        //                     activeSet.DisplayClassification();
        //                     Console.ReadLine();
        //                 }
        //                 else if (studyOption == "7")
        //                 {
        //                     if (studySetting)
        //                     {
        //                         activeSet.UpdateStudySetting(false);
        //                     }
        //                     else
        //                     {
        //                         activeSet.UpdateStudySetting(true);
        //                     }
        //                 }
        //                 else
        //                 {
        //                     isStudyingSet = false;
        //                 }
        //             }
        //         }
        //         else
        //         {
        //             Console.WriteLine("\nSelect an option:");
        //             Console.WriteLine("1. Study");
        //             Console.WriteLine("2. Upload from notes");
        //             Console.WriteLine("3. Add term");
        //             Console.WriteLine("4. Delete term");
        //             Console.WriteLine("5. Display terms");
        //             Console.WriteLine("6. Exit");

        //             Console.Write("\nOption: ");
        //             int response = int.Parse(Console.ReadLine()!);

        //             if (response == 1)
        //             {
        //                 isStudyingSet = true;
        //             }
        //             else if (response == 2)
        //             {
        //                 activeSet.UploadNotes();
        //             }
        //             else if (response == 3)
        //             {
        //                 Console.Write("\nTerm: ");
        //                 string term = Console.ReadLine()!;

        //                 Console.Write("\nDefinition: ");
        //                 string definition = Console.ReadLine()!;

        //                 activeSet.AddTerm(term, definition);
        //             }
        //             else if (response == 4)
        //             {
        //                 activeSet.DisplayTerms(1);

        //                 Console.Write("Term to delete [e.g. 4]: ");
        //                 int removeIndex = int.Parse(Console.ReadLine()!);

        //                 activeSet.RemoveTerm(removeIndex - 1);
        //             }
        //             else if (response == 5)
        //             {
        //                 activeSet.DisplayTerms(1);

        //                 string wait = Console.ReadLine()!;
        //             }
        //             else if (response == 6)
        //             {
        //                 activeSet = null;
        //             }
        //         }
        //     }
        } while (isLearning);
    }
    // static void PWD(Folder activeDirectory, StudySet activeSet)
    // {
    //     List<string> path = new List<string>();

    //     Console.WriteLine("Current Location:");
    //     // Get the study set name if one is active
    //     if (activeSet != null)
    //     {
    //         path.Add(activeSet.GetName());
    //     }

    //     // Gets the Folder location
    //     do
    //     {
    //         path.Add(activeDirectory.GetName());
    //         activeDirectory = activeDirectory.GetParent();
    //     } while (activeDirectory != null);

    //     string fullPath = string.Join("/", path.ToArray().Reverse());

    //     Console.WriteLine(fullPath);
    // }
}
