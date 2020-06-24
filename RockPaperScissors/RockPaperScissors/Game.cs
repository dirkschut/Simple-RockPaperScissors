using System;
using System.Collections.Generic;
using System.Text;

namespace RockPaperScissors
{
    internal enum Options
    {
        ROCK,
        PAPER,
        SCISSORS
    }

    /// <summary>
    /// Contains the actual game
    /// </summary>
    class Game
    {
        private int enemyScore = -1;
        private int playerScore = -1;

        private Dictionary<int, string> optionNames;
        private Random random;

        public int EnemyScore
        {
            get
            {
                if (enemyScore == -1)
                {
                    enemyScore = 0;
                }

                return enemyScore;
            }
            set { enemyScore = value; }
        }

        public int PlayerScore
        {
            get
            {
                if (playerScore == -1)
                {
                    playerScore = 0;
                }

                return playerScore;
            }
            set { playerScore = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Game()
        {
            optionNames = new Dictionary<int, string>();
            optionNames.Add((int)Options.ROCK, "Rock");
            optionNames.Add((int)Options.PAPER, "Paper");
            optionNames.Add((int)Options.SCISSORS, "Scissors");
            random = new Random();
            //TODO: Load saved score
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        internal void Start()
        {
            DisplayScore();

            bool anotherTurn = true;
            while (anotherTurn)
            {
                DoTurn();
                anotherTurn = AskAgain();
            }
        }

        /// <summary>
        /// Displays the current score in the console
        /// </summary>
        private void DisplayScore()
        {
            Console.WriteLine($"Current Score is Player: {PlayerScore}, Enemy: {EnemyScore}");
        }

        /// <summary>
        /// Gets the choices, compares them, and assigns the new score.
        /// </summary>
        private void DoTurn()
        {
            int playerChoice = AskPlayerChoice();
            int robotChoice = random.Next(0, 3);
            Console.WriteLine($"The player choose {optionNames[playerChoice]} and the robot choose {optionNames[robotChoice]}.");

            if(playerChoice == robotChoice + 1 || playerChoice == robotChoice - 2)
            {
                Console.WriteLine("The Robot won.");
                EnemyScore++;
            }
            else if(playerChoice == robotChoice)
            {
                Console.WriteLine("It was a draw.");
            }
            else
            {
                Console.WriteLine("You won!");
                PlayerScore++;
            }
            DisplayScore();
        }

        /// <summary>
        /// Get the player choice
        /// </summary>
        /// <returns>Return an integer representation of the choice</returns>
        private int AskPlayerChoice()
        {
            while (true)
            {
                Console.WriteLine("Which do you want to chose: [R]ock, [P]aper, or [S]cissors?");
                ConsoleKeyInfo input = Console.ReadKey();
                Console.WriteLine();
                switch (input.Key)
                {
                    case ConsoleKey.R:
                        return (int)Options.ROCK;
                    case ConsoleKey.P:
                        return (int)Options.ROCK;
                    case ConsoleKey.S:
                        return (int)Options.ROCK;
                    default:
                        Console.WriteLine("I\'m sorry but that was not a valid input. Please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Asks the user if they want to play again
        /// </summary>
        /// <returns>Bool value true if they want to play again</returns>
        private bool AskAgain()
        {
            while (true)
            {
                Console.WriteLine("Play Again? [Y]es/[N]o");
                ConsoleKeyInfo input = Console.ReadKey();
                Console.WriteLine();
                switch (input.Key)
                {
                    case ConsoleKey.Y:
                        return true;
                    case ConsoleKey.N:
                        return false;
                    default:
                        Console.WriteLine("I\'m sorry but that was not a valid input. Please try again.");
                        break;
                }
            }
        }
    }
}
