using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KütüphaneOtomasyonu
{
    public partial class AdminMainForm : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KPLDU1P\\SQLEXPRESS;Initial Catalog=KütüphaneDB;Integrated Security=True;Encrypt=False;");

        public AdminMainForm()
        {
            InitializeComponent();
            label1.Text = "Hoş geldiniz " + AppState.userName + "," + " lütfen ne yapmak istediğinizi seçin.";   
        }

        private void AdminMainForm_Load(object sender, EventArgs e)
        {
            Listele();
        }

        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Books", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || maskedTextBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }

            try
            {
                con.Open();

                SqlCommand com = new SqlCommand("INSERT INTO Books(book_name, publish_year, author) VALUES(@name, @year, @author)", con);
                com.Parameters.AddWithValue("@name", textBox1.Text.Trim());
                com.Parameters.AddWithValue("@year", maskedTextBox1.Text.Trim());
                com.Parameters.AddWithValue("@author", textBox2.Text.Trim());

                com.ExecuteNonQuery();

                MessageBox.Show("Kitap başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                Listele();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silinecek kitabı seçin!");
                return;
            }

            try
            {
                int bookId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                con.Open();

                SqlCommand com = new SqlCommand("DELETE FROM Books WHERE book_id = @id", con);
                com.Parameters.AddWithValue("@id", bookId);

                com.ExecuteNonQuery();

                MessageBox.Show("Kitap başarıyla silindi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                Listele();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["book_name"].Value.ToString();
                maskedTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["publish_year"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["author"].Value.ToString();
            }
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen güncellenecek kitabı seçin!");
                return;
            }

            if (textBox1.Text.Trim() == "" || maskedTextBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }


            try
            {
                int bookId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                con.Open();

                SqlCommand com = new SqlCommand("UPDATE Books SET book_name = @name, publish_year = @year, author = @author WHERE book_id = @id", con);
                com.Parameters.AddWithValue("@name", textBox1.Text.Trim());
                com.Parameters.AddWithValue("@year", maskedTextBox1.Text.Trim());
                com.Parameters.AddWithValue("@author", textBox2.Text.Trim());
                com.Parameters.AddWithValue("@id", bookId);

                com.ExecuteNonQuery();

                MessageBox.Show("Kitap başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                Listele();
            }
        }
    }
}