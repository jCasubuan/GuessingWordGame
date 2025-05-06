using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameCommon
{
    public class WordHint
    {
        public int No { get; set; }
        public string Word { get; set; }
        public string Hint { get; set; }
        public string Difficulty { get; set; } 

        public WordHint(string word, string hint, string difficulty = "medium") 
        { 
            Word = word;
            Hint = hint;
            Difficulty = difficulty;
        }

    }
}
