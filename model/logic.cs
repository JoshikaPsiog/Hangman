using System;
using System.Collections.Generic;
using System.IO;

namespace logics.model
{
    class Starting
    {
        private List<string> words = new List<string>();        // Stores all words from CSV
        private List<string> usedWords = new List<string>();    // Stores already used words
        private static Random rnd = new Random();

        public string choosenword { get; private set; }
        public List<string> AllWords => words;

        // Entry function
        public void Startfun(string csvFilePath)
        {
            Load(csvFilePath);

            if (words.Count == 0)
                throw new InvalidOperationException("No words loaded from the CSV file!");

            PickRandomWord();
        }

        // Load all words from CSV file
        private void Load(string csvFilePath)
        {
            //  Properly find CSV file in model folder relative to program's run directory
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

        // Pick a random unused word from the list
        public void PickRandomWord()
        {
            if (words.Count == 0)
                throw new InvalidOperationException("Word list is empty!");

            // Filter out used words
            var availableWords = new List<string>(words);
            foreach (var used in usedWords)
                availableWords.Remove(used);

            // ✅ If all words are used, reset
            if (availableWords.Count == 0)
            {
                Console.WriteLine("🔁 All words used once — resetting list...");
                usedWords.Clear();
                availableWords = new List<string>(words);
            }

            // Randomly pick a new word
            int index = rnd.Next(availableWords.Count);
            choosenword = availableWords[index].ToUpper();

            //  Add to used list to prevent repeat
            usedWords.Add(choosenword);
        }
    }
}
