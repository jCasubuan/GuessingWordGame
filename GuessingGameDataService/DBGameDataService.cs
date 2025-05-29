using GuessingGameCommon;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public class DBGameDataService : IGameDataService
    {
        static string connectionString
            = "Data Source = LAPTOP-NG-AGGIN\\SQLEXPRESS; Initial Catalog=GuessingWordGame; Integrated Security=True; TrustServerCertificate=True;";

        SqlConnection sqlConnection;

        public DBGameDataService()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        private bool DoesWordExist(string word)
        {
            string query = "SELECT COUNT(*) FROM WordHints WHERE Word = @Word";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Word", word);
                    sqlConnection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void ReSequenceAllWords(SqlConnection sqlConnection)
        {
            List<WordHint> currentWords = new List<WordHint>();
            string selectQuery = "SELECT ID, No, Word, Hint, Difficulty FROM WordHints ORDER BY ID";

            using (SqlCommand selectCommand = new SqlCommand(selectQuery, sqlConnection))
            {
                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        currentWords.Add(new WordHint
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            No = Convert.ToInt32(reader["No"]),
                            Word = reader["Word"].ToString(),
                            Hint = reader["Hint"].ToString(),
                            Difficulty = reader["Difficulty"].ToString()
                        });
                    }
                }
            }

            UpdateQuery(sqlConnection, currentWords);
        }

        private void UpdateQuery(SqlConnection sqlConnection, List<WordHint> currentWords)
        {
            int currentNo = 1;
            foreach (var wordHint in currentWords)
            {
                wordHint.No = currentNo++;
            }

            using (SqlTransaction transaction = sqlConnection.BeginTransaction())
            {
                string query = "UPDATE WordHints SET No = @No WHERE ID = @ID";

                using (SqlCommand updateCommand = new SqlCommand(query, sqlConnection, transaction))
                {
                    updateCommand.Parameters.Add("@No", System.Data.SqlDbType.Int);
                    updateCommand.Parameters.Add("@ID", System.Data.SqlDbType.Int);

                    foreach (var wordHint in currentWords)
                    {
                        updateCommand.Parameters["@No"].Value = wordHint.No;
                        updateCommand.Parameters["@ID"].Value = wordHint.ID;
                        updateCommand.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
            }
        }

        //--- CREATE ---
        public bool AddWordHint(WordHint wordHint)
        {   
            if (DoesWordExist(wordHint.Word))
            {
                return false;
            }

            string insertStatement = "INSERT INTO WordHints (Word, Hint, Difficulty)" +
                                     "VALUES (@Word, @Hint, @Difficulty)";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection))
                {
                    insertCommand.Parameters.AddWithValue("@Word", wordHint.Word);
                    insertCommand.Parameters.AddWithValue("@Hint", wordHint.Hint);
                    insertCommand.Parameters.AddWithValue("@Difficulty", wordHint.Difficulty);

                    sqlConnection.Open();
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        ReSequenceAllWords(sqlConnection);
                        return true;
                    }
                    return false;
                }
            }
            
        }

        //--- READ ---
        public IReadOnlyList<WordHint> GetAllWordHints()
        {
            var wordHints = new List<WordHint>();
            string query = "SELECT ID, No, Word, Hint, Difficulty FROM WordHints ORDER BY No";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            WordHint wordHint = new WordHint
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                No = Convert.ToInt32(reader["No"]),
                                Word = reader["Word"].ToString(),
                                Hint = reader["Hint"].ToString(),
                                Difficulty = reader["Difficulty"].ToString()
                            };
                            wordHints.Add(wordHint);
                        }
                    }
                }
            }
            return wordHints.AsReadOnly();

        }

        public WordHint SearchForWord(string word)
        {
            WordHint foundWord = null;
            string query = "SELECT ID, No, Word, Hint, Difficulty FROM WordHints WHERE LOWER(Word) = LOWER(@Word)";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Word", word);
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            foundWord = new WordHint
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                No = Convert.ToInt32(reader["No"]),
                                Word = reader["Word"].ToString(),
                                Hint = reader["Hint"].ToString(),
                                Difficulty = reader["Difficulty"].ToString()
                            };
                        }
                    }
                }
            }
            return foundWord;
        }

        //--- UPDATE ---
        public bool UpdateWord(string oldWord, WordUpdateRequest updateRequest)
        {
            string updateStatement = @"UPDATE WordHints
                                       SET Word = @NewWord, Hint = @NewHint, Difficulty = @NewDifficulty
                                       WHERE LOWER(Word) = LOWER(@OldWord)";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection))
                {
                    updateCommand.Parameters.AddWithValue("@NewWord", updateRequest.NewWord);
                    updateCommand.Parameters.AddWithValue("@NewHint", updateRequest.NewHint);
                    updateCommand.Parameters.AddWithValue("@NewDifficulty", updateRequest.NewDifficulty);
                    updateCommand.Parameters.AddWithValue("@OldWord", oldWord);

                    sqlConnection.Open();
                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        //--- DELETE ---
        public bool DeleteWord(string word)
        {
            string deleteStatement = "DELETE FROM WordHints WHERE LOWER(Word) = LOWER (@Word)";

            using (SqlConnection sqlConnection = new SqlConnection (connectionString))
            {
                using (SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection))
                {                  
                        deleteCommand.Parameters.AddWithValue("@Word", word);

                        sqlConnection.Open();
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            ReSequenceAllWords(sqlConnection);
                            return true;
                        }
                        else
                        {
                            return false;
                        }                    
                }
            }
        }

 
    }
}
