using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameCommon
{
    public class WordUpdateRequest
    {
        public string NewWord {  get; set; }
        public string NewHint { get; set; }
        public string NewDifficulty { get; set; }

        public WordUpdateRequest()
        {
            
        }

        public WordUpdateRequest(string newWord, string newHint, string newDiffictuly)
        {
            NewWord = newWord;
            NewHint = newHint;
            NewDifficulty = newDiffictuly;
        }
    }
}
