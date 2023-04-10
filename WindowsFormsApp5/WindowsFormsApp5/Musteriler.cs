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

namespace WindowsFormsApp5
{
    public partial class Musteriler : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=USER25;Initial Catalog=otelkayit;Integrated Security=True");
        DataSet ds = new DataSet();
        public Musteriler()
        {
            InitializeComponent();
        }
        public void ToList()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Musteriler", conn);
            ds.Clear();
            adapter.Fill(ds, "Musteriler");
            dataGridView1.DataSource = ds.Tables["Musteriler"];

            conn.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
        }
        public void Güncelle()
        {
            if (label6.Text == "")
            {
                MessageBox.Show("Kayıt Seçiniz");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand komut = new SqlCommand("update Musteriler SET Musteriisim = @Musteriisim, MusteriSoyad = @MusteriSoyad,MusteriTelefon=@MusteriTelefon,MusteriMail=@MusteriMail,MusteriDT=@MusteriDT where MusteriID = @MusteriID");
                    komut.Parameters.AddWithValue("@Musteriisim", textBox1.Text);
                    komut.Parameters.AddWithValue("@MusteriSoyad", textBox2.Text);
                    komut.Parameters.AddWithValue("@MusteriTelefon", textBox3.Text);
                    komut.Parameters.AddWithValue("@MusteriMail", textBox5.Text);
                    komut.Parameters.AddWithValue("@MusteriDT", dateTimePicker1.Value);
                    komut.Connection = conn;
                    komut.Parameters.AddWithValue("@MusteriID", label6.Text);
                    komut.ExecuteNonQuery();
                    conn.Close();
                    ToList();
                }
                catch 
                {

                    MessageBox.Show("Doğru Veri giriniz!");
                }
                
            }
        }
        private void Musteriler_Load(object sender, EventArgs e)
        {
           
            this.musterilerTableAdapter.Fill(this.otelkayitDataSet.Musteriler);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO Musteriler(Musteriisim,MusteriSoyad,MusteriTelefon,MusteriMail,MusteriDT) Values(@Musteriisim,@MusteriSoyad,@MusteriTelefon,@MusteriMail,@MusteriDT)");
                komut.Parameters.AddWithValue("@Musteriisim", textBox1.Text);
                komut.Parameters.AddWithValue("@MusteriSoyad", textBox2.Text);
                komut.Parameters.AddWithValue("@MusteriTelefon", textBox3.Text);
                komut.Parameters.AddWithValue("@MusteriMail", textBox5.Text);
                komut.Parameters.AddWithValue("@MusteriDT", dateTimePicker1.Value);
                komut.Connection = conn;
                komut.ExecuteNonQuery();
                conn.Close();
                ToList();
            }
            catch
            {
                MessageBox.Show("Doğru Veri Giriniz!");
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
                SqlCommand komut = new SqlCommand("Delete From Musteriler where MusteriID=@MusteriID");
                komut.Connection = conn;
                komut.Parameters.AddWithValue("MusteriID", label6.Text);
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
                label6.Text = selectedRows.Cells[0].Value.ToString();
                textBox1.Text = selectedRows.Cells[1].Value.ToString();
                textBox2.Text = selectedRows.Cells[2].Value.ToString();
                textBox3.Text = selectedRows.Cells[3].Value.ToString();
                textBox5.Text = selectedRows.Cells[4].Value.ToString();
                dateTimePicker1.Text = selectedRows.Cells[5].Value.ToString();

            }
            catch
            {

                ToList();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void SearchData()
        {
            if (comboBox1.SelectedItem == "Musteriisim")
            {
                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * From Musteriler where Musteriisim like '%" + textBox4.Text + "%'", conn);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                conn.Close();
            }
            if (comboBox1.SelectedItem == "MusteriTelefon")
            {
                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * From Musteriler where MusteriTelefon like '%" + textBox4.Text + "%'", conn);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                conn.Close();
            }
            if (comboBox1.SelectedItem == "MusteriMail")
            {
                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * from Musteriler where MusteriMail like '%" + textBox4.Text + "%'", conn);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                conn.Close();
            }
            if (comboBox1.SelectedItem == "MusteriDT")
            {
                DateTime dateToday = dateTimePicker1.Value;
                string strData = dateToday.ToString("yyyy-MM-dd");
              
                    DataTable tablo = new DataTable();
                    conn.Open();
                    SqlDataAdapter adtr = new SqlDataAdapter("Select * from Musteriler where MusteriDT like '%" + strData + "%'", conn);
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

        private void odalarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 odalar = new Form1();
            odalar.Show();
            this.Hide();
        }

        private void sirketlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sirketler sirketler = new Sirketler();
            sirketler.Show();
            this.Hide();
        }
    }

}
