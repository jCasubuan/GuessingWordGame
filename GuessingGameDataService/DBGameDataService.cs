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

        private void ReSequenceAllWords(SqlConnection sqlConnection)
        {
            try
            {
                List<WordHint> currentWords = new List<WordHint>();
                string selectQuery = "SELECT ID, No, Word, Hint, Difficulty FROM WordHints ORDER BY ID";
                using (var selectCommand = new SqlCommand(selectQuery, sqlConnection))
                {
                    using (var reader = selectCommand.ExecuteReader())
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

                int currentNo = 1;
                foreach (var wordHint in currentWords)
                {
                    wordHint.No = currentNo++;
                }

                SqlTransaction transaction = null;
                try
                {
                    transaction = sqlConnection.BeginTransaction();
                    string updateQuery = "UPDATE WordHints SET No = @No WHERE ID = @ID";
                    using (var updateCommand = new SqlCommand(updateQuery, sqlConnection, transaction))
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
                catch (Exception txEx)
                {
                    transaction?.Rollback(); 
                    throw; 
                }
            }
            catch (Exception ex)
            {
            }
        }

        //--- CREATE ---
        public bool AddWordHint(WordHint wordHint)
        {
            string insertStatement = "INSERT INTO WordHints (Word, Hint, Difficulty)" +
                                     "VALUES (@Word, @Hint, @Difficulty)";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var insertCommand = new SqlCommand(insertStatement, sqlConnection))
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

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open(); 
                        using (var reader = command.ExecuteReader())
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
                    catch (Exception ex)
                    {
                        return new List<WordHint>().AsReadOnly();
                    }
                }
            }
            return wordHints.AsReadOnly(); 
        }

        public WordHint SearchForWord(string word)
        {   
            WordHint foundWord = null;
            string query = "SELECT ID, No, Word, Hint, Difficulty FROM WordHints WHERE LOWER(Word) = LOWER(@Word)";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query,sqlConnection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Word", word);
                        sqlConnection.Open();
                        using (var reader = command.ExecuteReader())
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
                    catch (Exception ex)
                    {
                        return null;
                    }
                    
                }
            }
            return foundWord;
        }

        //--- UPDATE ---
        public bool UpdateWord(string oldWord, WordUpdateRequest updateRequest)
        {
            string updateStatement = @"
                            UPDATE WordHints
                            SET Word = @NewWord,
                                Hint = @NewHint,
                                Difficulty = @NewDifficulty
                            WHERE LOWER(Word) = LOWER(@OldWord)";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var updateCommand = new SqlCommand(updateStatement, sqlConnection))
                {
                    try
                    {
                        updateCommand.Parameters.AddWithValue("@NewWord", updateRequest.NewWord);
                        updateCommand.Parameters.AddWithValue("@NewHint", updateRequest.NewHint);
                        updateCommand.Parameters.AddWithValue("@NewDifficulty", updateRequest.NewDifficulty);
                        updateCommand.Parameters.AddWithValue("@OldWord", oldWord); 

                        sqlConnection.Open();

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return true; 
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        //--- DELETE ---
        public bool DeleteWord(string word)
        {
            string deleteStatement = "DELETE FROM WordHints WHERE LOWER(Word) = LOWER (@Word)";

            using (var sqlConnection = new SqlConnection (connectionString))
            {
                using (var deleteCommand = new SqlCommand(deleteStatement, sqlConnection))
                {
                    try
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
                    catch(Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        

        

        
    }
}
