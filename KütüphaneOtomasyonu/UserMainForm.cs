using System;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace KütüphaneOtomasyonu
{
    public partial class UserMainForm : Form
    {
        public UserMainForm()
        {
            InitializeComponent();
            label1.Text = "Hoş geldin " + AppState.userName + "," + " lütfen ne işlem yapmak istediğini seç.";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BookBorrowForm frm = new BookBorrowForm();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BookReturnForm frm = new BookReturnForm();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginForm frm = new LoginForm();
            frm.Show();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void UserMainForm_Load(object sender, EventArgs e)
        {

        }
    }
}