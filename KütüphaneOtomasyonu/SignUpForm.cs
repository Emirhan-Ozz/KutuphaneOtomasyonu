using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KütüphaneOtomasyonu
{
    public partial class SignUpForm : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KPLDU1P\\SQLEXPRESS;Initial Catalog=KütüphaneDB;Integrated Security=True;Encrypt=False;");

        public SignUpForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || maskedTextBox1.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }

            try
            {
                con.Open();

                SqlCommand com = new SqlCommand("INSERT INTO Users(user_name, user_password, is_admin) VALUES(@name, @password, 0)", con);
                com.Parameters.AddWithValue("@name", textBox1.Text);
                com.Parameters.AddWithValue("@password", maskedTextBox1.Text);

                com.ExecuteNonQuery();

                MessageBox.Show("Kayıt başarılı!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SignUpForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
