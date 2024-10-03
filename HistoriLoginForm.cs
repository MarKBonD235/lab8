using System;
using System.Data;
using System.Windows.Forms;
using LibraryBondarenko;

namespace Практика_Зад5
{
    public partial class HistoriLoginForm : Form
    {
        private readonly HistoryLogManager _historyLogManager;

        public HistoriLoginForm()
        {
            InitializeComponent();

            // Инициализация HistoryLogManager с строкой подключения
            string connectionString = @"Data Source=VLADIK;Initial Catalog=БондаренкоПрактика;Integrated Security=True";
            _historyLogManager = new HistoryLogManager(connectionString);

            searchBox.TextChanged += SearchAllColumns;
        }

        // Загрузка формы
        private void HistoriLoginForm_Load(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        // Заполнение DataGridView данными
        private void FillDataGridView()
        {
            try
            {
                DataTable dataTable = _historyLogManager.GetAllLoginHistory();
                dataGridView1.DataSource = dataTable;
                UpdateRecordCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        // Поиск по всем колонкам
        private void SearchAllColumns(object sender, EventArgs e)
        {
            string searchText = searchBox.Text;

            try
            {
                DataTable dataTable = _historyLogManager.SearchLoginHistory(searchText);
                dataGridView1.DataSource = dataTable;
                UpdateRecordCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        // Динамическое обновление количества записей
        private void UpdateRecordCount()
        {
            int count = dataGridView1.RowCount;
            label1.Text = $"Записей: {count}";
        }
    }
}
