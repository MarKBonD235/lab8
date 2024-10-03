using System;
using System.Data;
using System.Data.SqlClient;

namespace LibraryBondarenko
{
        public class HistoryLogManager
        {
            private readonly string _connectionString;

            public HistoryLogManager(string connectionString)
            {
                _connectionString = connectionString;
            }

            // Заполнение DataSet данными из таблицы ИсторияВхода
            public DataTable GetAllLoginHistory()
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "SELECT * FROM ИсторияВхода";
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "ИсторияВхода");
                        return dataSet.Tables["ИсторияВхода"];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Ошибка при загрузке данных: " + ex.Message);
                    }
                }
            }

            // Поиск данных в таблице ИсторияВхода по указанному тексту
            public DataTable SearchLoginHistory(string searchText)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        string searchTextLogin = searchText.ToLower() switch
                        {
                            "true" => "1",
                            "false" => "0",
                            _ => searchText
                        };

                        string query = @"
                        SELECT * FROM ИсторияВхода
                        WHERE ID_входа LIKE '%' + @SearchText + '%' OR
                              Время LIKE '%' + @SearchText + '%' OR
                              Логин LIKE '%' + @SearchText + '%' OR
                              Пароль LIKE '%' + @SearchText + '%' OR
                              Авторизация LIKE '%' + @SearchTextLogin + '%'";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@SearchText", searchText);
                        command.Parameters.AddWithValue("@SearchTextLogin", searchTextLogin);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "ИсторияВхода");
                        return dataSet.Tables["ИсторияВхода"];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Ошибка при поиске данных: " + ex.Message);
                    }
                }
            }
        }
    

}
