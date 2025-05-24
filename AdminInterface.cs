using GuessingGame_BusinessLogic;
using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GuessingWordGame
{   
    // this class is exclusive for admin features only, specifically to perform CRUD operations
    public static class AdminInterface
    {

        // Menus
        private static string[] adminMenu = new string[]
        {
            "[1] Manage Players",
            "[2] Manage Words",
            "[3] Leaderboard Tools",
            "[4] Log out\n"
        };

        private static string[] managePlayers = new string[]
        {
            "[1] View All Players",
            "[2] View Player Stats",
            "[3] Delete Player Account",
            "[4] Return to Admin Menu\n"
        };

        private static string[] manageWords = new string[]
        {
            "[1] View All Words",
            "[2] Search Words",
            "[3] Add New Word & Hint",
            "[4] Update Existing Word",
            "[5] Delete Word",
            "[6] Return to Admin Menu\n"
        };

        private static string[] leaderboardTools = new string[]
        {
            "[1] View Leaderboard",
            "[2] Find Player in Leaderboard",
            "[3] Return to Admin Menu\n"
        };

        public static void DisplayAdminMenu(string userName)
        {
            Console.Clear();
            Console.WriteLine("===== Admin Menu =====\n");
            Console.WriteLine($"Welcome, {userName}!\n");

            Program.DisplayMenuOptions(adminMenu);
        }

        public static void DisplayLoggedInAdminMenu(string userName) // for Admin main menu
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                DisplayAdminMenu(userName);

                int optionsInput = Program.GetOptionsInput("Choose an option: ", 1, 4);
                Console.WriteLine();

                switch (optionsInput)
                {
                    case 1:
                        PlayerManager(userName);
                        break;

                    case 2:
                        WordManager();
                        break;

                    case 3:
                        LeaderboardTools();
                        break;

                    case 4:
                        if (Program.ConfirmLogout(userName))
                        {
                            Console.WriteLine($"\nLogging out...\n");
                            loggedIn = false;
                        }
                        else
                        {
                            Console.WriteLine($"Logout cancelled. Returning to Admin menu...\n");
                        }
                        break;                   
                }

            }
        }

        public static void DisplayManagePlayerOptions()
        {
            Console.Clear();
            Console.WriteLine("===== Manage Players =====\n");

            Program.DisplayMenuOptions(managePlayers);
        }

        public static void PlayerManager(string userName) // for manage player submenu
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                DisplayManagePlayerOptions();
                int optionsInput = Program.GetOptionsInput("Choose an option: ", 1, 4);
                Console.WriteLine();

                switch (optionsInput)
                {
                    case 1: 
                        ViewAllPlayers();   
                        break;

                    case 2:
                        ViewPlayerStats();
                        break;

                    case 3:
                        DeletePlayerAccount(); 
                        break;

                    case 4:
                        return;

                }   
            }
        }

        public static void ViewAllPlayers()
        {
            Console.Clear();
            Console.WriteLine("===== List of All Registered Players =====\n");

            Console.WriteLine($"{"ID",-5}{"Username",-15}{"Full Name",-20}{"Score",-10}{"High Score"}");
            Console.WriteLine(new string('-', 60));

            DisplayAllPlayersData(Program.gameProcess);
            Console.WriteLine();
            Console.WriteLine();
            Program.WaitForAcknowledgement();
        }
        
        private static void DisplayAllPlayersData(GuessingGameProcess gameProcess)
        {
            List<Player> allPlayers = gameProcess.GetAllPlayers();

            foreach (var player in allPlayers)
            {
                Console.WriteLine($"{player.PlayerId,-5}{player.UserName,-15}{player.FullName,-20}{player.Scores,-10}{player.HighScore}");
            }

            if (allPlayers.Count == 0)
            {
                Console.WriteLine("\n\nNo registered players found.\n");
            }

        }

        public static void ViewPlayerStats()
        {
            Console.Clear();
            Console.WriteLine("===== View Player Stats =====");

            string adminInput = Program.AcceptNonEmptyInput("\nEnter the username or player ID to search: ");
            Player player = Program.gameProcess.SearchPlayerByInput(adminInput);

            if (player != null)
            {
                DisplayPlayerInfo(player);
            }
            else
            {
                Console.WriteLine($"\nPlayer '{adminInput}' not found.\n");
            }

            Program.WaitForAcknowledgement();
        }

        private static void DisplayPlayerInfo(Player player)
        {
            Console.WriteLine($"\nPlayer Id             : {player.PlayerId}");
            Console.WriteLine($"Username              : {player.UserName}");
            Console.WriteLine($"Full Name             : {player.FullName}");
            Console.WriteLine($"Score                 : {player.Scores}");
            Console.WriteLine($"High Score            : {player.HighScore}");
            Console.WriteLine($"Last Level Reached    : Level {player.LastCompletedLevel + 1}\n\n");
        }

        public static void DeletePlayerAccount()
        {
            Console.Clear();
            DeletePlayerWarning();
            string usernameToDelete = Program.AcceptNonEmptyInput("Enter the username to delete: ");

            if(!Program.gameProcess.PlayerExists(usernameToDelete))
            {
                Console.WriteLine($"\nPlayer '{usernameToDelete}' does not exist.\n");
            }
            else
            {
                DeletePlayerValidation(usernameToDelete);
            }

            Program.WaitForAcknowledgement();
        }

        private static void DeletePlayerValidation(string usernameToDelete)
        {
            bool confirm = Program.YesNoConfirmation($"\nAre you sure you want to delete '{usernameToDelete}'? This process cannot be undone. (yes/no): ");

            if (confirm)
            {
                Program.gameProcess.DeletePlayer(usernameToDelete);
                Console.WriteLine($"\n\nPlayer '{usernameToDelete}' has been successfully deleted.\n\n");
            }
            else
            {
                Console.WriteLine($"\n\nDeletion cancelled for '{usernameToDelete}'.\n\n");
            }
        }

        private static void DeletePlayerWarning()
        {           
            Console.WriteLine("===== DELETE PLAYER ACCOUNT =====\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("WARNING: IRREVERSIBLE ACTION\n");
            Console.WriteLine("This operation will PERMANENTLY delete the selected player account and ALL associated data.");
            Console.WriteLine("• - All player progress will be lost.");
            Console.WriteLine("• - All achievements and statistics will be erased.");
            Console.WriteLine("• - Account recovery will NOT be possible after deletion.");
            Console.WriteLine("\nPROCEED WITH EXTREME CAUTION. THIS ACTION CANNOT BE UNDONE.\n");
            Console.ResetColor();
        }

        public static void DisplayManageWordOptions()
        {
            Console.Clear();
            Console.WriteLine("===== Manage Words =====\n");

            Program.DisplayMenuOptions(manageWords);

        }
      
        public static void WordManager()
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                DisplayManageWordOptions();

                int optionsInput = Program.GetOptionsInput("Choose an option: ", 1, 6);
                Console.WriteLine();

                switch(optionsInput)
                {
                    case 1:
                        ViewAllWords(Program.gameProcess);
                        break;

                    case 2:
                        SearchWord();
                        break;
                    
                    case 3:
                        AddNewWordHint();
                        break;

                    case 4:
                        UpdateExistingWord();
                        break;
                    
                    case 5:
                        DeleteWord();
                        break;

                    case 6:
                        return;

                }   

            }
        }
        

        public static void ViewAllWords(GuessingGameProcess gameProcess)
        {
            List<WordHint> wordsToGuess = gameProcess.GetWordHints();
            const int pageSize = 20;

            if (wordsToGuess.Count == 0)
            {
                DisplayEmptyWordList();
            }
            else
            {
                BrowseWordPages(wordsToGuess, pageSize);
            }

            Program.WaitForAcknowledgement();
        }

        private static void DisplayEmptyWordList()
        {
            Console.Clear();
            Console.WriteLine("===== List of All Words =====\n");
            Console.WriteLine("\nNo words to guess yet.\n\n");
        }

        private static void BrowseWordPages(List<WordHint> wordsToGuess, int pageSize)
        {
            int totalPages = (int)Math.Ceiling((double)wordsToGuess.Count / pageSize);
            int currentPage = 0;    

            while (true)
            {
                Console.Clear();
                DisplayPageContents(wordsToGuess, currentPage, pageSize, totalPages);

                Console.Write("Choose an option: ");
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "n" && currentPage < totalPages -1)
                {
                    currentPage++;
                }
                else if (input == "p" && currentPage > 0)
                {
                    currentPage--;
                }
                else if (input == "q")
                {
                    break;
                }
            }
        }

        private static void DisplayPageContents(List<WordHint> wordsToGuess, int currentPage, int pageSize, int totalPages)
        {
            Console.WriteLine("===== List of All Words =====\n");
            Console.WriteLine($"{"No.",-5}{"Word",-30}{"Hint",-45}{"Difficulty",-20}");
            Console.WriteLine(new string('-', 95));

            int start = currentPage * pageSize;
            int end = Math.Min(start + pageSize, wordsToGuess.Count);

            for (int i = start; i < end; i++ )
            {
                var word = wordsToGuess[i];
                Console.WriteLine($"{i + 1,-5}{word.Word,-20}{word.Hint,-55}{word.Difficulty,-30}");
            }

            Console.WriteLine($"\nN = Next | P = Previous | Q = Quit\t\tPage {currentPage + 1} of {totalPages}");
        }

        public static void SearchWord()
        {
            Console.Clear();
            Console.WriteLine("===== Search Words =====\n");

            string inputWord = Program.AcceptNonEmptyInput("Enter the word to search: ");
            WordHint wordHint= Program.gameProcess.SearchForWord(inputWord);

            if (wordHint != null)
            {
                DisplayWordHint(wordHint);
            }
            else
            {
                Console.WriteLine($"\nWord '{inputWord}' was not found.\n\n");
            }

            Program.WaitForAcknowledgement();
        }

        private static void DisplayWordHint(WordHint wordHint)
        {
            Console.WriteLine("\nWord found!\n");
            Console.WriteLine($"Word      :  {wordHint.Word}");
            Console.WriteLine($"Hint      :  {wordHint.Hint}");
            Console.WriteLine($"Difficulty:  {wordHint.Difficulty}\n");

        }

        public static void AddNewWordHint()
        {
            Console.Clear();
            Console.WriteLine("===== Add new Word & Hint =====\n\n");

            string inputWord = Program.AcceptNonEmptyInput("Enter the new word to add: ");
            string inputHint = Program.AcceptNonEmptyInput("Enter a new hint to add: ");
            string inputDifficulty = ChooseDifficulty();

            Console.WriteLine($"\nWord      : {inputWord}");
            Console.WriteLine($"Hint      : {inputHint}");
            Console.WriteLine($"Difficulty: {inputDifficulty}");

            bool confirm = Program.YesNoConfirmation("\nDo you want to save this word? (yes/no): ");

            if ( confirm )
            {
                bool success = Program.gameProcess.AddWordHint(inputWord, inputHint, inputDifficulty);
                if ( success )
                {
                    Console.WriteLine("\nNew word successfully added!");
                }
                else
                {
                    Console.WriteLine("\nWord already exists. Cannot add duplicate entries.");
                }
            }
            else
            {
                Console.WriteLine("\nWord not saved. Returning to menu...");
            }

            Console.WriteLine();
            Console.WriteLine();

            Program.WaitForAcknowledgement();
        }

        private static string ChooseDifficulty()
        {
            string[] levels = { "easy", "medium", "hard", "extra hard" };
            Console.WriteLine("\nChoose difficulty: ");

            for (int i = 0; i < levels.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {levels[i]}");
            }

            int choice = Program.GetOptionsInput("\nSelect (1-4): ", 1, 4);
            return levels[choice - 1];
        }

        public static void UpdateExistingWord()
        {
            Console.Clear();
            Console.WriteLine("===== Update Existing Words =====\n");

            string inputWord = GetWordToUpdate();
            if (inputWord == null )
                return;

            WordHint existingWord = Program.gameProcess.SearchForWord(inputWord);

            string newWord = GetUpdatedProperty(existingWord, "Word", existingWord.Word);
            string newHint = GetUpdatedProperty(existingWord, "Hint", existingWord.Hint);
            string newDifficulty = GetUpdatedProperty(existingWord, "Difficulty", existingWord.Difficulty);
            
            ProcessWordUpdate(inputWord, newWord, newHint, newDifficulty);
            
        }

        private static string GetWordToUpdate()
        {
            string inputWord = Program.AcceptNonEmptyInput("Enter the word you want to update: ");
            WordHint existingWord = Program.gameProcess.SearchForWord(inputWord);

            if (existingWord == null)
            {
                Console.WriteLine($"\nThe word '{inputWord}' was not found.\n");
                Program.WaitForAcknowledgement();
                return null;
            }

            Console.WriteLine($"\nCurrent Hint: {existingWord.Hint}");
            Console.WriteLine($"Current Difficulty: {existingWord.Difficulty}\n");

            return inputWord;
        }

        private static string GetUpdatedProperty(WordHint existingWord, string propertyName, string currentValue)
        {
            Console.Write($"Enter new {propertyName.ToLower()} (or just press ENTER to keep current): ");
            string newInput = Console.ReadLine()?.Trim();
            return string.IsNullOrEmpty( newInput ) ? currentValue : newInput;
        }

        private static void ProcessWordUpdate(string oldWord, string newWord, string newHint, string newDifficulty)
        {
            bool confirm = Program.YesNoConfirmation("\nAre you sure you want to update this word? (yes/no): ");

            if (confirm)
            {
                bool success = Program.gameProcess.UpdateWord(oldWord, newWord,newHint,newDifficulty);

                if (success)
                {
                    Console.WriteLine("\nWord updated successfully!\n");
                }
                else
                {
                    Console.WriteLine("\nUpdate failed. The new word may already exist or conflict with another entry.\n");
                }
            }
            else
            {
                Console.WriteLine("\nUpdate cancelled.\n");
            }
            Program.WaitForAcknowledgement();
        }

        public static void DeleteWord()
        {
            Console.Clear();
            DeleteWordWarning();
            string inputWord = Program.AcceptNonEmptyInput("Enter the word you want to delete: ");
            WordHint wordHint = Program.gameProcess.SearchForWord(inputWord);

            if (wordHint == null)
            {
                Console.WriteLine($"\nThe word '{inputWord}' was not found.\n");
                Program.WaitForAcknowledgement();
                return;
            }

            DisplayWordHint(wordHint);
            DeleteWordValidation(inputWord);
            Program.WaitForAcknowledgement();
        }

        private static void DeleteWordWarning()
        {            
            Console.WriteLine("===== Delete Word =====\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("WARNING: Words deleted through this function will be permanently removed and CANNOT be recovered!\n");
            Console.ResetColor();
        }

        private static void DeleteWordValidation(string inputWord)
        {
            bool confirm = Program.YesNoConfirmation("\nAre you sure you want to permanently delete this word? (yes/no): ");

            if (confirm)
            {
                bool success = Program.gameProcess.DeleteWord(inputWord);

                if (success)
                {
                    Console.WriteLine("\nWord deleted successfully!\n");
                }
                else
                {
                    Console.WriteLine("\nFailed to delete the word. Something went wrong.\n");
                }
            }
            else
            {
                Console.WriteLine("\nDelete operation cancelled.\n");
            }
        }
           

        public static void DisplayLeaderboardToolOptions()
        {
            Console.Clear();
            Console.WriteLine("===== Leaderboard Tools =====\n");

            Program.DisplayMenuOptions(leaderboardTools);
        }

        public static void LeaderboardTools()   
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                DisplayLeaderboardToolOptions();

                int optionsInput = Program.GetOptionsInput("Choose an option: ", 1, 3);
                Console.WriteLine();

                switch(optionsInput)
                {
                    case 1:
                        ViewLeaderboard();
                        break;

                    case 2:
                        FindPlayerInLeaderboard();
                        break;

                    case 3:
                        return;

                }
            }
        }

        public static void ViewLeaderboard()
        {
            Console.Clear();
            Console.WriteLine("===== Leaderboards =====\n");
            Console.WriteLine("Rank\tUsername\tScore");
            Console.WriteLine("-----------------------------\n");

            Program.HandleLeaderboardContents(showEmptyMesssage: false);

            Console.WriteLine();
            Console.WriteLine();
            Program.WaitForAcknowledgement();
        }

        public static void FindPlayerInLeaderboard()
        {
            Console.Clear();
            Console.WriteLine("===== Find Player in Leaderboards =====\n");

            string adminInput = Program.AcceptNonEmptyInput("\nEnter the username or player ID to search: ");
            Player player = Program.gameProcess.SearchPlayerByInput(adminInput);

            if (player != null)
            {
                DisplayPlayerRank(player.UserName);
            }
            else
            {
                Console.WriteLine($"\nPlayer '{adminInput}' not found.\n");
            }
            Program.WaitForAcknowledgement();
        }

        private static void DisplayPlayerRank(string username)
        {
            var leaderboard = Program.gameProcess.GetLeaderboard();
            int rank = 1;

            foreach (var entry in leaderboard)
            {
                if (entry.UserName.Equals(username, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"\nPlayer Found!\n");
                    Console.WriteLine($"Rank: #{rank}");
                    Console.WriteLine($"Username: {entry.UserName}");
                    Console.WriteLine($"Score: {entry.HighScore}\n");
                    return;
                }
                rank++;
            }

            Console.WriteLine("\nPlayer exists, but not found in leaderboard ranking.\n");
        }


    }
}
