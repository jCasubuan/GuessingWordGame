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
            wordsToGuess.Add(new WordHint("cache memory", "High-speed memory for frequently accessed data", "hard"));
            wordsToGuess.Add(new WordHint("tokenization", "Process of replacing sensitive data with tokens", "extra hard"));
            wordsToGuess.Add(new WordHint("encryption", "Technique for securing data through coding", "medium"));
            wordsToGuess.Add(new WordHint("firewall", "Security system that controls network traffic", "medium"));
            wordsToGuess.Add(new WordHint("bandwidth", "Maximum data transfer rate of a network", "medium"));
            wordsToGuess.Add(new WordHint("algorithm", "Step-by-step procedure for solving a problem", "easy"));
            wordsToGuess.Add(new WordHint("bitrate", "Amount of data processed per second in a stream", "medium"));
            wordsToGuess.Add(new WordHint("syntax", "Rules defining correct structure in programming", "medium"));
            wordsToGuess.Add(new WordHint("iteration", "Repetition of a process or loop", "medium"));
            wordsToGuess.Add(new WordHint("latency", "Time delay in data transmission", "medium"));
            wordsToGuess.Add(new WordHint("debugging", "Process of finding and fixing code errors", "easy"));
            wordsToGuess.Add(new WordHint("malware", "Software designed to damage or disrupt systems", "medium"));
            wordsToGuess.Add(new WordHint("phishing", "Fraudulent attempt to obtain sensitive information", "medium"));
            wordsToGuess.Add(new WordHint("compiler", "Tool that translates code into executable form", "easy"));
            wordsToGuess.Add(new WordHint("protocol", "Set of rules for data communication", "medium"));
            wordsToGuess.Add(new WordHint("virtual machine", "Software emulation of a physical computer", "hard"));
            wordsToGuess.Add(new WordHint("scalability", "Ability of system to handle growing workload", "hard")); //21
            wordsToGuess.Add(new WordHint("processor", "Brain of a computer that executes instructions", "easy"));
            wordsToGuess.Add(new WordHint("query", "Request for information from a database", "medium"));

        }

        public IReadOnlyList<WordHint> GetAllWordHints()
        {
            return wordsToGuess.AsReadOnly();
        }

        // other CRUD functionalities, coming soon for admin menu


    }
}
