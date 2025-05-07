using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public class GameDataService
    {
        private List<WordHint> wordsToGuess = new List<WordHint>();

        public GameDataService()
        {
            CreateDummyWords();
        }

        private void CreateDummyWords()
        {
            wordsToGuess.Add(new WordHint("motherboard", "Main circuit board of a computer", "easy"));
            wordsToGuess.Add(new WordHint("database", "Structured collection of data", "medium"));
            wordsToGuess.Add(new WordHint("ping", "Network diagnostic tool for measuring latency", "easy"));
            wordsToGuess.Add(new WordHint("recursion", "Programming technique where a function calls itself", "medium"));
            wordsToGuess.Add(new WordHint("cache memory", "High-speed memory for frequently accessed data", "hard")); // 5
            wordsToGuess.Add(new WordHint("tokenization", "Process of replacing sensitive data with tokens", "extra hard"));
            wordsToGuess.Add(new WordHint("encryption", "Technique for securing data through coding", "medium"));
            wordsToGuess.Add(new WordHint("firewall", "Security system that controls network traffic", "medium"));
            wordsToGuess.Add(new WordHint("bandwidth", "Maximum data transfer rate of a network", "medium"));
            wordsToGuess.Add(new WordHint("algorithm", "Step-by-step procedure for solving a problem", "easy")); // 10
            wordsToGuess.Add(new WordHint("bitrate", "Amount of data processed per second in a stream", "medium"));
            wordsToGuess.Add(new WordHint("syntax", "Rules defining correct structure in programming", "medium"));
            wordsToGuess.Add(new WordHint("iteration", "Repetition of a process or loop", "medium"));
            wordsToGuess.Add(new WordHint("latency", "Time delay in data transmission", "medium"));
            wordsToGuess.Add(new WordHint("debugging", "Process of finding and fixing code errors", "easy")); // 15
            wordsToGuess.Add(new WordHint("malware", "Software designed to damage or disrupt systems", "medium"));
            wordsToGuess.Add(new WordHint("phishing", "Fraudulent attempt to obtain sensitive information", "medium"));
            wordsToGuess.Add(new WordHint("compiler", "Tool that translates code into executable form", "easy"));
            wordsToGuess.Add(new WordHint("protocol", "Set of rules for data communication", "medium"));
            wordsToGuess.Add(new WordHint("virtual machine", "Software emulation of a physical computer", "hard")); // 20
            wordsToGuess.Add(new WordHint("scalability", "Ability of system to handle growing workload", "hard")); 
            wordsToGuess.Add(new WordHint("processor", "Brain of a computer that executes instructions", "easy"));
            wordsToGuess.Add(new WordHint("query", "Request for information from a database", "medium"));
            wordsToGuess.Add(new WordHint("devops", "Combining software development and IT operations", "medium"));
            wordsToGuess.Add(new WordHint("variable", "Named storage location for data in programming", "easy")); // 25


        }

        public IReadOnlyList<WordHint> GetAllWordHints()
        {
            return wordsToGuess.AsReadOnly();
        }

        public WordHint SearchForWord(string word)
        {
            foreach (WordHint wordHint in wordsToGuess)
            {
                if (wordHint.Word.Equals(word, StringComparison.OrdinalIgnoreCase))
                {
                    return wordHint;
                }
            }
            return null;

        }

        public bool AddWordHint(string word, string hint, string difficulty)
        {
            foreach (var words in wordsToGuess)
            {
                if (words.Word.Equals(word, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            wordsToGuess.Add(new WordHint(word, hint, difficulty));
            return true;
        }

        public bool UpdateWord(string oldWord, string newWord, string newHint, string newDifficulty)
        {
            int oldWordIndex = FindWordIndex(oldWord);

            if (oldWordIndex == -1)
            {
                return false; // word not found
            }

            if (!oldWord.Equals(newWord, StringComparison.OrdinalIgnoreCase) &&
                DoesWordExist(newWord))
            {
                return false; 
            }

            // Perform update
            wordsToGuess[oldWordIndex].Word = newWord;
            wordsToGuess[oldWordIndex].Hint = newHint;
            wordsToGuess[oldWordIndex].Difficulty = newDifficulty;
            return true;
        }


        private bool DoesWordExist(string word) // method pang chek kung ang word ay nasa list collection
        {
            for (int i = 0; i < wordsToGuess.Count; i++)
            {
                if (string.Equals(wordsToGuess[i].Word, word, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private int FindWordIndex(string word) // method pang hanap ng index ng word sa list collection
        {
            for (int i = 0; i < wordsToGuess.Count; i++)
            {
                if (string.Equals(wordsToGuess[i].Word, word, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        public bool DeleteWord(string word)
        {
            int index = FindWordIndex(word);

            if (index == -1)
                return false;

            wordsToGuess.RemoveAt(index);
          
            for (int i = 0; i < wordsToGuess.Count; i++)
            {
                wordsToGuess[i].No = i + 1;
            }

            return true;
        }

    }
}
