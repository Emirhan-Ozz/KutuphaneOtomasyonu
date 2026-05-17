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


namespace KütüphaneOtomasyonu
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KPLDU1P\\SQLEXPRESS;Initial Catalog=KütüphaneDB;Integrated Security=True;Encrypt=False;");


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
            con.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM Users WHERE user_name = @name AND user_password = @password", con);
            com.Parameters.AddWithValue("@name", maskedTextBox1.Text);
            com.Parameters.AddWithValue("@password", maskedTextBox2.Text);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                Console.WriteLine($"{dr.GetInt32(0)}\t{dr.GetString(1)}\t{dr.GetBoolean(3)}");
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
            con.Close();
            
        }
    }
}
