using System;
using System.IO;

namespace ProjectPine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"______  _                _____              _         _   ");
            Console.WriteLine(@"| ___ \(_)              /  ___|            (_)       | |  ");
            Console.WriteLine(@"| |_/ / _  _ __    ___  \ `--.   ___  _ __  _  _ __  | |_ ");
            Console.WriteLine(@"|  __/ | || '_ \  / _ \  `--. \ / __|| '__|| || '_ \ | __|");
            Console.WriteLine(@"| |    | || | | ||  __/ /\__/ /| (__ | |   | || |_) || |_ ");
            Console.WriteLine(@"\_|    |_||_| |_| \___| \____/  \___||_|   |_|| .__/  \__|");
            Console.WriteLine(@"                                              | |         ");
            Console.WriteLine(@"                                              |_|         ");
            Console.WriteLine("\n\nWelcome to PineScript. This is the Application that simple runs the Pinescript code.\nThis of this as a me writing a programming language interpreter in C#\n");

            Console.Write("Name of the File: ");
            var fileName = Console.ReadLine();

            if (!fileName.EndsWith(".pn"))
            {
                Console.WriteLine("File is not a PineScript");
                Console.ReadKey(true);
                return;
            }
            else
            {
                if (File.Exists(fileName))
                {
                    Console.WriteLine($"Reading File {fileName}");

                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }
                    }
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("File does not exist");
                    Console.ReadKey(true);
                }

                return;
            }
        }
    }
}
