using GuessingGameCommon;
using System.Text.Json;

namespace GuessingGameDataService
{
    public class JsonFileGameDataService : IGameDataService
    {   
        private List<WordHint> wordHints;
        private string jsonFilPath = "words.json";

        public JsonFileGameDataService() 
        {   
            wordHints = new List<WordHint>();
            ReadJsonDataFromFile();
        }

        public void EnsureFileExists()
        {
            if (!File.Exists(jsonFilPath))
            {
                File.WriteAllText(jsonFilPath, "[]");
            }
        }

        private void ReadJsonDataFromFile()
        {
            EnsureFileExists();
            string jsonText = File.ReadAllText(jsonFilPath);

            if (string.IsNullOrEmpty(jsonText))
            {
                wordHints = new List<WordHint>();
            }
            else
            {
                List<WordHint> deserializedWordHint = JsonSerializer.Deserialize<List<WordHint>>(jsonText,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

                if (deserializedWordHint == null)
                {
                    wordHints = new List<WordHint>();
                }
                else
                {
                    wordHints = deserializedWordHint;
                }
            }
        }

        private void WriteJsonDataToFile()
        {
            string jsonString = JsonSerializer.Serialize(wordHints, new JsonSerializerOptions
            { WriteIndented = true});

            File.WriteAllText(jsonFilPath, jsonString);
        }

        //CREATE
        public bool AddWordHint(WordHint newWordHint)
        {
            if (newWordHint == null || string.IsNullOrWhiteSpace(newWordHint.Word))
            {
                return false;
            }

            foreach (var existingWord in wordHints)
            {
                if (existingWord.Word.Equals(newWordHint.Word, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            int nextNo = 1;
            if (wordHints.Count > 0)
            {
                int maxNo = 0;
                for (int i = 0; i < wordHints.Count; i++)
                {
                    if (wordHints[i].No > maxNo)
                    {
                        maxNo = wordHints[i].No;
                    }
                }
                nextNo = maxNo + 1;
            }

            if (newWordHint.No == 0)
            {
                newWordHint.No = nextNo;
            }

            wordHints.Add(newWordHint);
            WriteJsonDataToFile();
            return true;
        }

        //READ
        public IReadOnlyList<WordHint> GetAllWordHints()
        {
            return wordHints.AsReadOnly();
        }

        public WordHint SearchForWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return null;
            }

            WordHint foundWord = null;
            for (int i = 0; i < wordHints.Count; i++)
            {
                if (wordHints[i].Word.Equals(word, StringComparison.OrdinalIgnoreCase))
                {
                    foundWord = wordHints[i];
                    break; 
                }
            }
            return foundWord;
        }

        //UPDATE
        public bool UpdateWord(string oldWord, WordUpdateRequest updateRequest)
        {
            if (string.IsNullOrWhiteSpace(oldWord) || updateRequest == null)
            {
                return false;
            }

            WordHint wordToUpdate = null;
            for (int i = 0; i < wordHints.Count;i++)
            {
                if (wordHints[i].Word.Equals(oldWord, StringComparison.OrdinalIgnoreCase))
                {
                    wordToUpdate = wordHints[i];
                    break;
                }
            }

            if (wordToUpdate == null)
            {
                return false;
            }

            if (!oldWord.Equals(updateRequest.NewWord, StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < wordHints.Count; i++)
                {
                    if (wordHints[i].Word.Equals(updateRequest.NewWord, StringComparison.OrdinalIgnoreCase))
                    {
                        return false; 
                    }
                }
            }

            wordToUpdate.Word = updateRequest.NewWord;
            wordToUpdate.Hint = updateRequest.NewHint;
            wordToUpdate.Difficulty = updateRequest.NewDifficulty;

            WriteJsonDataToFile();
            return true;
        }

        //DELETE
        public bool DeleteWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return false;
            }

            WordHint wordToRemove = null;
            int indexToRemove = -1;
            for(int i = 0;i < wordHints.Count;i++)
            {
                if (wordHints[i].Word.Equals(word, StringComparison.OrdinalIgnoreCase))
                {
                    wordToRemove = wordHints[i];
                    indexToRemove = i;
                    break;
                }
            }

            if (wordToRemove == null)
            {
                return false;
            }

            wordHints.RemoveAt(indexToRemove); 

            int currentNo = 1;
            for (int i = 0; i < wordHints.Count; i++)
            {
                wordHints[i].No = currentNo++;
            }

            WriteJsonDataToFile();
            return true;
        }

        

        

        
    }
}
