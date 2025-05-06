using System;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using GuessingGame_BusinessLogic;
using GuessingGameCommon;
using static System.Formats.Asn1.AsnWriter;

namespace GuessingWordGame

{   // this class handles logic for player gameplays only
    public class Program
    {
        static string[] mainMenuActions = new string[] 
        {
            "[1] Login",
            "[2] Register",
            "[3] Leaderboards", 
            "[4] About me",
            "[5] Exit\n",
        };
        static string[] playerMenu = new string[] 
        { 
            "[1] Start new game",
            "[2] Load game",
            "[3] View points", 
            "[4] Game mechanics",
            "[5] Log out\n" 
        };

        static string[] gameDifficulty = new string[] // coming soon hehe
        {
            "[1] easy",
            "[2] medium",
            "[3] hard",
            "[1] god-mode"
        };

        public static GuessingGameProcess gameProcess = new GuessingGameProcess();

        static void Main(string[] args)
        {
            DisplayWelcomeMessage();

            //string userName = string.Empty;

            bool running = true;
            while (running)
            {
                DisplayMainMenu();

                int optionsInput = GetOptionsInput("Choose an option: ", 1, 5);
                Console.WriteLine();

                switch (optionsInput)
                {   
                    case 1:
                        HandleLogin();
                        break;

                    case 2:
                        HandleRegistration();
                        break;

                    case 3:
                        HandleLeaderBoards();
                        break;
                    
                    case 4:
                        HandleAboutMe();
                        break;

                    case 5:
                        if (ConfirmExit())
                        {
                            running = false;
                        }
                        else
                        {
                            Console.WriteLine("\nReturning to main menu...\n");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please choose from 1 to 5.\n");
                        break;
                }
            }
        }


        static void DisplayWelcomeMessage()
        {
            string gameTitle = ">>> GUESS IT! <<<";
            string welcomeMessage = "Welcome to the Best Word Guessing Game of all time!";
            string separator = "------------------------------------------------";
            string prompt = "Press any key to continue...";
            string loading = "Loading...";
            string pleaseWait = "Please wait...";

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(CenterConsoleText(gameTitle));      
            Console.WriteLine();
            Console.WriteLine(CenterConsoleText(separator));
            Console.WriteLine(CenterConsoleText(welcomeMessage));
            Console.WriteLine(CenterConsoleText(separator));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write(CenterConsoleText(prompt));
            Console.ReadKey();
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(CenterConsoleText(loading));
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine(CenterConsoleText(pleaseWait));
            Thread.Sleep(1700);
            Console.Clear();
        }

        static string CenterConsoleText(string text)
        {
            return text.PadLeft((Console.WindowWidth + text.Length) / 2);
        }

        public static void DisplayMenuOptions(string[] menuOptions)
        {
            foreach (var action in menuOptions)
            {
                Console.WriteLine(action);
            }
        }

        static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine($"===== Main menu =====");
            Console.WriteLine();

            DisplayMenuOptions(mainMenuActions);
        }

        static void DisplayPlayerMenu(string userName)
        {
            Console.Clear();
            Console.WriteLine($"===== Player Menu: {userName} =====");
            Console.WriteLine();

            DisplayMenuOptions(playerMenu);
        }

        public static void HandleLeaderBoards()
        {
            Console.Clear();
            Console.WriteLine("===== Leaderboards =====\n");
            Console.WriteLine("Rank\tUsername\tScore");
            Console.WriteLine("-----------------------------\n");

            HandleLeaderboardContents();

            Console.WriteLine();
            WaitForAcknowledgement();
        }

        public static void HandleLeaderboardContents(bool showEmptyMesssage = true)
        {
            var leaderboard = gameProcess.GetLeaderboard();

            if (leaderboard.Count == 0)
            {
                Console.WriteLine("Nothing to show right now.\n");

                if (showEmptyMesssage)
                { 
                    Console.WriteLine("Play your first game and see how you rank on the leaderboards!\n\n");
                };
            }
            else
            {
                int rank = 1;
                foreach (var entry in leaderboard)
                {
                    Console.WriteLine($"{rank}\t{entry.Key}\t\t{entry.Value}");
                    rank++;
                }
            }
        }

        public static void WaitForAcknowledgement()
        {
            Console.Write("Type \"ok\" to continue: ");
            string input;

            do
            {
                input = Console.ReadLine()?.ToLower();
                if (input != "ok")
                {
                    Console.Write("\nPlease type \"ok\" to continue: ");
                }
            } while (input != "ok");
            Console.WriteLine();
        }

