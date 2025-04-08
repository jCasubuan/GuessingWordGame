
using System;
using GuessingGame_BusinessLogic;

namespace GuessingWordGame
{
    public class Program
    {
        static bool accountSaved = false;

        static void Main(string[] args)
        {
            DisplayWelcomeMessage(); // method calling

            string userName = string.Empty;

            bool running = true;
            while (running)
            {
                Console.WriteLine("[1] Login ");
                Console.WriteLine("[2] Register ");
                Console.WriteLine("[3] Leaderboards ");
                Console.WriteLine("[4] Exit\n");
                string optionsInput = AcceptNonEmptyInput("Choose an option: ");
                Console.WriteLine();

                switch (optionsInput)
                {
                    case "1":
                        HandleLogin();
                        break;

                    case "2":
                        HandleRegistration();
                        break;

                    case "3":
                        HandleLeaderBoards();
                        break;

                    case "4":
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

        static void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to my C# version of Hangman. Hope you like it!");
            Console.WriteLine("_______________________________________________________" + Environment.NewLine);
        }

        static void HandleLogin()
        {
            Console.WriteLine("=====Login=====");
            string userName = AcceptNonEmptyInput("Enter Username: ");
            string passWord = AcceptNonEmptyInput("Enter Password: ");
            Console.WriteLine($"\nWelcome, {userName}! Let's start the game\n");
            bool wantsToPlay = StartGame(userName);

                if (wantsToPlay)
                {
                    Console.WriteLine("Returning to main menu...\n");
                }
        }

        static bool StartGame(string userName)
        {
            string[] wordsToGuess = GuessingGameProcess.GetWords(); // business logic
            int totalLevel = GuessingGameProcess.GetTotalLevel(); // business logic
            int points = GuessingGameProcess.GetPoints(); // business logic

            for (int level = 0; level < totalLevel; level++)
            {
                Console.WriteLine($"LEVEL {level + 1}/{totalLevel}");

                int playerLives = 7;

                string wordToGuess = wordsToGuess[level];
                bool guessedCorrectly = false;

                while (!guessedCorrectly)
                {
                    guessedCorrectly = PlayLevel(userName, wordToGuess, ref playerLives); // method calling

                    if (!guessedCorrectly)
                    {
                            Console.Write("\nDo you want to try again? (yes/no): ");
                            string tryAgain = YesNoConfirmation("") ? "yes" : "no";
                            Console.WriteLine();

                            if (tryAgain == "no")
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

                        playerLives = 3; // Reset lives if they choose to try again
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

        public static bool PlayLevel(string userName, string wordToGuess, ref int playerLives)
        {
            char[] guessedWord = GuessingGameProcess.InitializeGuessedWord(wordToGuess);

            while (playerLives > 0)
            {
                DisplayLevelInfo(playerLives, guessedWord);

                char guessedLetter = GetValidGuess();
                bool correctGuess = GuessingGameProcess.ProcessPlayerGuess(wordToGuess, guessedWord, guessedLetter, ref playerLives);

                if (GuessingGameProcess.IsWordGuessed(guessedWord, wordToGuess))
                {
                    return CheckLevelResult(guessedWord, wordToGuess);
                }

                if (playerLives > 0)
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

            Console.WriteLine("Sorry, you ran out of lives!");
            return false;  
        }

        public static void DisplayLevelInfo(int playerLives, char[] guessedWord)
        {
            Console.WriteLine($"Your lives remaining: {playerLives} | Points: {GuessingGameProcess.GetPoints()}");
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
            Console.WriteLine($"Better luck next time, {userName}!");
        }

        public static void ExitMidGameLosing(int points)
        {
            Console.WriteLine("You have decided to end the game midway, but why?");
            Console.WriteLine($"Your total score: {GuessingGameProcess.GetPoints()} points.");
            Console.WriteLine("Remember, winners never quit, and quitters never win.\n");
        }

        static bool HandleLevelProgression(ref int points, int level, int totalLevel)
        {
            GuessingGameProcess.UpdatePoints(10);  // Award 10 points for correct guess
            Console.WriteLine($"You earned 10 points! Total points: {GuessingGameProcess.GetPoints()}");
            Console.WriteLine();

            if (level < totalLevel - 1)
            {
                bool continueGame = YesNoConfirmation("Do you want to continue to the next level? (yes/no): ");
                Console.WriteLine();


                if (!continueGame)
                {
                    Console.WriteLine($"Your total score: {GuessingGameProcess.GetPoints()} points.");
                    Console.WriteLine("Thanks for playing!\n");
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
            Console.WriteLine($"Your total score: {GuessingGameProcess.GetPoints()} points.");
            Console.WriteLine("You're a Hangman Master!");
            Console.WriteLine("====================================================");
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

    }
}