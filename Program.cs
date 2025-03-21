﻿using System;

namespace GuessingWordGame
{
     public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my C# version of Hangman. Hope you like it!"); // Header
            Console.WriteLine("_______________________________________________________" + Environment.NewLine);
            // for separator

            string userName = GetUserName("Before we start, please enter your preferred username: ");

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
                Console.WriteLine("Word to guess: " + new string(guessedWord)); // prints the wordToGuess as an " _ "
                Console.WriteLine();

                Console.WriteLine("Guess a letter: ");
                string inputLetter = Console.ReadLine()?.Trim().ToLower();                

                if (string.IsNullOrEmpty(inputLetter))
                {
                    UserEmptyInput();
                    continue;
                    // bawal empty input

                }

                if (inputLetter.Length != 1)
                {
                    ReadOnlySingleChar();
                    continue;
                    // bawal maraming characters

                }

                if (!char.IsLetter(inputLetter[0]))
                {
                    CheckForLetterInput();
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

        public static string GetUserName(string userInput)
        {
            string userName;
            do
            {
                Console.Write(userInput);
                userName = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(userName))
                {
                    Console.WriteLine("Username cannot be empty! Please try again.");
                    Console.WriteLine();

                }
            }
            while (string.IsNullOrEmpty(userName));

            return userName;
        }

        public static void UserEmptyInput()
        {
            Console.WriteLine("Your input cannot be empty! Please enter a letter.");
            Console.WriteLine();
        }

        public static void ReadOnlySingleChar()
        {
            Console.WriteLine("Not counted! Please enter a single letter only.");
            Console.WriteLine();
        }

        public static void CheckForLetterInput()
        {
            Console.WriteLine("Oops, only letters are acceptable! Any symbols and numbers are not allowed.");
            Console.WriteLine();
        }

    }
}
