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
        IGameDataService gameDataService;

        public GameDataService() 
        {
            gameDataService = new InMemoryGameDataService();
            //gameDataService = new TextFileGameDataService();
            //gameDataService = new JsonFileGameDataService();
            //gameDataService = new DBGameDataService();
        }

        //--- CREATE ---
        public bool AddWordHint(WordHint newWordHint)
        {
            return gameDataService.AddWordHint(newWordHint);
        }

        //--- READ ---
        public IReadOnlyList<WordHint> GetAllWordHints()
        {
            return gameDataService.GetAllWordHints();
        }

        public WordHint SearchForWord(string word)
        {
            return gameDataService.SearchForWord(word);

        }

        //--- UPDATE --- 
        public bool UpdateWord(string oldWord, WordUpdateRequest updateRequest)
        {
            return gameDataService.UpdateWord(oldWord, updateRequest);
        }

        //--- DELETE ---
        public bool DeleteWord(string word)
        {
            return gameDataService.DeleteWord(word);
        }
    }


}
