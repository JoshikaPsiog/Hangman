using System;
using logics.model;
using structs.controller;
using System.Collections.Generic;

namespace ui.model
{
    class Hangman
    {
        public void Frontend()
        {
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(playerName))
                playerName = "Player";
            Starting start = new Starting();
start.Startfun("words.csv");

bool keepPlaying = true;

while (keepPlaying)
{
    start.PickRandomWord();
    string wordToGuess = start.choosenword;

    HangmanGame game = new HangmanGame(wordToGuess);

    game.GameEnded += (g) =>
    {
        Console.Clear();
        g.DisplayHangman();
        Console.WriteLine("Word: " + g.DisplayWord());
        if (g.GameStatus == GameStatus.PlayerWon)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  O  ");
            Console.WriteLine(" /|\\ ");
            Console.WriteLine(" / \\ ");
            Console.ResetColor();
            Console.WriteLine($"Congratulations!  {playerName} You guessed the word!");
        }
        else
        {
            Console.WriteLine($"Sorry {playerName}, you lost. The word was: " + g.WordToGuess);
        }
    };

    while (game.GameStatus == GameStatus.Ongoing)
    {
        Console.Clear();
                    Console.WriteLine("Theme is School, Guess the Words, ALL THE BEST");
        Console.WriteLine("Word: " + game.DisplayWord());
        Console.WriteLine("Wrong guesses: " + string.Join(", ", game.WrongGuesses));
        Console.WriteLine($"Attempts left: {game.AttemptsLeft}/3");
        game.DisplayHangman();

        Console.Write("Enter a letter: ");
        char guess = char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();
        game.Guess(guess);
    }

    Console.WriteLine("\nPress Enter to play another random word or type 'exit' to quit...");
    string input = Console.ReadLine();
    if (input.Trim().ToLower() == "exit")
        keepPlaying = false;
}

Console.WriteLine("Game Over!");

        }
    }
}