        static void EndGameMessage(string userName)
        {
            int score = gameProcess.GetPlayerScore(userName);

            Console.WriteLine("====================================================");
            Console.WriteLine($"CONGRATULATIONS, {userName}!");
            Console.WriteLine($"You completed all levels!");
            Console.WriteLine($"Your total score: {score} points.");
            Console.WriteLine("You're a Hangman Master!");
            Console.WriteLine("====================================================\n");

            WaitForAcknowledgement();
        }

        static void DisplayLevelInfo(int playerLives, List<char> guessedWord, string userName, string hint, string difficulty, int currentLevel) 
        {           
            int score = gameProcess.GetPlayerScore(userName);

            Console.Clear();
            Console.WriteLine($"===== Guess it!: Word Guessing Game =====");
            Console.WriteLine($"\nLEVEL: {currentLevel}\n");
            Console.WriteLine($"Your lives remaining: {playerLives} | Points: {score}\n");
            Console.WriteLine("Category: IT related terminologies");
            Console.WriteLine($"Difficulty: {difficulty}");
            Console.WriteLine($"\nHint: {hint}");
            Console.WriteLine("\nWord to guess: " + string.Join(" ", guessedWord)); // for better spacing           
        }

        static void ViewPoints(string userName)
        {
            Console.Clear();

            int score = gameProcess.GetPlayerScore(userName);

            Console.WriteLine($"===== Points Summary for {userName} =====\n");
            Console.WriteLine($"Your points: {score}\n");
            Console.WriteLine("======================================\n");
            WaitForAcknowledgement();
        }

