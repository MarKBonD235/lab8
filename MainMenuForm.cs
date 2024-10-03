using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace Практика_Зад5
{
    public partial class MainMenuForm : Form
    {
        private string userRole;
        private SqlConnection connection;
        private string connectionString;

        public MainMenuForm()
        {
            InitializeComponent();
            this.userRole = ProfileData.Role;

            connectionString = @"Data Source=VLADIK;Initial Catalog=БондаренкоПрактика;Integrated Security=True";
            connection = new SqlConnection(connectionString);

            dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;

            searcBox.TextChanged += SearchAllColumns;
            update_label();
        }


        // Обработчик события при начале редактирования ячейки
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }

        // Обработчик события при окончании редактирования ячейки
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Проверить, изменилось ли значение
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag)
                {
                    // Получить ID_запроса из текущей строки
                    int requestId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID_запроса"].Value);

                    // Получить имя столбца, который был изменен
                    string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

                    // Получить новое значение из ячейки
                    object newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                    // Обновить значение в базе данных
                    UpdateRequest(requestId, columnName, newValue);

                    FillDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        // Метод для обновления данных в базе данных
        private void UpdateRequest(int requestId, string columnName, object newValue)
        {
            try
            {
                connection.Open();

                string query = $"UPDATE Заявки SET {columnName} = @NewValue WHERE ID_запроса = @RequestId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@RequestId", requestId);
                command.Parameters.AddWithValue("@NewValue", newValue);

                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
        //Загрузка формы
        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            if (userRole == "Мастер")
            {
                menuBtn.Text = "Комментарии";
                addBtn.Visible = false;
                delBtn.Visible = false;
            }
            else if (userRole == "Оператор")
            {
                menuBtn.Text = "История входа";
                addBtn.Visible = false;
                delBtn.Visible = true;
            }
            else if(userRole == "Менеджер")
            {
                menuBtn.Text = "Просмотр QR";
                addBtn.Visible = false;
                delBtn.Visible = false;
            }
            else
            {
                addBtn.Enabled = true;
                menuBtn.Text = "Просмотр QR";
            }
            roleBox.Text = userRole;

            FillDataGridView();
            update_label();
        }
        //Динамическое обновление количества запросов
        private void update_label()
        {
            int count = dataGridView1.RowCount;
            label1.Text = $"Записей: {count}";
        }
        //Заполнение datagridview данными
        private void FillDataGridView()
        {
            if(userRole == "Клиент") //Делаю для клиента урезанное отображение данных
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("Select z.ID_Запроса, z.ДатаНачала, m.Наименование, z.Проблема, z.Статус From Заявки z JOIN МодельОргтехники m ON z.ID_Модели = m.ID_модели", connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet, "Заявки");

                    dataGridView1.DataSource = dataSet.Tables["Заявки"];

                    connection.Close();

                    update_label();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }
            else
            {

                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(@"
                                                            SELECT 
                                                                z.ID_Запроса, 
                                                                z.ДатаНачала, 
                                                                m.Наименование, 
                                                                z.Проблема, 
                                                                z.Статус, 
                                                                z.ДатаОкончания, 
                                                                COALESCE(zap.Наименование, 'Не указано') AS ЗапасныеДетали, 
                                                                COALESCE(s.Фамилия, 'Не указано') AS ФамилияМастера, 
                                                                COALESCE(k.Фамилия, 'Не указано') AS ФамилияКлиента
                                                            FROM 
                                                                Заявки z
                                                            JOIN 
                                                                МодельОргтехники m ON z.ID_Модели = m.ID_модели
                                                            LEFT JOIN 
                                                                ЗапасныеДетали zap ON z.ЗапасныеДетали = zap.ID_детали
                                                            LEFT JOIN 
                                                                Сотрудники s ON z.ID_мастера = s.ID_сотрудника
                                                            LEFT JOIN 
                                                                Клиенты k ON z.ID_клиента = k.ID_клиента", connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet, "Заявки");

                    dataGridView1.DataSource = dataSet.Tables["Заявки"];

                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }

            }
        }
        //Кнопка выхода из аккаунта
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
        //Меню
        private void menuBtn_Click(object sender, EventArgs e)
        {
            if (userRole == "Мастер")
            {
                CommentsForm commentsForm = new CommentsForm();
                this.Close();
                commentsForm.Show();
            }else if(userRole == "Оператор")
            {
                HistoriLoginForm historiLoginForm = new HistoriLoginForm();
                historiLoginForm.ShowDialog();
            }else if(userRole == "Менеджер")
            {
                QrCodeForm qrCodeForm = new QrCodeForm();
                qrCodeForm.ShowDialog();
            }
            else
            {
                QrCodeForm qrCodeForm = new QrCodeForm();
                qrCodeForm.ShowDialog();

            }
        }
        //Кнопка добавления заявки
        private void addBtn_Click(object sender, EventArgs e)
        {
            AddRequestForm addRequestForm = new AddRequestForm();
            addRequestForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillDataGridView();
            dataGridView1.Refresh();
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            bool youReady = false;

            DialogResult result = MessageBox.Show("Вы уверены?","Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                youReady = true;
            }

            if (youReady)
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите строку для удаления", "Ошибка");
                    return;
                }

                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_запроса"].Value);

                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Заявки WHERE ID_запроса = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                    connection.Close();

                    FillDataGridView();
                    dataGridView1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Ок");
            }
        }

        // Метод для поиска по всем столбцам
        private void SearchAllColumns(object sender, EventArgs e)
        {
            string searchText = searcBox.Text;
            if(userRole == "Клиент")
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT  z.ДатаНачала, m.Наименование, z.Проблема, z.Статус From Заявки z JOIN МодельОргтехники m ON z.ID_Модели = m.ID_модели WHERE 
                                  ДатаНАчала LIKE '%' + @SearchText + '%' OR
                                  Наименование LIKE '%' + @SearchText + '%' OR
                                  Проблема LIKE '%' + @SearchText + '%' OR
                                  Статус LIKE '%' + @SearchText + '%'";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@SearchText", searchText);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet, "Заявки");

                    dataGridView1.DataSource = dataSet.Tables["Заявки"];

                    connection.Close();
                    update_label();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }

            }
            else
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT * FROM Заявки WHERE 
                                  ID_запроса LIKE '%' + @SearchText + '%' OR
                                  ДатаНАчала LIKE '%' + @SearchText + '%' OR
                                  ID_модели LIKE '%' + @SearchText + '%' OR
                                  Проблема LIKE '%' + @SearchText + '%' OR
                                  Статус LIKE '%' + @SearchText + '%' OR
                                  ДатаОкончания LIKE '%' + @SearchText + '%' OR
                                  ЗапасныеДетали LIKE '%' + @SearchText + '%' OR
                                  ID_мастера LIKE '%' + @SearchText + '%' OR
                                  ID_клиента LIKE '%' + @SearchText + '%'";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@SearchText", searchText);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet, "Заявки");

                    dataGridView1.DataSource = dataSet.Tables["Заявки"];

                    connection.Close();
                    update_label();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }

            }
        }

        private void profileBtn_Click(object sender, EventArgs e)
        {
            ChechProfileForm profileForm = new ChechProfileForm();
            profileForm.ShowDialog();
        }
    }
}
