using GuessingGameCommon;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public class DBAdminDataService : IAdminDataService
    {
        static string connectionString
            = "Data Source = LAPTOP-NG-AGGIN\\SQLEXPRESS; Initial Catalog=GuessingWordGame; Integrated Security=True; TrustServerCertificate=True;";

        SqlConnection sqlConnection;

        public DBAdminDataService()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        //--- READ ---
        public bool GetAdminAccount(AdminAccount adminAccount)
        {
            string query = @"SELECT COUNT(1) FROM AdminAccounts WHERE Username = @Username AND Password = @Password";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Username", adminAccount.Username);
                    command.Parameters.AddWithValue("@Password", adminAccount.Password);

                    sqlConnection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();

            string query = "SELECT PlayerId, Username, FullName, Scores, HighScore FROM Players";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Player player = new Player
                            {
                                PlayerId = Convert.ToInt32(reader["PlayerId"]),
                                UserName = reader["Username"].ToString(),
                                FullName = reader["FullName"].ToString(),
                                Scores = Convert.ToInt32(reader["Scores"]),
                                HighScore = Convert.ToInt32(reader["HighScore"])
                            };
                            players.Add(player);
                        }
                    }
                }
            }

            return players;
        }

        public Player SearchById(int playerId)
        {
            Player foundPlayer = null; 

            string query = "SELECT * FROM Players WHERE PlayerId = @PlayerId"; 

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@PlayerId", playerId); 
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) 
                        {
                            foundPlayer = new Player
                            {
                                PlayerId = Convert.ToInt32(reader["PlayerId"]),
                                UserName = reader["Username"].ToString(),
                                FullName = reader["FullName"].ToString(),
                                Scores = Convert.ToInt32(reader["Scores"]),
                                HighScore = Convert.ToInt32(reader["HighScore"]),
                                LastCompletedLevel = Convert.ToInt32(reader["LastCompletedLevel"])
                            };
                        }
                    }
                }
            }
            return foundPlayer; 
        }

        public Player SearchByUsername(string userName)
        {
            Player foundPlayer = null;
            string query = "SELECT * FROM Players WHERE Username = @Username";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Username", userName);
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            foundPlayer = new Player
                            {
                                PlayerId = Convert.ToInt32(reader["PlayerId"]),
                                UserName = reader["Username"].ToString(),
                                FullName = reader["FullName"].ToString(),
                                Scores = Convert.ToInt32(reader["Scores"]),
                                HighScore = Convert.ToInt32(reader["HighScore"]),
                                LastCompletedLevel = Convert.ToInt32(reader["LastCompletedLevel"])
                            };
                        }
                    }
                }
            }
            return foundPlayer;
        }

        //--- DELETE
        public bool DeletePlayer(string userName)
        {
            string deleteQuery = "DELETE FROM Players WHERE UserName = @Username";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection))
                {
                    deleteCommand.Parameters.AddWithValue("@Username", userName);

                    sqlConnection.Open();
                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        
    }
}
