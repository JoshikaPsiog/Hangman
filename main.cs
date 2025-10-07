using System;
using Hangmanadmin.model;
using logics.model;
using ui.model;
class program
{
    public static void Main()
    {
        Console.WriteLine("Are you an User or Admin\n 1.Admin \n 2.Player");
        int num = int.Parse(Console.ReadLine());
        if (num == 1)
        {
            Csv admins = new Csv();
            admins.added();
        }
        else
        {

            Hangman game = new Hangman();
            game.Frontend();


        }
    }
}