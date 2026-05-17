using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KütüphaneOtomasyonu
{
    public partial class BookBorrowForm : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KPLDU1P\\SQLEXPRESS;Initial Catalog=KütüphaneDB;Integrated Security=True;Encrypt=False;");

        public BookBorrowForm()
        {
            InitializeComponent();
            Listele();
        }

        private void BookBorrowForm_Load(object sender, EventArgs e)
        {
            Listele();
        }

        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Books WHERE borrower_id IS NULL", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen ödünç alınacak kitabı seçin!");
                return;
            }

            try
            {
                int bookId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                con.Open();

                SqlCommand com = new SqlCommand("UPDATE Books SET borrower_id = @myID, borrow_date = GETDATE() WHERE book_id = @bookId", con);
                com.Parameters.AddWithValue("@bookId", bookId);
                com.Parameters.AddWithValue("@myId", AppState.userID);
                com.ExecuteNonQuery();
                MessageBox.Show("Kitap başarıyla ödünç alındı.");
                Listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Listele();
            }
            finally
            {
                con.Close();
                Listele();
            }
        }

        private void BookBorrowForm_Load_1(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'kütüphaneDBDataSet.Books' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.booksTableAdapter.Fill(this.kütüphaneDBDataSet.Books);

        }
    }
}