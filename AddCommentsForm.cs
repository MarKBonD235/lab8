using System;
using System.Windows.Forms;
using LibraryBondarenko; 

namespace Практика_Зад5
{
    public partial class AddCommentsForm : Form
    {
        private int id;
        private CommentManager commentManager;
        
        public AddCommentsForm()
        {
            InitializeComponent();
            this.id = ProfileData.ID;

            // Инициализация CommentManager с connectionString
            string connectionString = @"Data Source=VLADIK;Initial Catalog=БондаренкоПрактика;Integrated Security=True";
            commentManager = new CommentManager(connectionString);
        }

        // Кнопка добавления комментария
        public void button1_Click(object sender, EventArgs e)
        {
            while (string.IsNullOrEmpty(commentBox.Text))
            {
                MessageBox.Show("Заполните поле комментарий", "Внимание", MessageBoxButtons.OK);
                return;
            }
            //while (int.TryParse(requestIDBox.Text, out id))
            //{
            //    MessageBox.Show("Заполните поле ID_запроса");
            //    return;
            //}

            try
            {
                int rowsAffected = commentManager.AddComment(commentBox.Text, id.ToString(), requestIDBox.Text);
                MessageBox.Show($"Добавлено {rowsAffected} строк.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}");
            }
        }

        private void AddCommentsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
