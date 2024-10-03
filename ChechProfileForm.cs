using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Практика_Зад5
{
    public partial class ChechProfileForm : Form
    {
        private string name;
        private string surname;
        private string patronymic;
        private string role;
        private int ID;
        public ChechProfileForm()
        {
            InitializeComponent();
            this.name = ProfileData.Name;
            this.surname = ProfileData.Surname;
            this.patronymic = ProfileData.Patronymic;
            this.role = ProfileData.Role;
            this.ID = ProfileData.ID;
        }

        private void ChechProfileForm_Load(object sender, EventArgs e)
        {
            nameBox.Text = name;
            surnameBox.Text = surname;
            patronomicBox.Text = patronymic;
            roleBox.Text = role;
            IDBox.Text = ID.ToString();
        }
    }
}
