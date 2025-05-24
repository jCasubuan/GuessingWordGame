using GuessingGameCommon;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;

namespace GuessingGameDataService
{
    public class DBPlayerDataService : IPlayerDataService
    {
        static string connectionString
            = "Data Source = LAPTOP-NG-AGGIN\\SQLEXPRESS; Initial Catalog=GuessingWordGame; Integrated Security=True; TrustServerCertificate=True;";

        SqlConnection sqlConnection;

        public DBPlayerDataService()
        {
            sqlConnection = new SqlConnection(connectionString);    
        }

        //CREATE
        public bool RegisterPlayer(Player player)
        {
            var insertStatement = "INSERT INTO Players (FullName, UserName, Password, Scores, HighScore, LastCompletedLevel )" +
                                   "VALUES (@FullName, @UserName, @Password, @Scores, @HighScore, @LastCompletedLevel)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@FullName", player.FullName);
            insertCommand.Parameters.AddWithValue("@UserName", player.UserName);
            insertCommand.Parameters.AddWithValue("@Password", player.Password);
            insertCommand.Parameters.AddWithValue("@Scores", player.Scores);
            insertCommand.Parameters.AddWithValue("@HighScore", player.HighScore);
            insertCommand.Parameters.AddWithValue("@LastCompletedLevel", player.LastCompletedLevel);
            sqlConnection.Open();

            int rowsAffected = insertCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return rowsAffected > 0;

        }

        //READ
        public bool PlayerExists(string userName)
        {
            const string query = "SELECT COUNT(*) FROM Players WHERE UserName = @UserName";

            using (var sqlConnection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@UserName", userName);

                sqlConnection.Open();
                int count = (int)command.ExecuteScalar(); 
                return count > 0; 
            }

        }

        public int GetPlayerScore(string userName)
        {
            throw new NotImplementedException();
        }

        public int GetLastCompletedLevel(string userName)
        {
            throw new NotImplementedException();
        }

        public bool GetPlayerByCredentials(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public List<LeaderboardEntry> GetLeaderboard()
        {
            throw new NotImplementedException();
        }

        //UPDATE
        public void AddToPlayerScore(string userName, int pointsToAdd)
        {
            throw new NotImplementedException();
        }  

        public void UpdatePlayerLevelProgress(string usernName, int lastCompletedLevel)
        {
            throw new NotImplementedException();
        }

        public void ResetPlayerScore(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
