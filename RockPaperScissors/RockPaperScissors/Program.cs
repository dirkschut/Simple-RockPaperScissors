using System;

namespace RockPaperScissors
{
    class Program
    {
        private static Game game;


        /// <summary>
        /// Application entrypoint
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Welcome to Rock Paper Scissors by Dirk Schut!");
            game = new Game();
            game.LoadSave();
            game.Start();
            Console.WriteLine("Thanks for playing!");
            Console.ReadLine();
        }
    }
}
