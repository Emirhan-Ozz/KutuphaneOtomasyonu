using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KütüphaneOtomasyonu
{
    public partial class LoginForm : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KPLDU1P\\SQLEXPRESS;Initial Catalog=KütüphaneDB;Integrated Security=True;Encrypt=False;");

        public LoginForm()
        {
            InitializeComponent();

            maskedTextBox1.KeyPress += maskedTextBox1_KeyPress;
            maskedTextBox2.KeyPress += maskedTextBox2_KeyPress;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form ExistingForm = Application.OpenForms["SignUpForm"];

            if (ExistingForm != null)
            {
                ExistingForm.Focus();
                ExistingForm.WindowState = FormWindowState.Normal;
            }
            else
            {
                SignUpForm signUpForm = new SignUpForm();
                signUpForm.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Trim() == "" || maskedTextBox2.Text.Trim() == "")
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz!");
                return;
            }

            try
            {
                con.Open();

                SqlCommand com = new SqlCommand("SELECT * FROM Users WHERE user_name = @name AND user_password = @password", con);
                com.Parameters.AddWithValue("@name", maskedTextBox1.Text.Trim());
                com.Parameters.AddWithValue("@password", maskedTextBox2.Text.Trim());

                SqlDataReader dr = com.ExecuteReader();

                if (dr.Read())
                {
                    AppState.userID = dr.GetInt32(0);
                    AppState.userName = dr.GetString(1);
                    AppState.isAdmin = dr.GetBoolean(3);

                    if (!AppState.isAdmin)
                    {
                        UserMainForm frm = new UserMainForm();
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
                        AdminMainForm frm = new AdminMainForm();
                        frm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Hatalı Giriş Yaptınız!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void maskedTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}