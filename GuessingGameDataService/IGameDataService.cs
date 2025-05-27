using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public interface IGameDataService
    {
        //CREATE
        bool AddWordHint(WordHint wordHint);

        //READ
        IReadOnlyList<WordHint> GetAllWordHints();
        WordHint SearchForWord(string word);

        //UPDATE
        public bool UpdateWord(string oldWord, WordUpdateRequest updateRequest);

        //DELETE
        public bool DeleteWord(string word);


    }
}
