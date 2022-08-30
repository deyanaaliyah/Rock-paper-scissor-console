using System;
using RockPaperScissorsConsole;

namespace RockPaperScissorsConsole // Note: actual namespace depends on the project name.
{
    internal class RockPaperScissorsConsole
    {
        static void Main(string[] args)
        {
            // Run game
            Game g = new Game();
            g.InitializeGame();
        }
    }

    // Enum of choices
    public enum Choice { Rock = 0, Paper = 1, Scissor = 2, Null = 99 }

    // Model:
    public class User
    {
        public string? Name { get; set; } // Name of player
        public Choice MyChoice { get; set; } // Player's choice
        public int Score { get; set; } // Player's score
        public bool IsGame { get; set; } // To determine if player still wants to continue the game
    }

    // The game
    public class Game
    {
        // 1st: Create a randomizer that returns a choice for the AI/computer
        private static Choice ReturnRandomChoice()
        {
            Choice[] ComputersChoice = new[] { Choice.Rock, Choice.Paper, Choice.Scissor };
            Random r = new Random();

            return ComputersChoice[r.Next(0,2)];
        }

        // 2nd: Create a user
        private static void InitializeUser()
        {
            Console.Clear(); // Clears the console
            Console.WriteLine("Hvad er dit navn?");

            // Input of the user equals its name
            User user = new User
            {
                Name = Console.ReadLine(),
                Score = 0,
                IsGame = true
            };

            // Send this user to StartGame(model)
            StartGame(user);
        }

        // 3rd: Initialize the game
        public void InitializeGame()
        {
            // Return guide to play
            Console.WriteLine("Er du klar til at spille mod mig?"
                + "\n\n----------------------------------------"
                + "\n\nTast [J] for at starte spillet \nHUSK: at du kan taste [Å] for at afslutte.");

            // Translate the input into running code
            switch (Console.ReadLine())
            {
                // If player is ready
                case "J":
                case "j":
                    Console.Clear();
                    InitializeUser();
                    break;

                // If player wants to go home
                case "Å":
                case "å":
                    Console.Clear();
                    Console.WriteLine("\nNederent. Du pøvede ikke engang :/");
                    break;

                // Everything else
                default:
                    Console.Clear();
                    Console.WriteLine("\nComputer ikke forstå hvad bruger siger. Prøv igen");
                    InitializeGame();
                    break;
            }
        }
        
        // 4th: Start the game
        private static void StartGame(User user)
        {
            // Create a computer-player.
            string[] ComputerNames = { "John", "Jane", "Taylor" };
            Random r = new Random();

            // Instanciate the computer-player with the given information. This way, the computer's choice is re-instanced
            User computer = new User
            {
                Name = ComputerNames[r.Next(ComputerNames.Length)],
                IsGame = true
            };

            // Continue the game as long the player IsGame.
            do
            {
                // Print guide
                Console.WriteLine("Er du klar, " + user.Name +"? Du kender reglerne." +
                        "\n\n[X] for sten \t[Y] for saks \t[Z] for papir");

                // Save input as a string
                string Input = Console.ReadLine();
                        
                // Manipulate this attribute
                switch (Input)
                {
                    case "X":
                    case "x":
                        user.MyChoice = Choice.Rock; // Sets player's choice to rock
                        break;

                    case "Y":
                    case "y":
                        user.MyChoice = Choice.Scissor; // Sets player's choice to scissor
                        break;

                    case "Z":
                    case "z":
                        user.MyChoice = Choice.Paper; // Sets player's choice to paper
                        break;

                    // If user wants to quit
                    case "Å" :
                    case "å" :
                        user.MyChoice = Choice.Null; // Set player's choice to null
                        computer.MyChoice = Choice.Null; // Set computer's choice to null
                        Console.Clear();
                        GetScore(user, computer); // Get final score
                        Console.WriteLine("\n\nTak for kampen!" +
                                "\n\n\n\nHire me, pls");
                        user.IsGame = false; // Set player's IsGame to false
                        break;

                    // Default
                    default:
                        user.MyChoice = Choice.Null; // Set player's choice to null
                        Console.Clear();
                        Console.WriteLine("Jeg forstår ikke hvad \"" + 
                            Input + "\" betyder. Prøv igen. \n\n");
                        StartGame(user); // Restar the game without deleting user's and/or computer's data
                        break;
                }

                // Add the ælogic
                GameLogic(user, computer);
            }
            while (user.IsGame == true && user.MyChoice != Choice.Null);
        }
        
        // 5th: Add the logic
        private static void GameLogic(User user, User computer)
        {
            // Computer's choice is random everytime.
            computer.MyChoice = ReturnRandomChoice();

            // Draw
            if (computer.MyChoice == user.MyChoice || user.MyChoice == computer.MyChoice)
            {
                GetScore(user, computer);

                Console.WriteLine("******************************* \n" +
                    "Du slog: " + user.MyChoice +
                    "\nJeg slog: " + computer.MyChoice +
                    "\nDet betyder altså, at I slog det samme." +
                    "\n*******************************" );
            }

            // If computer wins
            else if (computer.MyChoice == Choice.Rock && user.MyChoice == Choice.Scissor || 
                computer.MyChoice == Choice.Paper && user.MyChoice == Choice.Rock || 
                computer.MyChoice == Choice.Scissor && user.MyChoice == Choice.Paper)
            {
                computer.Score++; // plus 1, everytime computer wins
                GetScore(user, computer);

                Console.WriteLine("******************************* \n" +
                    "Du slog: " + user.MyChoice +
                    "\nJeg slog: " + computer.MyChoice +
                    "\n\n" + computer.Name + " vinder denne gang!" +
                    "\n*******************************");
            }

            // If user wins
            else if (user.MyChoice == Choice.Rock && computer.MyChoice == Choice.Scissor || 
                user.MyChoice == Choice.Paper && computer.MyChoice == Choice.Rock || 
                user.MyChoice == Choice.Scissor && computer.MyChoice == Choice.Paper)
            {
                user.Score++; // plus 1, everytime user wins
                GetScore(user, computer);

                Console.WriteLine("******************************* \n" +
                    "Du slog: " + user.MyChoice +
                    "\nJeg slog: " + computer.MyChoice +
                    "\n\nOooog " + user.Name + " vinder runden!" +
                    "\n*******************************");
            }
        }
        
        // Create a score-board
        private static void GetScore(User user, User computer)
        {
            // Print score
            Console.Clear();
            Console.WriteLine("+++++++++++++++SCORE+++++++++++++++" +
                "\n\n" + user.Name + ": " + user.Score +
                "\n" + computer.Name + ": " + computer.Score +
                "\n+++++++++++++++++++++++++++++++++++\n\n");
        }
    }
}