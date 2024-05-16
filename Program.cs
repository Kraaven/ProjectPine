using System;
using System.IO;

namespace ProjectPine
{
    class Program
    {

        static int[] LanguageMemory = {0,0,0,0,0,0,0,0};
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
                            Interpret(line);
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
    
        static void Interpret(string Instruction){
            Console.WriteLine("------");
            string[] KeyWords = Instruction.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            switch (KeyWords[0]){
                case "PUSH":
                    int Num = 0;
                    Num = int.Parse(KeyWords[1]);
                    int Mem = 0;
                    Mem = GetMemoryIndex(KeyWords[2]);
                    if(Mem != -1){
                        Console.WriteLine("Pushed " + Num + $" --> [{Mem}]");
                        LanguageMemory[Mem] = Num;
                        PrintMemory();
                    }
                    else{
                        Console.WriteLine("INVALID SYNTAX");
                        break;
                    }
                    break;
                
                case "COPY":
                    int ML1  = GetMemoryIndex(KeyWords[1]);
                    int ML2 = GetMemoryIndex(KeyWords[2]);

                    if(ML1 == -1 || ML2 == -1){
                        break;
                    }
                    else{
                        LanguageMemory[ML2] = LanguageMemory[ML1];
                        Console.WriteLine($"[{ML1}]"+ " ==> "+ $"[{ML2}]");
                        PrintMemory();
                        break;
                    }
                
                case "PRINT":
                    int ML = GetMemoryIndex(KeyWords[1]);
                    if(ML == -1){
                        break;
                    }
                    else
                    {
                        Console.WriteLine($">> {LanguageMemory[ML]}");
                        PrintMemory();
                    }
                    break;
                
                case "ADD":
                    int MLA1 = GetMemoryIndex(KeyWords[1]);
                    int MLA2 = GetMemoryIndex(KeyWords[2]);

                    if(MLA1 == -1 || MLA2== -1){
                        break;
                    }
                    else {
                        Console.WriteLine($"{LanguageMemory[MLA1]}:[{MLA1}] + {LanguageMemory[MLA2]}:[{MLA2}] --> [{MLA2}]");
                        LanguageMemory[MLA2] += LanguageMemory[MLA1];
                        PrintMemory();
                    }
                    break;

                default:
                    Console.WriteLine("Key Does not exist");
                    break;
            }
        }

        static int GetMemoryIndex(string keycode){
            if(keycode.Length != 4){
                return -1;
            }
            else{
                if(keycode[0] == 'M' && keycode[1] == 'E' && keycode[2] == 'M'){
                    return int.Parse(keycode[3].ToString());
                }
                else{
                    return -1;
                }
            }
        
            return 1;
        }

        static void PrintMemory(){
            Console.WriteLine($"+---+---+---+---+---+---+---+---+\n| {LanguageMemory[0]} | {LanguageMemory[1]} | {LanguageMemory[2]} | {LanguageMemory[3]} | {LanguageMemory[4]} | {LanguageMemory[5]} | {LanguageMemory[6]} | {LanguageMemory[7]} |\n+---+---+---+---+---+---+---+---+");
        }
    }
}
