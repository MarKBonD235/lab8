using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryBondarenko
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class AuthenticationManager
    {
        public string ConnectionString { get; private set; }
        public static string Pass { get; set; }
        public static bool UserFound { get; set; }

        public AuthenticationManager(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public User AuthenticateUser(string login, string password)
        {
            string[] sotrID = new string[0];
            string[] loginS = new string[0];
            string[] passwordS = new string[0];
            string[] nameS = new string[0];
            string[] surnameS = new string[0];
            string[] patronymicS = new string[0];
            string[] roleS = new string[0];

            string[] ClientID = new string[0];
            string[] loginK = new string[0];
            string[] passwordK = new string[0];
            string[] nameK = new string[0];
            string[] surnameK = new string[0];
            string[] patronymicK = new string[0];

            // Проверка по таблице Сотрудники
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "SELECT s.ID_сотрудника, s.Логин, s.Пароль, s.Имя, s.Фамилия, s.Отчество, r.Роль " +
                             "FROM Сотрудники s JOIN Роли r ON s.Роль = r.ID_Роли";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> IDlist = new List<string>();
                        List<string> loginList = new List<string>();
                        List<string> passwordList = new List<string>();
                        List<string> nameList = new List<string>();
                        List<string> surnameList = new List<string>();
                        List<string> patronymicList = new List<string>();
                        List<string> roleList = new List<string>();

                        while (reader.Read())
                        {
                            IDlist.Add(reader["ID_сотрудника"].ToString());
                            loginList.Add(reader["Логин"].ToString());
                            passwordList.Add(reader["Пароль"].ToString());
                            nameList.Add(reader["Имя"].ToString());
                            surnameList.Add(reader["Фамилия"].ToString());
                            patronymicList.Add(reader["Отчество"].ToString());
                            roleList.Add(reader["Роль"].ToString());
                        }

                        sotrID = IDlist.ToArray();
                        loginS = loginList.ToArray();
                        passwordS = passwordList.ToArray();
                        nameS = nameList.ToArray();
                        surnameS = surnameList.ToArray();
                        patronymicS = patronymicList.ToArray();
                        roleS = roleList.ToArray();
                    }
                }
            }

            // Проверка по таблице Клиенты
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "SELECT ID_Клиента, Логин, Пароль, Имя, Фамилия, Отчество FROM Клиенты";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> IDList = new List<string>();
                        List<string> loginList = new List<string>();
                        List<string> passwordList = new List<string>();
                        List<string> nameList = new List<string>();
                        List<string> surnameList = new List<string>();
                        List<string> patronymicList = new List<string>();

                        while (reader.Read())
                        {
                            IDList.Add(reader["ID_Клиента"].ToString());
                            loginList.Add(reader["Логин"].ToString());
                            passwordList.Add(reader["Пароль"].ToString());
                            nameList.Add(reader["Имя"].ToString());
                            surnameList.Add(reader["Фамилия"].ToString());
                            patronymicList.Add(reader["Отчество"].ToString());
                        }

                        ClientID = IDList.ToArray();
                        loginK = loginList.ToArray();
                        passwordK = passwordList.ToArray();
                        nameK = nameList.ToArray();
                        surnameK = surnameList.ToArray();
                        patronymicK = patronymicList.ToArray();
                    }
                }
            }

            // Проверка сотрудников
            for (int i = 0; i < loginS.Length; i++)
            {
                if (loginS[i] == login && passwordS[i] == password)
                {

                    return new User
                    {
                        Id = int.Parse(sotrID[i]),
                        Login = login,
                        Name = nameS[i],
                        Surname = surnameS[i],
                        Patronymic = patronymicS[i],
                        Role = roleS[i],
                    };
                    Pass = passwordS[i].ToString();
                }
            }
            
            // Проверка клиентов
            for (int i = 0; i < loginK.Length; i++)
            {
                if (loginK[i] == login && passwordK[i] == password)
                {
                    return new User
                    {
                        Id = int.Parse(ClientID[i]),
                        Login = login,
                        Name = nameK[i],
                        Surname = surnameK[i],
                        Patronymic = patronymicK[i],
                        Role = "Клиент"
                    };
                }
            }

            return null;
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Role { get;set; }
    }


}