        static void HandleLogin()
        {
            Console.Clear();
            Console.WriteLine("===== Login =====\n");
            
            bool loginSuccss = HandleLoginAttempts();

            if (!loginSuccss)
            {
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static bool HandleLoginAttempts()
        {
            int attempts = 0;
            int maximumAttempts = 3;
            string userName = "";
            string passWord = "";           

            while (attempts < maximumAttempts)
            {
                userName = AcceptNonEmptyInput("Enter Username: ");
                passWord = AcceptNonEmptyPassword("Enter Password: ");

                if (gameProcess.VerifyLogin(userName, passWord))
                {
                    DisplayLoggedInMenu(userName);
                    return true;
                }

                if (gameProcess.ValidateAdminLogin(userName, passWord))
                {
                    AdminInterface.DisplayLoggedInAdminMenu(userName);
                    return true;
                }

                Console.WriteLine("\nInvalid username or password. Please try again.\n");
                attempts++;
            }
            Console.WriteLine("Maximum attempts reached. Returning to main menu...\n");
            return false;
        }

        static void HandleRegistration()
        {
            Console.Clear();
            Console.WriteLine("===== Account Registration =====\n");

            string fullName = AcceptNonEmptyInput("Enter your full name: ");
            SaveAccount(fullName);

            WaitForAcknowledgement();
        }

        private static void SaveAccount(string fullName)
        {
            string username = GetUniqueUsername();
            string password = GetValidPassword();

            if (YesNoConfirmation("\nDo you want to save this account? (yes/no): "))
            {
                if (gameProcess.RegisterPlayer(fullName, username, password))
                {
                    Console.WriteLine("\nAccount saved successfully. You can now login to the game.\n");
                }
            }
            else
            {
                Console.WriteLine("\nAccount not saved. Please register again.\n");
            }
        }

        private static string GetUniqueUsername()
        {
            while (true)
            {
                string username = AcceptNonEmptyInput("Enter your desired username: ");
                if (!gameProcess.PlayerExists(username))
                
                    return username;

                Console.WriteLine("\nUsername already exists. Please choose a different username.\n");
                
            }
        }

        private static string GetValidPassword()
        {
            return AcceptNonEmptyPassword("Enter your password: ");
        }

        static void DisplayLoggedInMenu(string userName)
        {
            bool loggedIn = true;

            while (loggedIn)
            {               
                DisplayPlayerMenu(userName);

                int optionsInput = GetOptionsInput("Choose an option: ", 1, 5);
                Console.WriteLine();

                switch (optionsInput)
                {
                    case 1:
                        StartGame(userName);
                        break;
                            
                    case 2:
                        LoadGame(userName);
                        break;
                    
                    case 3:
                        ViewPoints(userName);
                        break;

                    case 4:
                        ShowGameMechanics();
                        break;

                    case 5:
                        if (ConfirmLogout(userName))
                        {
                            Console.WriteLine($"\nLogging out...\n");
                            loggedIn = false;
                        }
                        else
                        {
                            Console.WriteLine($"Logout cancelled. Returning to player menu...\n");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid input! Please choose from 1 to 5\n");
                        break;                         
                }
            }   
        }      

        public static bool ConfirmLogout(string userName)
        {
            return YesNoConfirmation($"Are you sure you want to log out, {userName}? (yes/no): ");
        }

        public static bool ConfirmExit()
        {
            return YesNoConfirmation("Are you sure you want to exit? (yes/no): ");
        }

        public static string AcceptNonEmptyInput(string displayText)
        {
            string userInput;

            do
            {
                Console.Write(displayText);
                userInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Your input CANNOT be empty! Please try again.\n");
                }
            }
            while (string.IsNullOrWhiteSpace(userInput));

            return userInput;
        }

        static string AcceptNonEmptyPassword(string displayText)
        {
            string userInput;

            do
            {
                Console.Write(displayText);
                userInput = ReadPassword()?.Trim();

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Your input CANNOT be empty! Please try again.\n");
                }
            }
            while (string.IsNullOrEmpty(userInput));
            return userInput;
        }

        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (key.Key != ConsoleKey.Enter && !char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        static bool StartGame(string userName)
        {
            IReadOnlyList<WordHint> wordHints = gameProcess.GetWordHints();
            int totalLevel = gameProcess.TotalLevel; 

            Console.Clear();

            for (int level = 0; level < totalLevel; level++)
            {               
                gameProcess.ResetLives();
                gameProcess.ResetWrongGuesses();

                WordHint current = wordHints[level]; 
                string wordToGuess = current.Word;   
                string hint = current.Hint;
                string difficulty = current.Difficulty ?? "medium";

                bool guessedCorrectly = false;

                while (!guessedCorrectly)
                {
                    guessedCorrectly = PlayLevel(
                        userName, 
                        wordToGuess, 
                        hint, 
                        difficulty,
                        currentLevel: level + 1,
                        totalLevel: totalLevel);

                    if (!guessedCorrectly)
                    {
                        bool tryAgain = YesNoConfirmation("Do you want to try again? (yes/no): ");
                        Console.WriteLine();

                        if (!tryAgain)
                        {
                            if (level == 0)
                            {
                                ExitOnFirstTry(userName); // method calling

                            }
                            else
                            {
                                ExitMidGameLosing( userName); // method calling                               
                            }
                            return false; // ← return to main menu
                        }

                        gameProcess.ResetLives(); // Reset lives if they choose to try again
                    }
                }

                if (guessedCorrectly)
                {
                    int earnedPoints = gameProcess.CalculateScoreForLevel();
                    gameProcess.AddToPlayerScore(userName, earnedPoints);

                    gameProcess.UpdatePlayerLevelProgress(userName, level + 1); // for saved game, delete this if it causes load game bugs

                    if (!HandleLevelProgression(userName, level, totalLevel))
                    {
                        return false; // ← return to main menu
                    }
                }
            }

            EndGameMessage(userName); // Game completed successfully
            return false; // After full game completion, return to menu
        }

        static bool PlayLevel(string userName, string wordToGuess, string hint, string difficulty,int currentLevel,int totalLevel)
        {
            List<char> guessedWord = gameProcess.InitializeGuessedWord(wordToGuess);

            while (gameProcess.PlayerLives > 0)
            {
                DisplayLevelInfo(gameProcess.PlayerLives, 
                    guessedWord, 
                    userName, 
                    hint, 
                    difficulty,
                    currentLevel
                    ); // UI method call

                char guessedLetter = GetValidGuess();
                bool correctGuess = gameProcess.ProcessPlayerGuess(wordToGuess, guessedWord, guessedLetter);

                if (!correctGuess)
                {
                    gameProcess.IncrementWrongGuesses();
                    Task.Run(() => Console.Beep(1000, 120));

                }


                if (gameProcess.IsWordGuessed(guessedWord, wordToGuess))
                {
                    return CheckLevelResult(guessedWord, wordToGuess);
                }

            }

            return CheckLevelResult(guessedWord, wordToGuess);
        }

        static bool HandleLevelProgression(string userName, int level, int totalLevel)
        {
            int earnedPoints = gameProcess.CalculateScoreForLevel();
            int wrongGuesses = gameProcess.GetWrongGuessesInLevel();
            int totalPlayerScore = gameProcess.GetPlayerScore(userName);

            Console.WriteLine("You earned 10 points!");
            Console.WriteLine($"Total wrong guesses: {wrongGuesses} | Total points: {totalPlayerScore}");
            Console.WriteLine();

            if (level < totalLevel - 1)
            {
                bool continueGame = YesNoConfirmation("Do you want to continue to the next level? (yes/no): ");
                Console.WriteLine();


                if (!continueGame)
                {
                    int score = gameProcess.GetPlayerScore(userName);

                    Console.WriteLine($"Your total score: {score} points.");
                    Console.WriteLine("Thanks for playing!\n");

                    WaitForAcknowledgement();
                    return false;  // Back to main menu
                }
            }
            return true;  // Continue to next level
        }

        static bool CheckLevelResult(List<char> guessedWord, string wordToGuess)
        {
            if (gameProcess.IsWordGuessed(guessedWord, wordToGuess))
            {
                Console.WriteLine($"Congratulations! You guessed the word: {wordToGuess}\n");
                Console.Beep(1300, 100);
                Console.Beep(1600, 100);

                return true;
            }

            Console.WriteLine("Sorry, you ran out of lives!\n");
            return false;
        }
       
        static void ExitOnFirstTry(string userName)
        {
            Console.WriteLine("You gave up on the first level.");
            Console.WriteLine("Unfortunately, you didn't earn any points.");
            Console.WriteLine($"Better luck next time, {userName}!\n");

            WaitForAcknowledgement();

        }

        static void ExitMidGameLosing(string userName)
        {
            int score = gameProcess.GetPlayerScore(userName);

            Console.WriteLine("You ran out of lives this round — no points added this time.\n");
            Console.WriteLine($"Your current score: {score} points.\n\n");

            WaitForAcknowledgement();
        }

        public static int GetOptionsInput(string displayText, int minOption, int maxOption)
        {
            int userInput;

            while (true)
            {
                Console.Write(displayText);

                if (int.TryParse(Console.ReadLine()?.Trim(), out userInput))
                {
                    if (userInput >= minOption && userInput <= maxOption)
                    {
                        return userInput;
                    }
                    else
                    {
                        Console.WriteLine($"\nInvalid input. Please enter a number between {minOption} and {maxOption}.\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid number.\n");
                }
            }
        }

        static char GetValidGuess()
        {
            while (true)
            {
                Console.Write("\nGuess a letter: ");
                string inputLetter = Console.ReadLine()?.Trim().ToLower();
                Console.WriteLine();

                if (string.IsNullOrEmpty(inputLetter))
                {
                    UserEmptyInput(); // method calling
                    continue;
                }

                if (inputLetter.Length != 1)
                {
                    ReadOnlySingleChar(); // method calling
                    continue;
                }

                if (!char.IsLetter(inputLetter[0]))
                {
                    CheckForLetterInput(); // method calling
                    continue;
                }

                return inputLetter[0];
            }
        }

        public static bool YesNoConfirmation(string displayText)
        {
            string input;

            do
            {
                Console.Write(displayText);
                input = Console.ReadLine()?.Trim().ToLower();

                if (input != "yes" && input != "no")
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.\n");
                }
            }
            while (input != "yes" && input != "no");

            return input == "yes";
        }

        static void UserEmptyInput()
        {
            Console.WriteLine("Your input CANNOT be empty! Please enter a letter.");
            Console.WriteLine();
        }

        static void ReadOnlySingleChar()
        {
            Console.WriteLine("NOT COUNTED! Please enter a single letter only.");
            Console.WriteLine();
        }

        static void CheckForLetterInput()
        {
            Console.WriteLine("OOPS, only letters are acceptable! Any symbols and numbers are not allowed.");
            Console.WriteLine();
        }

        static void ShowGameMechanics()
        {
            Console.Clear();

            Console.WriteLine("Welcome to the Word Guessing Game! Here's everything you need to know to play and enjoy:");

            PrintSection("Objective: ",
                "Guess the hidden word letter by letter before you run out of lives!");
            PrintSection("Game Rules: ",
                "\n- Start with 7 lives\n" +
                "- Each incorrect guess costs 1 life\n" +
                "- Correct guesses reveal letters in the word\n" +
                "- Complete all 50 levels to win");
            PrintSection("Scoring System: ",
                "\n- Base of 10 points for each level\n" +
                "- minus(-) 1 point for each wrong guess in a level\n" +
                "- Points earned = 10 - (number of wrong guesses)\n" +
                "- Track your high score on the leaderboard");
            PrintSection("Hints: ",
                "\n- Each word comes with a helpful hint\n" +
                "- Use hints strategically when stuck!");
            PrintSection("Controls: ",
                "\n- Type single letters to guess\n" +
                "- Use number keys to navigate menus\n");
            Console.WriteLine("Good luck, and enjoy the game!\n\n");
            

            WaitForAcknowledgement();
        }

        static void PrintSection(string title, string content)
        {
            Console.Write($"\n{title}");
            Console.WriteLine(content);
        }

        static void HandleAboutMe()
        {
            Console.Clear();

            PrintAboutSection("Introduction: ",
                "Welcome to the Word Guessing Game, created by me — Jayboy Casubuan." +
                "\nI'm a solo game developer passionate about designing fun and challenging games for players.");

            PrintAboutSection("Background: ",
                "Since childhood, I've dreamed of creating my own game. Now, as an IT student, " +
                "\nI'm taking my first steps toward that goal. I have experience with C#, Java, and problem-solving." +
                "\nThis game reflects my skills in logic, OOP, and multi-layer programming.");

            PrintAboutSection("Game Development Journey: ",
                "I’ve always enjoyed games that challenge the mind. Word Guessing Game was inspired by Hangman — " +
                "\na favorite of mine — but enhanced with levels, hints, and a points system for extra excitement.");

            PrintAboutSection("Contact Me: ",
                "I'd love to hear your feedback! Feel free to reach out through any of the links below:\n" +
                "\nEmail   : jayboy.casubuan.08@gmail.com" +
                "\nGitHub  : github.com/jCasubuan" +
                "\nLinkedIn: linkedin.com/in/jayboy-casubuan");

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("\nThank you for playing Word Guessing Game! Your support means a lot to me.");
            Console.WriteLine("I hope you enjoy the game as much as I enjoyed creating it!\n");

            Console.WriteLine("Word Guessing Game - Est. 2025");
            Console.WriteLine("Copyright (c) 2025 Jayboy Casubuan. All Rights Reserved.\n");

            WaitForAcknowledgement();
        }

        static void PrintAboutSection(string title, string content)
        {
            Console.WriteLine($"\n{title}");
            Console.WriteLine(content);
        }

        static void LoadGame(string userName)
        {
            int lastLevel = gameProcess.GetLastCompletedLevel(userName);
            int score = gameProcess.GetPlayerScore(userName);

            if (lastLevel == 0 && score == 0)
            {
                Console.WriteLine("You haven't played the game yet.");
                Console.WriteLine("Please start a new game first before you can load a previous session.\n");
                WaitForAcknowledgement();

                return;
            }

            if (lastLevel >= gameProcess.TotalLevel)
            {
                Console.WriteLine("You have already completed the game!");
                Console.WriteLine("You can start a new game if you'd like to play again.\n");
                WaitForAcknowledgement();
                return;
            }

            Console.WriteLine($"Welcome back, {userName}!");
            Console.WriteLine($"You left off at Level {lastLevel} with {gameProcess.GetPlayerScore(userName)} points.\n");

            bool continueGame = YesNoConfirmation("Ready to continue? (yes/no): ");

            if (continueGame)
            {
                StartGameFromLevel(userName, lastLevel);
            }
            else
            {
                Console.WriteLine("Returning to player menu...\n");
            }
        }

        static bool StartGameFromLevel(string userName, int startLevel) // variant of StartGame(), for the load game process,
                                                                        // separate from the main flow of the game where all data are fresh
        {
            IReadOnlyList<WordHint> wordHints = gameProcess.GetWordHints();
            int totalLevel = gameProcess.TotalLevel;

            Console.Clear();

            for (int level = startLevel; level < totalLevel; level++)  
            {
                gameProcess.ResetLives();
                gameProcess.ResetWrongGuesses();

                WordHint current = wordHints[level];
                string wordToGuess = current.Word;
                string hint = current.Hint;
                string difficulty = current.Difficulty ?? "medium";

                bool guessedCorrectly = false;

                while (!guessedCorrectly)
                {
                    guessedCorrectly = PlayLevel(
                        userName,
                        wordToGuess,
                        hint,
                        difficulty,
                        currentLevel: level + 1,
                        totalLevel: totalLevel);

                    if (!guessedCorrectly)
                    {
                        bool tryAgain = YesNoConfirmation("Do you want to try again? (yes/no): ");
                        Console.WriteLine();

                        if (!tryAgain)
                        {
                            if (level == startLevel - 1)
                                ExitOnFirstTry(userName);
                            else
                                ExitMidGameLosing(userName);

                            return false;
                        }

                        gameProcess.ResetLives();
                    }
                }

                if (guessedCorrectly)
                {
                    int earnedPoints = gameProcess.CalculateScoreForLevel();
                    gameProcess.AddToPlayerScore(userName, earnedPoints);
                    gameProcess.UpdatePlayerLevelProgress(userName, level); // Save progress

                    if (!HandleLevelProgression(userName, level, totalLevel))
                    {
                        return false;
                    }
                }
            }

            EndGameMessage(userName);
            return false;
        } 



    }
}