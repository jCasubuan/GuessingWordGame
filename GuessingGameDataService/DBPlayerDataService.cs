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

        private void AssignPlayerRanks(List<LeaderboardEntry> leaderboard)
        {
            if (leaderboard.Count == 0)
            {
                return;
            }

            leaderboard[0].Rank = 1; 

            for (int playerIndex = 1; playerIndex < leaderboard.Count; playerIndex++)
            {
                if (leaderboard[playerIndex].HighScore == leaderboard[playerIndex - 1].HighScore)
                {
                    leaderboard[playerIndex].Rank = leaderboard[playerIndex - 1].Rank;
                }
                else
                {
                    leaderboard[playerIndex].Rank = playerIndex + 1;
                }
            }
        }

        //CREATE
        public bool RegisterPlayer(Player player)
        {
            string insertStatement = "INSERT INTO Players (FullName, UserName, Password, Scores, HighScore, LastCompletedLevel) " +
                                     "VALUES (@FullName, @UserName, @Password, @Scores, @HighScore, @LastCompletedLevel)";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection))
            {
                insertCommand.Parameters.AddWithValue("@FullName", player.FullName);
                insertCommand.Parameters.AddWithValue("@UserName", player.UserName);
                insertCommand.Parameters.AddWithValue("@Password", player.Password);
                insertCommand.Parameters.AddWithValue("@Scores", player.Scores);
                insertCommand.Parameters.AddWithValue("@HighScore", player.HighScore);
                insertCommand.Parameters.AddWithValue("@LastCompletedLevel", player.LastCompletedLevel);

                sqlConnection.Open();
                int rowsAffected = insertCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        //READ
        public bool PlayerExists(string userName)
        {
            string query = "SELECT COUNT(*) FROM Players WHERE UserName = @UserName";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@UserName", userName);

                sqlConnection.Open();
                int count = (int)command.ExecuteScalar(); 
                return count > 0; 
            }

        }

        public int GetPlayerScore(string userName)
        {
            string query = "SELECT Scores FROM Players WHERE UserName = @UserName";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                sqlConnection.Open();
                object result = command.ExecuteScalar();

                if (result == null)
                {
                    return 0;
                }
                return Convert.ToInt32(result);
            }
        }

        public int GetLastCompletedLevel(string userName)
        {
            string query = "SELECT LastCompletedLevel FROM Players WHERE UserName = @UserName";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@UserName", userName );
                sqlConnection.Open();
                object result = command.ExecuteScalar();

                if (result == null)
                {
                    return 0;
                }
                return Convert.ToInt32(result);
            }

        }

        public bool GetPlayerByCredentials(string userName, string password)
        {
            string query = "SELECT COUNT(*) FROM Players WHERE UserName = @UserName AND Password = @Password";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand( query, sqlConnection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);

                sqlConnection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        public List<LeaderboardEntry> GetLeaderboard()
        {
            string query = "SELECT UserName, HighScore FROM Players WHERE HighScore > 0 ORDER BY HighScore DESC";
            var leaderboard = new List<LeaderboardEntry>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string userName = reader["UserName"].ToString();
                            int highScore = Convert.ToInt32(reader["HighScore"]);

                            leaderboard.Add(new LeaderboardEntry(userName, highScore));
                        }
                    }
                }
            }
            AssignPlayerRanks(leaderboard);
            return leaderboard;
        }

        //UPDATE
        public void AddToPlayerScore(string userName, int pointsToAdd)
        {
            string query = @"
                            UPDATE Players 
                            SET Scores = Scores + @PointsToAdd,
                                HighScore = CASE 
                                    WHEN (Scores + @PointsToAdd) > HighScore 
                                    THEN (Scores + @PointsToAdd) 
                                    ELSE HighScore 
                                END 
                            WHERE UserName = @UserName";


            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand( query, sqlConnection) )
            {
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@PointsToAdd", pointsToAdd);
                sqlConnection.Open ();
                command.ExecuteNonQuery();
            }
        }  

        public void UpdatePlayerLevelProgress(string userName, int lastCompletedLevel)
        {
            string query = "UPDATE Players SET LastCompletedLevel = @LastCompletedLevel WHERE UserName = @UserName AND LastCompletedLevel < @LastCompletedLevel";
            
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query,sqlConnection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@LastCompletedLevel", lastCompletedLevel);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void ResetPlayerScore(string userName)
        {
            string query = "UPDATE Players SET Scores = 0 WHERE UserName = @UserName";

            using (SqlConnection sqlConnection = new SqlConnection( connectionString))
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@UserName", userName);

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
