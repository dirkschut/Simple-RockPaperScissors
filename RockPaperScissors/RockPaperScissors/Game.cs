using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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
    public class Game
    {
        private int enemyScore = -1;
        private int playerScore = -1;

        private bool autoturn = false;

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
                if(!autoturn)
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

            if(playerChoice == -1)
            {
                autoturn = false;
                return;
            }

            int robotChoice = random.Next(0, 3);
            Console.WriteLine($"The player choose {optionNames[playerChoice]} and the robot choose {optionNames[robotChoice]}.");

            if(playerChoice == robotChoice + 1 || playerChoice == robotChoice - 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The Robot won.");
                Console.ForegroundColor = ConsoleColor.White;
                EnemyScore++;
            }
            else if(playerChoice == robotChoice)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("It was a draw.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You won!");
                Console.ForegroundColor = ConsoleColor.White;
                PlayerScore++;
            }
            DisplayScore();
            SaveGame();
        }

        /// <summary>
        /// Get the player choice
        /// </summary>
        /// <returns>Return an integer representation of the choice</returns>
        private int AskPlayerChoice()
        {
            while (true)
            {
                Console.WriteLine("Which do you want to chose: [R]ock, [P]aper, or [S]cissors? ([Q]uit)");
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
                    case ConsoleKey.Q:
                        return -1;
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
                Console.WriteLine("Play Again? [Y]es/[N]o ([A]utoturn)");
                ConsoleKeyInfo input = Console.ReadKey();
                Console.WriteLine();
                switch (input.Key)
                {
                    case ConsoleKey.Y:
                        return true;
                    case ConsoleKey.N:
                        return false;
                    case ConsoleKey.A:
                        autoturn = true;
                        return true;
                    default:
                        Console.WriteLine("I\'m sorry but that was not a valid input. Please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// A very simple file reader.
        /// </summary>
        public void LoadSave()
        {
            if (!File.Exists("rps.xml"))
            {
                Console.WriteLine("No save file found.");
                return;
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            FileStream fileStream = new FileStream("rps.xml", FileMode.Open);
            XmlReader xmlReader = XmlReader.Create(fileStream, settings);

            bool isPlayer = false;
            bool isEnemy = false;

            while (xmlReader.Read())
            {

                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xmlReader.Name == "playerscore")
                            isPlayer = true;
                        else if (xmlReader.Name == "enemyscore")
                            isEnemy = true;
                        break;
                    case XmlNodeType.Text:
                        if (isPlayer)
                        {
                            int.TryParse(xmlReader.Value, out playerScore);
                            isPlayer = false;
                        }
                        else if (isEnemy)
                        {
                            int.TryParse(xmlReader.Value, out enemyScore);
                            isEnemy = false;
                        }
                        break;
                    default:
                        break;
                }
            }
            xmlReader.Close();
            fileStream.Close();
        }

        /// <summary>
        /// Saves the score in a simple XML document.
        /// </summary>
        private void SaveGame()
        {
            FileStream fileStream = new FileStream("rps.xml", FileMode.Create);
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            XmlWriter xmlWriter = XmlWriter.Create(fileStream, xmlWriterSettings);

            xmlWriter.WriteStartElement("score");
            xmlWriter.WriteElementString("playerscore", PlayerScore.ToString());
            xmlWriter.WriteElementString("enemyscore", EnemyScore.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.Flush();
            xmlWriter.Close();
            fileStream.Close();
        }
    }
}
