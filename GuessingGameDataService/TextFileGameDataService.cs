using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public class TextFileGameDataService : IGameDataService
    {
        private string FilePath = "words.txt";
        private char Delimiter = '|';
        private List<WordHint> wordsCache;

        public TextFileGameDataService() 
        {
            wordsCache = new List<WordHint>();
            EnsureFileExists();
            wordsCache.AddRange(LoadWordsFromFile());
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Close();
            }
        }

        private List<WordHint> LoadWordsFromFile()
        {
            List<WordHint> loadedWords = new List<WordHint>();
            try 
            {
                var lines = File.ReadAllLines(FilePath);

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var parts = line.Split(Delimiter);
                    if (parts.Length == 4)
                    {
                        if (int.TryParse(parts[0].Trim(), out int no))
                        {
                            string word = parts[1].Trim();
                            string hint = parts[2].Trim();
                            string difficulty = parts[3].Trim();
                            loadedWords.Add(new WordHint(word, hint, difficulty) { No = no });
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<WordHint>();
            }
            return loadedWords;
        }

        private void SaveWordsToFile()
        {
            List<string> linesToSave = new List<string>();
            foreach (WordHint word in wordsCache)
            {
                linesToSave.Add($"{word.No}{Delimiter}{word.Word}{Delimiter}{word.Hint}{Delimiter}{word.Difficulty}");
            }

            try 
            {
                File.WriteAllLines(FilePath, linesToSave); 
            }
            catch (Exception) 
            {
                //Console.WriteLine($"Error saving words to '{FilePath}': {"Error saving"}");
            }
        }

        //CREATE
        public bool AddWordHint(WordHint newWordHint)
        {
            if (newWordHint == null || string.IsNullOrWhiteSpace(newWordHint.Word))
            {
                return false;
            }

            foreach (WordHint existingWord in wordsCache)
            {
                if (existingWord.Word.Equals(newWordHint.Word, StringComparison.OrdinalIgnoreCase))
                {
                    return false; 
                }
            }

            if (newWordHint.No == 0)
            {
                int currentMaxNo = 0;
                if (wordsCache.Count > 0)
                {
                    foreach (WordHint existingWord in wordsCache)
                    {
                        if (existingWord.No > currentMaxNo)
                        {
                            currentMaxNo = existingWord.No;
                        }
                    }
                }
                newWordHint.No = currentMaxNo + 1;
            }

            wordsCache.Add(newWordHint);
            SaveWordsToFile(); 
            return true;
        }

        //READ
        public IReadOnlyList<WordHint> GetAllWordHints()
        {
            return wordsCache.AsReadOnly();
        }

        public WordHint SearchForWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return null;
            }

            foreach(WordHint existingWord in wordsCache)
            {
                if (existingWord.Word.Equals(word, StringComparison.OrdinalIgnoreCase))
                {
                    return existingWord;
                }
            }
            return null;
        }

        //UPDATE
        public bool UpdateWord(string oldWord, WordUpdateRequest updateRequest)
        {
            if (string.IsNullOrWhiteSpace (oldWord) || updateRequest == null)
            {
                return false;
            }

            WordHint wordToUpdate = null;
            for (int i = 0; i < wordsCache.Count; i++)
            {
                if (wordsCache[i].Word.Equals(oldWord, StringComparison.OrdinalIgnoreCase))
                {
                    wordToUpdate = wordsCache[i];
                    break;
                }
            }
            if (wordToUpdate == null)
            {
                return false;
            }

            wordToUpdate.Word = updateRequest.NewWord;
            wordToUpdate.Hint = updateRequest.NewHint;
            wordToUpdate.Difficulty = updateRequest.NewDifficulty;

            SaveWordsToFile();
            return true;
        }

        //DELETE
        public bool DeleteWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return false;
            }

            int indexToRemove = -1;
            for (int i = 0; i < wordsCache.Count; i++)
            {
                if (wordsCache[i].Word.Equals (word, StringComparison.OrdinalIgnoreCase))
                {
                    indexToRemove = i; 
                    break;
                }
            }

            if (indexToRemove == -1)
            {
                return false;
            }

            wordsCache.RemoveAt(indexToRemove);

            int currentNo = 1;
            foreach (WordHint wordHint in wordsCache)
            {
                wordHint.No = currentNo++;
            }

            SaveWordsToFile();
            return true;
        }
    }
}
