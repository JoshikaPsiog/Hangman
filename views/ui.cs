using System;
using System.Collections.Generic;
using System.IO;

namespace ui.views
{
    class Starting
    {
        private List<String> words;
        private Random rnd;
        public string choosenword {  get;private set; }

        public void Startfun(string csvFilePath)
        {
            rnd = new Random();
            load(csvFilePath);
            rndword();
        }
        private void load(string csvFilePath)
        {
            string path = Path.Combine("model", csvFilePath);
            if (!File.Exists(path))
                throw new FileNotFoundException("CSV file not found");

            words = new List<string>();
            foreach (var line in File.ReadAllLines(path))
            {
                if (!string.IsNullOrWhiteSpace(line))
                    words.Add(line.Trim());
            }
        }
        public void rndword()
        {
            if (words.Count == 0)
                throw new InvalidOperationException("Word list is empty");

            int index = rnd.Next(words.Count);
            choosenword = words[index].ToUpper();
            
        }
    }
}

