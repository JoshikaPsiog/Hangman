using System;
using System.Collections.Generic;
using System.IO;

namespace logics.model
{
    class Starting
    {
        private List<string> words = new List<string>();        
        private List<string> usedWords = new List<string>();   
        private static Random rnd = new Random();

        public string choosenword { get; private set; }
        public List<string> AllWords => words;

        
        public void Startfun(string csvFilePath)
        {
            Load(csvFilePath);

            if (words.Count == 0)
                throw new InvalidOperationException("No words loaded from the CSV file!");

            PickRandomWord();
        }

      
        private void Load(string csvFilePath)
        {
           
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "model", csvFilePath);

            if (!File.Exists(path))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" CSV file not found at: {path}");
                Console.ResetColor();
                words = new List<string>();
                return;
            }

            words.Clear();

            foreach (var line in File.ReadAllLines(path))
            {
                if (!string.IsNullOrWhiteSpace(line))
                    words.Add(line.Trim());
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Loaded {words.Count} words from CSV file.\n");
            Console.ResetColor();
        }

        public void PickRandomWord()
        {
            if (words.Count == 0)
                throw new InvalidOperationException("Word list is empty!");

            var availableWords = new List<string>(words);
            foreach (var used in usedWords)
                availableWords.Remove(used);
            if (availableWords.Count == 0)
            {
                Console.WriteLine("All words used once — resetting list...");
                usedWords.Clear();
                availableWords = new List<string>(words);
            }

            
            int index = rnd.Next(availableWords.Count);
            choosenword = availableWords[index].ToUpper();

            usedWords.Add(choosenword);
        }
    }
}
