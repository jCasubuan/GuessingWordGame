using GuessingGameCommon;

namespace GuessingGameDataService
{
    public class TextFilePlayerDataService
    {
        string filePath = "accounts.txt";

        private void GetDataFromFile()
        {
            File.ReadAllLines(filePath);
        }

        public void AddAccounts(Player players)
        {

        }

        public List<Player> GetAccounts()
        {

        }      

        public void DeleteAccounts(Player players)
        {

        }

    }
}
