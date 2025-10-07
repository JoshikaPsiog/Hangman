using System;
using System.Collections.Generic;
using System.Linq;

namespace structs.controller
{
    public delegate void GameStatusDelegate(HangmanGame game);

    public enum GameStatus
    {
        Ongoing,
        PlayerWon,
        PlayerLost
    }

    public class HangmanGame
    {
        public string WordToGuess { get; private set; }
        public List<char> CorrectGuesses { get; private set; }
        public List<char> WrongGuesses { get; private set; }
        public int AttemptsLeft { get; private set; } = 3;
        public GameStatus GameStatus { get; private set; } = GameStatus.Ongoing;

        public event GameStatusDelegate GameEnded;

        public HangmanGame(string wordToGuess)
        {
            WordToGuess = wordToGuess.ToUpper();
            CorrectGuesses = new List<char>();
            WrongGuesses = new List<char>();
        }

        public void Guess(char character)
        {
            character = char.ToUpper(character);

            if (WordToGuess.Contains(character))
            {
                if (!CorrectGuesses.Contains(character))
                {
                    CorrectGuesses.Add(character);
                    AttemptsLeft = 3;
                }
                   

            }
            else
            {
                if (!WrongGuesses.Contains(character))
                {
                    WrongGuesses.Add(character);
                    AttemptsLeft--;
                }
            }

            CheckGameStatus();
        }

        private void CheckGameStatus()
        {
            if (WordToGuess.All(c => CorrectGuesses.Contains(c)))
            {
                GameStatus = GameStatus.PlayerWon;
                GameEnded?.Invoke(this);
            }
            else if (AttemptsLeft <= 0)
            {
                GameStatus = GameStatus.PlayerLost;
                GameEnded?.Invoke(this);
            }
        }

        public string DisplayWord()
        {
            return string.Join(" ", WordToGuess.Select(c => CorrectGuesses.Contains(c) ? c : '_'));
        }

        public void DisplayHangman()
        {
            switch (AttemptsLeft)
            {
                case 3:
                    //Console.WriteLine("  +---+");
                    //Console.WriteLine("  |   |");
                    //Console.WriteLine("      |");
                    //Console.WriteLine("      |");
                    //Console.WriteLine("      |");
                    //Console.WriteLine("      |");
                    //Console.WriteLine("=========");
                    break;

                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    Console.ResetColor();
                    break;

                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine(" /|\\  |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    Console.ResetColor();
                    break;

                case 0:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine(" /|\\  |");
                    Console.WriteLine(" / \\  |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    Console.WriteLine("💀 The Hangman died!");
                    Console.ResetColor();
                    break;
            }


        }

        public void Reset(string newWord)
        {
            WordToGuess = newWord.ToUpper();
            CorrectGuesses.Clear();
            WrongGuesses.Clear();
            AttemptsLeft = 3;
            GameStatus = GameStatus.Ongoing;
        }
    }
}
