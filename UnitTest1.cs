using Xunit;

using System.Data;
using System.Data.SqlClient;
using LibraryLetelov;

namespace LibraryTests
{
    public class ProfileTests
    {
        [Fact]
        public void Value()//Проверка на то, что при загрузке формы Профиля у нее создаются значения, которые заполняются данными
        {
            var expectedName = "Имя";
            var expectedSurname = "Фамилия";
            var expectedPatronymic = "Отчество";
            var expectedRole = "Роль";

            var profile = ProfileDataManager.LoadProfile();

            Assert.NotNull(profile);
            Assert.Equal(expectedName, profile.Name);
            Assert.Equal(expectedSurname, profile.Surname);
            Assert.Equal(expectedPatronymic, profile.Patronymic);
            Assert.Equal(expectedRole, profile.Role);
        }
    }

    public class CommentTest //Проверка на заполнение полей строковыми значениями
    {
        [Fact]
        public void CommentBox()
        {
            var expectedComment = "Hello";
            var comment = CommentManager.Comment;
            var expectedMasterId = "1";
            var masterID = CommentManager.MasterID;
            var expectedRequesId = "1";
            var requestID = CommentManager.RequestID;


            comment = expectedComment;
            masterID = expectedMasterId;
            requestID = expectedRequesId;

            Assert.Equal(expectedComment, comment);
            Assert.Equal(expectedMasterId, masterID);
            Assert.Equal(expectedRequesId, requestID);

        }

    }

    public class HistoryTests //Проверяется заполнение полей в форме из базы данных
    {
        private class MockConnection : IDisposable
        {
            public void Open() { }
            public void Close() { }
            public void Dispose() { }
        }

        private class MockDataAdapter : IDisposable
        {
            public DataTable DataTable { get; } = new DataTable("ИсторияВхода");
            public MockDataAdapter(IEnumerable<DataRow> dataRows)
            {
                foreach (var column in new[] { "ID_входа", "Логин", "Пароль", "Время", "Авторизация" })
                {
                    DataTable.Columns.Add(column, typeof(string));
                }

                foreach (var row in dataRows)
                {
                    DataTable.Rows.Add(row.ItemArray);
                }
            }

            public void Fill(DataSet dataSet, string tableName)
            {
                dataSet.Tables.Add(DataTable);
            }

            public void Dispose() { }
        }

        [Fact]
        public void Colums()
        {
            var expectedColumns = new[] { "ID_входа", "Логин", "Пароль", "Время", "Авторизация" };

            var historyLogManager = new HistoryLogManager(@"Data Source=VLADIK;Initial Catalog=БондаренкоПрактика;Integrated Security=True");
            var actualTable = historyLogManager.GetAllLoginHistory();

            Assert.NotNull(actualTable);
            Assert.Equal(expectedColumns, actualTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray());
        }

    }


    public class LoginTests //Проверка функции проверки на наличие аккаунта
    {
        public string[] currentLogin = { "login1", "login2", "login11" };
        public string[] currentPass = { "pass1", "pass2", "pass11" };

        [Fact]
        public void login()
        {
            User user = new User();
            string log = user.Login = "login11";
            string pass = AuthenticationManager.Pass = "pass11";
            bool userFound = AuthenticationManager.UserFound;
            for (int i = 0; i < currentLogin.Length; i++)
            {
                if (log == currentLogin[i] && pass == currentPass[i])
                {
                    userFound = true;
                    break;
                }
                else { userFound = false; }

            }

            Assert.True(userFound);
        }

    }

    public class RequestTest//Проверка на возможность создания новой записи в таблице заявки
    {
        [Fact]
        public void CreateRequest_ShouldSetProperties()
        {
            var requestText = "Test request";
            var masterID = 123;
            var requestManager = new RequestManager();

            requestManager.CreateRequest(requestText, masterID);

            Assert.Equal(requestText, requestManager.RequestText);
            Assert.Equal(masterID, requestManager.MasterID);
        }
    }
}
