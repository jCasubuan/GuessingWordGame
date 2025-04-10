
using System;
using GuessingGame_BusinessLogic;

namespace GuessingWordGame
{
    public class Program
    {
        static string[] mainMenuActions = new string[] { "[1] Login", "[2] Register", "[3] Leaderboards", "[4] Exit\n", };
        static string[] playerMenu = new string[] { "[1] Start new game", "[2] View total points", "[3] Log out\n" };
        static bool accountSaved = false;

        static void Main(string[] args)
        {
            DisplayWelcomeMessage();

            //string userName = string.Empty;

            bool running = true;
            while (running)
            {
                DisplayMainMenu();

                int optionsInput = GetOptionsInput("Choose an option: ");
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
                        if (ConfirmExit())
                        {
                            Console.WriteLine("\nExiting the game...");
                            running = false;
                        }
                        else
                        {
                            Console.WriteLine("\nReturning to main menu...\n");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please choose from 1 to 4.\n");
                        break;
                }
            }
        }

        static void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to my C# version of Hangman. Hope you like it!");
            Console.WriteLine("_______________________________________________________\n");
        }

        static void DisplayMainMenu()
        {           

            foreach (var action in mainMenuActions)
            {
                Console.WriteLine(action);
            }
               
        }

        static void HandleLogin()
        {
            Console.WriteLine("=====Login=====");
            string userName = AcceptNonEmptyInput("Enter Username: ");
            string passWord = AcceptNonEmptyInput("Enter Password: ");
            Console.WriteLine($"\nWelcome, {userName}! Let's start the game.\n");

            DisplayLoggedInMenu(userName);
        }

        static void DisplayLoggedInMenu(string userName)
        {
            bool loggedIn = true;

            while (loggedIn)
            {
                
                DisplayPlayerMenu(userName);

                int optionsInput = GetOptionsInput("Choose an option: ");
                Console.WriteLine();

                switch (optionsInput)
                {
                    case 1:
                        StartGame(userName);
                        break;
                    
                    case 2:
                        ViewTotalPoints(userName);
                        break;

                    case 3:
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
                        Console.WriteLine("Invalid input! Please choose from 1 to 3\n");
                        break;  
                       
                }
            }   
        }

        static void DisplayPlayerMenu(string userName)
        {
            Console.WriteLine($"=====Player Menu: {userName}=====\n");

            foreach (var action in playerMenu)
            {
                Console.WriteLine(action);
            }
         
        }

        static bool ConfirmLogout(string userName)
        {
            return YesNoConfirmation($"Are you sure you want to log out, {userName}? (yes/no): ");
        }

        static void ViewTotalPoints(string userName)
        {
            Console.WriteLine($"=====Points Summary for {userName}=====\n");
            Console.WriteLine($"Your total points: {GuessingGameProcess.Points}");
            Console.WriteLine("======================================\n");
            WaitForAcknowledgement();
        }

        static bool StartGame(string userName)
        {
            string[] wordsToGuess = GuessingGameProcess.WordToGuess; // property from business logic
            int totalLevel = GuessingGameProcess.TotalLevel; // property from business logic
            int points = GuessingGameProcess.Points; // property from business logic

            for (int level = 0; level < totalLevel; level++)
            {               
                Console.WriteLine($"LEVEL {level + 1}/{totalLevel}");

                GuessingGameProcess.ResetLives();

                string wordToGuess = wordsToGuess[level];
                bool guessedCorrectly = false;

                while (!guessedCorrectly)
                {
                    guessedCorrectly = PlayLevel(userName, wordToGuess); // method calling

                    if (!guessedCorrectly)
                    {
                        bool tryAgain = YesNoConfirmation("Do you want to try again? (yes/no): ");
                        Console.WriteLine();

                        if (!tryAgain)
                        {
                            if (level == 0)
                            {
                                ExitOnFirstTry(points, userName); // method calling
                                
                            }
                            else
                            {
                                ExitMidGameLosing(points); // method calling                               
                            }
                                return false; // ← return to main menu
                            }

                        GuessingGameProcess.ResetLives(); // Reset lives if they choose to try again
                    }
                }

                if (guessedCorrectly)
                {
                    if (!HandleLevelProgression(ref points, level, totalLevel))
                    {
                            return false; // ← return to main menu
                    }
                }
            }

            EndGameMessage(userName); // Game completed successfully
            return false; // After full game completion, return to menu
        }

