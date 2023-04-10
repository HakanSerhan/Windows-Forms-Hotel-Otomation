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
    public partial class Calisanlar : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=USER25;Initial Catalog=otelkayit;Integrated Security=True");
        DataSet ds = new DataSet();
        public Calisanlar()
        {
            InitializeComponent();
        }
        public void ToList()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Calisanlar", conn);
            ds.Clear();
            adapter.Fill(ds, "Calisanlar");
            dataGridView1.DataSource = ds.Tables["Calisanlar"];

            conn.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }
        public void Güncelle()
        {
            if (label7.Text == "")
            {
                MessageBox.Show("Kayıt Seçiniz");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand komut = new SqlCommand("update Calisanlar SET CalisanAD = @CalisanAD, CalisanSoyad = @CalisanSoyad,CalisanTEL=@CalisanTEL,CalisanMail=@CalisanMail,CalisanUnvan=@CalisanUnvan,CalisanDT=@CalisanDT where CalisanID = @CalisanID");
                    komut.Parameters.AddWithValue("@CalisanAD", textBox1.Text);
                    komut.Parameters.AddWithValue("@CalisanSoyad", textBox2.Text);
                    komut.Parameters.AddWithValue("@CalisanTEL", textBox3.Text);
                    komut.Parameters.AddWithValue("@CalisanMail", textBox4.Text);
                    komut.Parameters.AddWithValue("@CalisanUnvan", textBox5.Text);
                    komut.Parameters.AddWithValue("@CalisanDT", dateTimePicker1.Value);
                    komut.Connection = conn;
                    komut.Parameters.AddWithValue("@CalisanID", label7.Text);
                    komut.ExecuteNonQuery();
                    conn.Close();
                    ToList();
                }
                catch 
                {
                    MessageBox.Show("Doğru veri Giriniz!");
                }
              
            }
        }
        private void Calisanlar_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'otelkayitDataSet.Calisanlar' table. You can move, or remove it, as needed.
            this.calisanlarTableAdapter.Fill(this.otelkayitDataSet.Calisanlar);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO Calisanlar(CalisanAD,CalisanSoyad,CalisanTEL,CalisanMail,CalisanUnvan,CalisanDT) Values(@CalisanAD,@CalisanSoyad,@CalisanTEL,@CalisanMail,@CalisanUnvan,@CalisanDT)");
                komut.Parameters.AddWithValue("@CalisanAD", textBox1.Text);
                komut.Parameters.AddWithValue("@CalisanSoyad", textBox2.Text);
                komut.Parameters.AddWithValue("@CalisanTEL", textBox3.Text);
                komut.Parameters.AddWithValue("@CalisanMail", textBox4.Text);
                komut.Parameters.AddWithValue("@CalisanUnvan", textBox5.Text);
                komut.Parameters.AddWithValue("@CalisanDT", dateTimePicker1.Value);
                komut.Connection = conn;
                komut.ExecuteNonQuery();
                conn.Close();
                ToList();
            }
            catch
            {
                MessageBox.Show("Doğru Veri Giriniz");
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
                SqlCommand komut = new SqlCommand("Delete From Calisanlar where CalisanID=@CalisanID");
                komut.Connection = conn;
                komut.Parameters.AddWithValue("@CalisanID", label7.Text);
                komut.ExecuteNonQuery();
                conn.Close();
                ToList();
            }
            catch
            {

                MessageBox.Show("Doğru Veri Giriniz!");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Güncelle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ind = e.RowIndex;
                DataGridViewRow selectedRows = dataGridView1.Rows[ind];
                label7.Text = selectedRows.Cells[0].Value.ToString();
                textBox1.Text = selectedRows.Cells[1].Value.ToString();
                textBox2.Text = selectedRows.Cells[2].Value.ToString();
                textBox3.Text = selectedRows.Cells[3].Value.ToString();
                textBox4.Text = selectedRows.Cells[4].Value.ToString();
                textBox5.Text = selectedRows.Cells[5].Value.ToString();
            }
            catch
            {

                ToList();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void SearchData()
        {
            if (comboBox1.SelectedItem == "CalisanAD")
            {
                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * from Calisanlar where CalisanAD like '%" + textBox6.Text + "%'", conn);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                conn.Close();
            }
            if (comboBox1.SelectedItem == "CalisanTEL")
            {
                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * from Calisanlar where CalisanTEL like '%" + textBox6.Text + "%'", conn);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                conn.Close();
            }
            if (comboBox1.SelectedItem == "CalisanMail")
            {
                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * from Calisanlar where CalisanMail like '%" + textBox6.Text + "%'", conn);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                conn.Close();
            }
            if (comboBox1.SelectedItem == "CalisanUnvan")
            {
                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * from Calisanlar where CalisanUnvan like '%" + textBox6.Text + "%'", conn);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                conn.Close();
            }
            if (comboBox1.SelectedItem == "CalisanDT")
            {
                DateTime dateToday = dateTimePicker1.Value;
                string strData = dateToday.ToString("yyyy-MM-dd");

                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * from Calisanlar where CalisanDT like '%" + strData + "%'", conn);
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
            Sirketler sirketler = new Sirketler();
            sirketler.Show();
            this.Hide();
        }

        private void odalarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 odalar = new Form1();
            odalar.Show();
            this.Hide();
        }

        private void musterilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Musteriler musteriler = new Musteriler();
            musteriler.Show();
            this.Hide();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
