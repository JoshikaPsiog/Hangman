using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Hangmanadmin.model
{
    public class Csv
    {
        string filePath = @"model\words.csv";

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Add a new word");
                Console.WriteLine("2. Update an existing word");
                Console.WriteLine("3. Delete a word");
                Console.WriteLine("4. View all words");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddWord(); break;
                    case "2": UpdateWord(); break;
                    case "3": DeleteWord(); break;
                    case "4": ViewWords(); break;
                    case "5": return;
                    default: Console.WriteLine("Invalid choice! Try again."); break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        
        public void AddWord()
        {
            Console.Write("Enter a word to add: ");
            string word = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(word))
            {
                Console.WriteLine("❌ Can't add null or empty value.");
                return;
            }

            File.AppendAllText(filePath, word + Environment.NewLine);
            Console.WriteLine("Word added successfully!");
        }

        public void UpdateWord()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found!");
                return;
            }

            List<string> words = File.ReadAllLines(filePath).ToList();
            ViewWords();

            Console.Write("\nEnter the word to update: ");
            string oldWord = Console.ReadLine()?.Trim();

            if (!words.Contains(oldWord, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine("Word not found in list!");
                return;
            }

            Console.Write("Enter the new word: ");
            string newWord = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(newWord))
            {
                Console.WriteLine("Invalid new word!");
                return;
            }

            int index = words.FindIndex(w => w.Equals(oldWord, StringComparison.OrdinalIgnoreCase));
            words[index] = newWord;

            File.WriteAllLines(filePath, words);
            Console.WriteLine($"'{oldWord}' updated to '{newWord}' successfully!");
        }

        // ❌ Delete a word
        public void DeleteWord()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found!");
                return;
            }

            List<string> words = File.ReadAllLines(filePath).ToList();
            ViewWords();

            Console.Write("\nEnter the word to delete: ");
            string deleteWord = Console.ReadLine()?.Trim();

            if (!words.Remove(deleteWord))
            {
                Console.WriteLine("Word not found!");
                return;
            }

            File.WriteAllLines(filePath, words);
            Console.WriteLine($"'{deleteWord}' deleted successfully!");
        }

        // 👀 View all words
        public void ViewWords()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine(" No words file found!");
                return;
            }

            string[] words = File.ReadAllLines(filePath);
            if (words.Length == 0)
            {
                Console.WriteLine("No words available!");
                return;
            }

            Console.WriteLine("\nWORD LIST:");
            for (int i = 0; i < words.Length; i++)
                Console.WriteLine($"{i + 1}. {words[i]}");
        }
    }
}
