using System;

class Program
{
    static void Main(string[] args)
    {
        Folder home = new Folder("Home", null);

        Directory activeDirectory = home;

        Console.WriteLine(activeDirectory.GetType());

        bool isLearning = true;
        do
        {
            Console.Clear();

            if (activeDirectory.GetType() == typeof(Folder))
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Create folder");
                Console.WriteLine("2. Create study set");
                Console.WriteLine("3. Delete folder");
                Console.WriteLine("4. Delete study set");
                Console.WriteLine("5. Exit");

                activeDirectory.DisplayContents(6);

                int response = int.Parse(Console.ReadLine());

                if (response == 1)
                {
                    Console.Write("Folder name: ");
                    string folderName = Console.ReadLine();

                    if (!string.IsNullOrEmpty(folderName))
                    {
                        Folder newFolder = new Folder(folderName, activeDirectory);
                    }
                }
            }
            else if (activeDirectory.GetType() == typeof(StudySet))
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Study");
                Console.WriteLine("2. Add term");
                Console.WriteLine("3. Edit term");
                Console.WriteLine("4. Delete term");
                Console.WriteLine("5. Display terms");
                Console.WriteLine("6. Exit");

                int response = int.Parse(Console.ReadLine());
            }
        } while (isLearning);
    }
    static void PWD()
    {
        Console.WriteLine("Current Location");
    }
}