        static int GetOptionsInput(string displayText)
        {
            int userInput;
            do
            {
                Console.Write(displayText);
                if (int.TryParse(Console.ReadLine()?.Trim(), out userInput))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid number.\n");
                }
            } while (true);
            return userInput;
        }

        static string AcceptNonEmptyInput(string displayText)
        {
            string userInput;
            do
            {
                Console.Write(displayText);
                userInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Your input CANNOT be empty! Please try again.\n");
                }
            }
            while (string.IsNullOrEmpty(userInput));

            return userInput;
        }

        public static bool PlayLevel(string userName, string wordToGuess)
        {
            char[] guessedWord = GuessingGameProcess.InitializeGuessedWord(wordToGuess);

            while (GuessingGameProcess.PlayerLives > 0)
            {
                DisplayLevelInfo(GuessingGameProcess.PlayerLives, guessedWord);

                char guessedLetter = GetValidGuess();
                bool correctGuess = GuessingGameProcess.ProcessPlayerGuess(wordToGuess, guessedWord, guessedLetter);

                if (GuessingGameProcess.IsWordGuessed(guessedWord, wordToGuess))
                {
                    return CheckLevelResult(guessedWord, wordToGuess);
                }

                if (GuessingGameProcess.PlayerLives > 0)
                {
                    HandleGuessFeedback(correctGuess);
                }
            }

            return CheckLevelResult(guessedWord, wordToGuess);
        }

        public static void HandleGuessFeedback(bool correctGuess)
        {
            if (correctGuess)
            {
                Console.WriteLine("Correct guess! Keep going!\n");
            }
            else
            {
                Console.WriteLine("INCORRECT GUESS! Please try again.\n");
            }
        }

        public static bool CheckLevelResult(char[] guessedWord, string wordToGuess)
        {
            if (GuessingGameProcess.IsWordGuessed(guessedWord, wordToGuess))
            {
                Console.WriteLine($"Congratulations! You guessed the word: {wordToGuess}");
                return true;
            }

            Console.WriteLine("Sorry, you ran out of lives!\n");
            return false;  
        }

        public static void DisplayLevelInfo(int playerLives, char[] guessedWord)
        {
            Console.WriteLine($"Your lives remaining: {playerLives} | Points: {GuessingGameProcess.Points}");
            Console.WriteLine($"Word to guess: {new string(guessedWord)}\n");
        }

        public static char GetValidGuess()
        {
            while (true)
            {
                Console.Write("Guess a letter: ");
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

        public static void UserEmptyInput()
        {
            Console.WriteLine("Your input CANNOT be empty! Please enter a letter.");
            Console.WriteLine();
        }

        public static void ReadOnlySingleChar()
        {
            Console.WriteLine("NOT COUNTED! Please enter a single letter only.");
            Console.WriteLine();
        }

        public static void CheckForLetterInput()
        {
            Console.WriteLine("OOPS, only letters are acceptable! Any symbols and numbers are not allowed.");
            Console.WriteLine();
        }

        public static void ExitOnFirstTry(int points, string userName)
        {
            Console.WriteLine("You gave up on the first level.");
            Console.WriteLine($"Your total score: {points} points.");
            Console.WriteLine($"Better luck next time, {userName}!\n");
            WaitForAcknowledgement();
        }

        public static void ExitMidGameLosing(int points)
        {
            Console.WriteLine("You have decided to end the game midway, but why?");
            Console.WriteLine($"Your total score: {GuessingGameProcess.Points} points.");
            Console.WriteLine("Remember, winners never quit, and quitters never win.\n");
            WaitForAcknowledgement();
        }

        static bool HandleLevelProgression(ref int points, int level, int totalLevel)
        {
            GuessingGameProcess.UpdatePoints(10);  // Award 10 points for correct guess
            Console.WriteLine($"You earned 10 points! Total points: {GuessingGameProcess.Points}");
            Console.WriteLine();

            if (level < totalLevel - 1)
            {
                bool continueGame = YesNoConfirmation("Do you want to continue to the next level? (yes/no): ");
                Console.WriteLine();


                if (!continueGame)
                {
                    Console.WriteLine($"Your total score: {GuessingGameProcess.Points} points.");
                    Console.WriteLine("Thanks for playing!\n");
                    WaitForAcknowledgement();
                    return false;  // Back to main menu
                }
            }
            return true;  // Continue to next level
        }

        public static void EndGameMessage(string userName)
        {
            Console.WriteLine("====================================================");
            Console.WriteLine($"CONGRATULATIONS, {userName}!");
            Console.WriteLine($"You completed all levels!");
            Console.WriteLine($"Your total score: {GuessingGameProcess.Points} points.");
            Console.WriteLine("You're a Hangman Master!");
            Console.WriteLine("====================================================\n");

            WaitForAcknowledgement();
        }

        static void AccountSaving()
        {
            bool confirmSave = YesNoConfirmation("\nDo you want to save this account? (yes/no): ");
            if (confirmSave)
            {
                Console.WriteLine("\nAccount saved successfully! You can now login to the game.\n");
                accountSaved = true;
            }
            else
            {
                Console.WriteLine("\nAccount not saved. Please register again.\n");
            }
        }

        static bool YesNoConfirmation(string displayText)
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

        static void HandleRegistration()
        {
            accountSaved = false;

            while (!accountSaved)
            {
                Console.WriteLine("=====Account Registration=====");
                string fullName = AcceptNonEmptyInput("Enter your full name: ");
                string registerUserName = AcceptNonEmptyInput("Enter your desired username: ");
                string registerPassWord = AcceptNonEmptyInput("Enter your password: ");
                AccountSaving();
            }
        }

        static void HandleLeaderBoards()
        {
            Console.WriteLine("=====Leaderboards=====\n");
            Console.WriteLine("Rank\tUsername\tScore");
            Console.WriteLine("-----------------------------\n");
            Console.WriteLine("Nothing to show right now.\n");
        }

        static bool ConfirmExit()
        {
            return YesNoConfirmation("Are you sure you want to exit the game? (yes/no): ");
        }

        static void WaitForAcknowledgement()
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


    }
}