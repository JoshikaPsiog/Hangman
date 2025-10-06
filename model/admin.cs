using System;
using System.IO;

namespace Hangmanadmin.model
{
    public class Csv
    {

        public void added()
        {
            Console.WriteLine("Enter a word to add:");
            string word = Console.ReadLine();
            if (word == null)
            {
                Console.WriteLine("Cant't add null value");
            }
            else
            {
                File.AppendAllText(@"model\words.csv", word + "\n");
                Console.WriteLine("done sucessfully");
            }

        }

    }
}