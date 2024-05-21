using System;
using System.Diagnostics;
using System.IO;

namespace ProjectPine
{
    class Program
    {

        static int[] LanguageMemory = {0,0,0,0,0,0,0,0};
        static bool Debug;

        static string[] lines;
        static int PC;
        static async Task Main(string[] args)
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

                    lines = await File.ReadAllLinesAsync(fileName);
                    PC = 0;

                    while(PC != lines.Length){
                        if(Debug){Console.WriteLine($"Program Counter: {PC}");}
                        Interpret(lines[PC]);
                        PC++;
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
            string[] KeyWords = Instruction.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


            switch (KeyWords[0]){
                
                case "DEBUG":
                    if(KeyWords[1] == "TRUE"){
                        Debug = true;
                    }
                    else{
                        Debug = false;
                    }
                    break;


                case "PUSH":
                    int Num = 0;
                    Num = int.Parse(KeyWords[1]);
                    int Mem = 0;
                    Mem = GetMemoryIndex(KeyWords[2]);
                    if(Mem != -1){

                        if(Debug){Console.WriteLine(">> " + Num + $" --> [{Mem}]");}
                        LanguageMemory[Mem] = Num;
                        //PrintMemory();
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
                        if(Debug){Console.WriteLine($">> [{ML1}]"+ " ==> "+ $"[{ML2}]");}

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
                        //PrintMemory();
                    }
                    break;
                
                case "ADD":
                    int MLA1 = GetMemoryIndex(KeyWords[1]);
                    int MLA2 = GetMemoryIndex(KeyWords[2]);

                    if(MLA1 == -1 || MLA2== -1){
                        break;
                    }
                    else {
                        if(Debug){Console.WriteLine($">> {LanguageMemory[MLA1]}:[{MLA1}] + {LanguageMemory[MLA2]}:[{MLA2}] --> [{MLA2}]");}
                        LanguageMemory[MLA2] += LanguageMemory[MLA1];
                        //PrintMemory();
                    }
                    break;

                case "MUL":
                    int MLM1 = GetMemoryIndex(KeyWords[1]);
                    int MLM2 = GetMemoryIndex(KeyWords[2]);

                    if(MLM1 == -1 || MLM2== -1){
                        break;
                    }
                    else {
                        if(Debug){Console.WriteLine($">> {LanguageMemory[MLM1]}:[{MLM1}] * {LanguageMemory[MLM2]}:[{MLM2}] --> [{MLM2}]");}
                        LanguageMemory[MLM2] *= LanguageMemory[MLM1];
                        //PrintMemory();
                    }
                    break;
                case "SUB":
                    int MLS1 = GetMemoryIndex(KeyWords[1]);
                    int MLS2 = GetMemoryIndex(KeyWords[2]);

                    if(MLS1 == -1 || MLS2== -1){
                        break;
                    }
                    else {
                        if(Debug){Console.WriteLine($">> {LanguageMemory[MLS1]}:[{MLS1}] - {LanguageMemory[MLS2]}:[{MLS2}] --> [{MLS2}]");}
                        LanguageMemory[MLS2] -= LanguageMemory[MLS1];
                        //PrintMemory();
                    }
                    break;
                
                case "SHOW":
                    PrintMemory();
                    break;

                case "IF":
                    int MLJ1 = GetMemoryIndex(KeyWords[1]);
                    int MLJ2 = GetMemoryIndex(KeyWords[3]);

                    if(CheckCondition(KeyWords[2], MLJ1, MLJ2)){
                        PC = int.Parse(KeyWords[5].ToString()) - 2;
                    }
                    else{
                        if(KeyWords[7] == "PASS"){break;}
                        else{PC = int.Parse(KeyWords[7].ToString()) - 2 ;}
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

        static bool CheckCondition(string condition, int memIndex1, int memIndex2)
        {
            switch (condition)
            {
                case "MORE":
                    return LanguageMemory[memIndex1] > LanguageMemory[memIndex2];
                case "EQUALS":
                    return LanguageMemory[memIndex1] == LanguageMemory[memIndex2];
                default:
                    Console.WriteLine("Invalid Condition");
                    return false;
                }
            }
        }
    }

