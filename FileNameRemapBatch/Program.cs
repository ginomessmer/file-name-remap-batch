using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace FileNameRemapBatch
{
    public class Program
    {
        public static Dictionary<string, string> ReplaceMap = new Dictionary<string, string>()
        {
            // old -> new
            { "ü", "ue" },
            { "ä", "ae" },
            { "ö", "oe" }
        };

        static void Main(string[] args)
        {
            WriteLine("File directory to fix:");
            string path = ReadLine();

            // Check whether directory exists
            if (Directory.Exists(path))
            {
                // Get all files
                var files = Directory.GetFiles(path).ToList();

                foreach (var file in files)
                {
                    FileInfo currentFile = new FileInfo(file);
                    foreach (var map in ReplaceMap)
                    {
                        // Check whether name contains chars in remap source
                        if (currentFile.Name.Contains(map.Key))
                        {
                            // Assign new name
                            string newName = currentFile.Name.Replace(map.Key, map.Value);
                            string newPath = $"{currentFile.DirectoryName}\\{newName}";

                            // Do it
                            WriteLine($"{DateTime.Now.ToShortTimeString()}: Renaming <{currentFile.Name}> to <{newPath}>");
                            currentFile.MoveTo(newPath);
                        }
                    }
                }

                ForegroundColor = ConsoleColor.Green;
                WriteLine("All files have been successfully renamed. Press any key to exit...");
            }
            else
            {
                // Directory doesn't exist -> restart
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"<{path}> doesn't exist. Please restart this application and try again.");
            }

            // Exit
            Read();
        }
    }
}
