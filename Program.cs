using System;

namespace GuessingWordGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my C# version of Hangman. Hope you like it!"); // Header
            Console.WriteLine("_______________________________________________________" + Environment.NewLine);
            // for separator

            string userName;
            do
            {
                Console.Write("Before we start, please enter your preferred username: "); // required username
                userName = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(userName))
                {
                    Console.WriteLine("Username cannot be empty! Please try again.");
                    Console.WriteLine();
                    // error handling for empty username input
                }
            }
            while (string.IsNullOrEmpty(userName));

            Console.WriteLine();

            string wordToGuess = "supercalifragilisticexpialidocious"; // pwede to mabago
            char[] guessedWord = new char[wordToGuess.Length];
            int playerLives = 3; // pwede rin to mabago 

            for (int i = 0; i < guessedWord.Length; i++)
            {
                guessedWord[i] = '_';
            }

            while (playerLives > 0)
            {
                Console.WriteLine($"Your lives remaining: {playerLives}"); // prints the playerLives
                Console.WriteLine("Word to guess: " + new string(guessedWord)); // prints the wordToGuess
                Console.WriteLine();
                Console.WriteLine("Guess a letter: ");

                string inputLetter = Console.ReadLine()?.Trim().ToLower();
                Console.WriteLine();

                if (string.IsNullOrEmpty(inputLetter))
                {
                    Console.WriteLine("Your input cannot be empty! Please enter a letter.");
                    Console.WriteLine();
                    continue;
                    // bawal empty input

                }

                if (inputLetter.Length != 1)
                {
                    Console.WriteLine("Not counted! Please enter a single letter only.");
                    Console.WriteLine();
                    continue;
                    // bawal maraming characters

                }

                if (!char.IsLetter(inputLetter[0]))
                {
                    Console.WriteLine("Oops, only letters are acceptable! Any symbols and numbers are not allowed.");
                    Console.WriteLine();
                    continue;
                    // bawal numbers at symbols

                }

                char guessedLetter = inputLetter[0];
                bool correctGuess = false;

                for (int i = 0; i < wordToGuess.Length; i++)
                {
                    if (wordToGuess[i] == guessedLetter)
                    {
                        guessedWord[i] = guessedLetter;
                        correctGuess = true;
                        // prints the correct corresponding letter and its position from the wordToGuess

                    }
                }

                if (!correctGuess)
                {
                    playerLives--;
                    if (playerLives > 0)
                    {
                        Console.WriteLine("Incorrect guess! Please try again.");
                        Console.WriteLine();
                        // print this pag mali input 

                    }
                }

                if (new string(guessedWord) == wordToGuess)
                {
                    Console.WriteLine($"Congratulations, {userName}! You won!!!");
                    return;
                    // prints this pag tugma sa wordToGuess

                }


            }

            Console.WriteLine($"Game Over! The word was: {wordToGuess}");
            // prints if the user uses all their lives without guessing the right word

        }
    }
}
