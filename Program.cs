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
                int response = int.Parse(Console.ReadLine());

                if (response == 1)
                {
                    Console.Write("Folder name: ");
                    string folderName = Console.ReadLine();

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
                    string setName = Console.ReadLine();

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
                    int index = int.Parse(Console.ReadLine()) - 1;

                    activeDirectory.RemoveFolder(index);
                }
                else if (response == 4)
                {
                    activeDirectory.DisplayStudySets();
                    Console.Write("\nSet to delete: ");
                    int index = int.Parse(Console.ReadLine()) - 1;

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
                Console.WriteLine("4. Edit term");
                Console.WriteLine("5. Delete term");
                Console.WriteLine("6. Display terms");
                Console.WriteLine("7. Exit");

                Console.Write("\nOption: ");
                int response = int.Parse(Console.ReadLine());

                if (response == 2)
                {
                    activeSet.UploadNotes();
                }
                else if (response == 6)
                {
                    activeSet.DisplayTerms(1);
                }
                else if (response == 7)
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
