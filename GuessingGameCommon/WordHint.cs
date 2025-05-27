using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameCommon
{
    public class WordHint
    {   
        public int ID {  get; set; }
        public int No { get; set; }
        public string Word { get; set; }
        public string Hint { get; set; }
        public string Difficulty { get; set; }

        public WordHint() { }

        public WordHint(int id, string word, string hint, string difficulty = "medium") 
        {
            ID = id;
            Word = word;
            Hint = hint;
            Difficulty = difficulty;
        }

        public WordHint(string word, string hint, string difficulty = "medium")
        {
            Word = word;
            Hint = hint;
            Difficulty = difficulty;
        }

    }
}
