using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;

namespace LibraryBondarenko
{

        public class CommentManager
        {
            private string _connectionString;
            public static string Comment { get; set; }
            public static string RequestID { get; set; }
            public static string MasterID { get; set; }
            public CommentManager(string connectionString)
            {
                _connectionString = connectionString;
            }

            public int AddComment(string comment, string masterID, string requestID)
            {
            Comment = comment;
            RequestID = requestID;
            MasterID = masterID;
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Комментарии (Комментарий, ID_мастера, ID_запроса) VALUES (@comment, @masterID, @requestID)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@comment", comment);
                        command.Parameters.AddWithValue("@masterID", masterID);
                        command.Parameters.AddWithValue("@requestID", requestID);

                        connection.Open();

                        try
                        {
                            return command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Ошибка при добавлении комментария: {ex.Message}");
                        }
                    }
                }
            }
        }

}
