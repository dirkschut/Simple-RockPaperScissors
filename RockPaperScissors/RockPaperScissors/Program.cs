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
            Console.WriteLine("Welcome to Rock Paper Scissors by Dirk Schut!");
            game = new Game();
            game.Start();
            Console.WriteLine("Thanks for playing!");
            Console.ReadLine();
        }
    }
}
