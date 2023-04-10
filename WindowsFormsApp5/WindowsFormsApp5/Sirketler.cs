using Microsoft.Win32.SafeHandles;
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

namespace WindowsFormsApp5
{
    public partial class Sirketler : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=USER25;Initial Catalog=otelkayit;Integrated Security=True");
        DataSet ds = new DataSet();
        public Sirketler()
        {
            InitializeComponent();
        }

        private void Sirketler_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'otelkayitDataSet.Sirketler' table. You can move, or remove it, as needed.
            this.sirketlerTableAdapter.Fill(this.otelkayitDataSet.Sirketler);

        }
        public void ToList()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Sirketler", conn);
            ds.Clear();
            adapter.Fill(ds, "Sirketler");
            dataGridView1.DataSource = ds.Tables["Sirketler"];
            conn.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
        public void Güncelle()
        {
            if (label3.Text == "")
            {
                MessageBox.Show("Kayıt Seçiniz");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand komut = new SqlCommand("update Sirketler SET SirketAD = @SirketAD, SirketMail = @SirketMail,SirketTEL =@SirketTEL where SirketID = @SirketID");
                    komut.Parameters.AddWithValue("@SirketAD", textBox1.Text);
                    komut.Parameters.AddWithValue("@SirketMail", textBox2.Text);
                    komut.Parameters.AddWithValue("@SirketTEL", textBox3.Text);
                    komut.Connection = conn;
                    komut.Parameters.AddWithValue("SirketID", label4.Text);
                    komut.ExecuteNonQuery();
                    conn.Close();
                    ToList();
                }
                catch 
                {

                    MessageBox.Show("Doğru Veri Giriniz!");
                }

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO Sirketler(SirketAD,SirketMail,SirketTEL) Values(@SirketAD,@SirketMail,@SirketTEL)");
                komut.Parameters.AddWithValue("@SirketAD", textBox1.Text);
                komut.Parameters.AddWithValue("@SirketMail", textBox2.Text);
                komut.Parameters.AddWithValue("SirketTEL", textBox3.Text);
                komut.Connection = conn;
                komut.ExecuteNonQuery();
                conn.Close();
                ToList();
            }
            catch
            {

                MessageBox.Show("Doğru Veri giriniz!");
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("Delete From Sirketler where SirketID=@SirketID");
                komut.Connection = conn;
                komut.Parameters.AddWithValue("SirketID", label4.Text);
                komut.ExecuteNonQuery();
                conn.Close();
                ToList();
            }
            catch 
            {

                MessageBox.Show("Doğru Veri Giriniz!");
            }

        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ind = e.RowIndex;
                DataGridViewRow selectedRows = dataGridView1.Rows[ind];
                label4.Text = selectedRows.Cells[0].Value.ToString();
                textBox1.Text = selectedRows.Cells[1].Value.ToString();
                textBox2.Text = selectedRows.Cells[2].Value.ToString();
                textBox3.Text = selectedRows.Cells[3].Value.ToString();
            }
            catch
            {

                ToList();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Güncelle();
        }
        private void SearchData()
        {
            if (comboBox1.SelectedItem == "SirketAD")
            {
                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * from Sirketler where SirketAD like '%" + textBox4.Text + "%'", conn);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                conn.Close();
            }
            if (comboBox1.SelectedItem == "SirketTEL")
            {
                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * from Sirketler where SirketTEL like '%" + textBox4.Text + "%'", conn);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                conn.Close();
            }
            if (comboBox1.SelectedItem == "SirketMail")
            {
                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * from Sirketler where SirketMail like '%" + textBox4.Text + "%'", conn);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                conn.Close();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void calisanlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calisanlar calisanlar = new Calisanlar();
            calisanlar.Show();
            this.Hide();
        }

        private void musterilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Musteriler musteriler = new Musteriler();
            musteriler.Show();
            this.Hide();
        }

        private void odalarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 odalar = new Form1();
            odalar.Show();
            this.Hide();
        }
    }
}
