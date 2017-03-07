using FileNameRemapBatch.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace FileNameRemapBatch
{
    public class Program
    {
        static void Main(string[] args)
        {
            Setup();

            var mapping = JsonConvert.DeserializeObject<List<Remap>>(File.ReadAllText("mapping.json"));
            Run(mapping);
        }

        private static void Setup()
        {
            if(!File.Exists("mapping.json"))
            {
                var remap = new List<Remap>()
                {
                    new Remap("ä", "ae"),
                    new Remap("ö", "oe"),
                    new Remap("ü", "ue"),
                    new Remap("ß", "ss")
                };

                var remapJson = JsonConvert.SerializeObject(remap);
                File.WriteAllText("mapping.json", remapJson, System.Text.Encoding.UTF8);
            }
        }

        private static void Run(List<Remap> mapping)
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
                    foreach (var map in mapping)
                    {
                        // Check whether name contains chars in remap source
                        if (currentFile.Name.Contains(map.FromChar))
                        {
                            // Assign new name
                            string newName = currentFile.Name.Replace(map.FromChar, map.ToChar);
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
