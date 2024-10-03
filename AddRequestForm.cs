using LibraryBondarenko;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Практика_Зад5
{
    public partial class AddRequestForm : Form
    {
        private DatabaseManager databaseManager;
        private int modelID;

        public AddRequestForm()
        {
            InitializeComponent();
            string connectionString = @"Data Source=VLADIK;Initial Catalog=БондаренкоПрактика;Integrated Security=True";
            databaseManager = new DatabaseManager(connectionString);
            modelList.SelectedIndexChanged += ModelID_changed;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(problemBox.Text))
            {
                MessageBox.Show("Запоните поле описание проблемы!");
                return;
            }
            if (string.IsNullOrEmpty(modelList.Text))
            {
                MessageBox.Show("Выберите модель");
                return;
            }

            string query = "INSERT INTO Заявки (ДатаНачала,ID_Модели, Проблема, ID_клиента, Статус) VALUES (@date, @id_модели, @problem, @ID, @status)";
            SqlParameter[] parameters = {
                new SqlParameter("@date", DateTime.Now),
                new SqlParameter("@id_модели", modelID),
                new SqlParameter("@problem", problemBox.Text),
                new SqlParameter("@ID", ProfileData.ID),
                new SqlParameter("@status", "Новая заявка")
            };

            try
            {
                databaseManager.ExecuteNonQuery(query, parameters);
                MessageBox.Show("Запись добавлена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}");
            }
        }

        private void AddRequestForm_Load(object sender, EventArgs e)
        {
            string query = "SELECT Наименование FROM МодельОргтехники";
            try
            {
                using (SqlDataReader reader = databaseManager.ExecuteReader(query, new SqlParameter[0]))
                {
                    while (reader.Read())
                    {
                        modelList.Items.Add(reader["Наименование"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void ModelID_changed(object sender, EventArgs e)
        {
            string modelName = modelList.SelectedItem.ToString();
            string query = "SELECT ID_модели FROM МодельОргтехники WHERE Наименование = @modelName";
            SqlParameter[] parameters = {
                new SqlParameter("@modelName", modelName)
            };

            try
            {
                using (SqlDataReader reader = databaseManager.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        modelID = Convert.ToInt32(reader["ID_модели"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}
