using System;

class Program
{
    static void Main(string[] args)
    {
        Folder home = new Folder("Home", null);

        Folder activeDirectory = home;
        StudySet activeSet = null;

        bool isLearning = true;
        do
        {
            Console.Clear();

            PWD(activeDirectory, activeSet);
            if (activeSet == null)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Create folder");
                Console.WriteLine("2. Create study set");
                Console.WriteLine("3. Delete folder");
                Console.WriteLine("4. Delete study set");
                Console.WriteLine("5. Exit");

                activeDirectory.DisplayContents(6);

                Console.Write("\nOption: ");
                int response = int.Parse(Console.ReadLine()!);

                if (response == 1)
                {
                    Console.Write("Folder name: ");
                    string folderName = Console.ReadLine()!;

                    if (!string.IsNullOrEmpty(folderName))
                    {
                        Folder newFolder = new Folder(folderName, activeDirectory);
                        activeDirectory.AddFolder(newFolder);

                        activeDirectory = newFolder;
                    }
                }
                else if (response == 2)
                {
                    Console.Write("Set name: ");
                    string setName = Console.ReadLine()!;

                    if (!string.IsNullOrEmpty(setName))
                    {
                        StudySet newSet = new StudySet(setName, activeDirectory);
                        activeDirectory.AddSet(newSet);

                        activeSet = newSet;
                    }   
                }
                else if (response == 3)
                {
                    activeDirectory.DisplayDirectories();
                    Console.Write("\nFolder to delete: ");
                    int index = int.Parse(Console.ReadLine()!) - 1;

                    activeDirectory.RemoveFolder(index);
                }
                else if (response == 4)
                {
                    activeDirectory.DisplayStudySets();
                    Console.Write("\nSet to delete: ");
                    int index = int.Parse(Console.ReadLine()!) - 1;

                    activeDirectory.RemoveSet(index);
                }
                else if (response == 5)
                {
                    Folder parentDirectory = activeDirectory.GetParent();

                    if (parentDirectory != null)
                    {
                        activeDirectory = parentDirectory;
                    }
                    else
                    {
                        isLearning = false;
                    }
                }
                else if (response > 5)
                {
                    // Change directories
                    int index = response - 5;

                    if (index > activeDirectory.GetDirectoryCount())
                    {
                        // Study Set
                        index -= activeDirectory.GetDirectoryCount();
                        activeSet = activeDirectory.GetStudySet(index - 1);
                    }
                    else if (index <= activeDirectory.GetDirectoryCount())
                    {
                        // Folder
                        activeDirectory = activeDirectory.GetDirectory(index - 1);
                    }
                }
            }
            else
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Study");
                Console.WriteLine("2. Upload from notes");
                Console.WriteLine("3. Add term");
                Console.WriteLine("4. Delete term");
                Console.WriteLine("5. Display terms");
                Console.WriteLine("6. Exit");

                Console.Write("\nOption: ");
                int response = int.Parse(Console.ReadLine()!);

                if (response == 1)
                {
                    Console.WriteLine("\nSelect a study activity:");
                    Console.WriteLine("1. Flashcards");
                    Console.WriteLine("2. Write");
                    Console.WriteLine("3. Multiple Choice");
                    Console.WriteLine("4. Timed Challenge");
                    Console.WriteLine("5. Test");
                    Console.WriteLine("6. Exit");

                    Console.Write("\nSelect an option: ");
                    string studyOption = Console.ReadLine()!;

                    if (studyOption == "1")
                    {
                        // Flashcard flashcard = new Flashcard(activeSet);
                        // Dictionary<string, bool> results = flashcard.PlaySession();
                        Console.Write("Need to build");
                    }
                    else if (studyOption == "2")
                    {
                        Console.Write("Need to build");
                    }
                    else if (studyOption == "3")
                    {
                        Console.Write("Need to build");
                    }
                    else if (studyOption == "4")
                    {
                        Console.Write("Need to build");
                    }
                    else if (studyOption == "5")
                    {
                        Console.Write("Need to build");
                    }
                    else if (studyOption == "6")
                    {
                        continue;
                    }
                }
                else if (response == 2)
                {
                    activeSet.UploadNotes();
                }
                else if (response == 3)
                {
                    Console.Write("\nTerm: ");
                    string term = Console.ReadLine()!;

                    Console.Write("\nDefinition: ");
                    string definition = Console.ReadLine()!;

                    activeSet.AddTerm(term, definition);
                }
                else if (response == 4)
                {
                    activeSet.DisplayTerms(1);

                    Console.Write("Term to delete [e.g. 4]: ");
                    int removeIndex = int.Parse(Console.ReadLine()!);

                    activeSet.RemoveTerm(removeIndex - 1);
                }
                else if (response == 5)
                {
                    activeSet.DisplayTerms(1);

                    string wait = Console.ReadLine()!;
                }
                else if (response == 6)
                {
                    activeSet = null;
                }
            }
        } while (isLearning);
    }
    static void PWD(Folder activeDirectory, StudySet activeSet)
    {
        List<string> path = new List<string>();

        Console.WriteLine("Current Location:");
        // Get the study set name if one is active
        if (activeSet != null)
        {
            path.Add(activeSet.GetName());
        }

        // Gets the Folder location
        do
        {
            path.Add(activeDirectory.GetName());
            activeDirectory = activeDirectory.GetParent();
        } while (activeDirectory != null);

        string fullPath = string.Join("/", path.ToArray().Reverse());

        Console.WriteLine(fullPath);
    }
}
