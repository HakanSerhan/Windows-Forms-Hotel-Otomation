using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//Data Source=USER25;Initial Catalog=otelkayit;Integrated Security=True
namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=USER25;Initial Catalog=otelkayit;Integrated Security=True");
        DataSet ds = new DataSet();
        public Form1()
        {

            InitializeComponent();
        }
        public void ToList()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Odalar", conn);
            ds.Clear();
            adapter.Fill(ds, "Odalar");
            dataGridView1.DataSource = ds.Tables["Odalar"];
           
            conn.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox2.SelectedItem = "Boş";
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
                    SqlCommand komut = new SqlCommand("update Odalar SET OdaNo=@OdaNO,OdaKat = @OdaKat, OdaKisiSayisi = @OdaKisiSayisi,OdaDurumu=@OdaDurumu where OdaID = @OdaID");
                    komut.Parameters.AddWithValue("@OdaKat", textBox1.Text);
                    komut.Parameters.AddWithValue("@OdaKisiSayisi", textBox2.Text);
                    komut.Parameters.AddWithValue("@OdaNO", textBox3.Text);
                    if (comboBox2.SelectedItem == "Boş")
                    {
                        komut.Parameters.AddWithValue("@OdaDurumu", 0);
                    }
                    else if (comboBox2.SelectedItem == "Dolu")
                    {
                        komut.Parameters.AddWithValue("@OdaDurumu", 1);

                    }
                    komut.Connection = conn;
                    komut.Parameters.AddWithValue("OdaID", label3.Text);
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
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedItem = "Boş";
            comboBox1.SelectedItem = "OdaNO";

            // TODO: This line of code loads data into the 'otelkayitDataSet.Odalar' table. You can move, or remove it, as needed.
            this.odalarTableAdapter.Fill(this.otelkayitDataSet.Odalar);

            this.odalarTableAdapter.Fill(this.otelkayitDataSet.Odalar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO Odalar(OdaNo,OdaKat,OdaKisiSayisi,OdaDurumu) Values(@OdaNo,@OdaKat,@OdaKisiSayisi,@OdaDurumu)");
                komut.Parameters.AddWithValue("@ODAKat", textBox1.Text);
                komut.Parameters.AddWithValue("@OdaKisiSayisi", textBox2.Text);
                komut.Parameters.AddWithValue("@OdaNO", textBox3.Text);
                if (comboBox2.SelectedItem == "Boş")
                {
                    komut.Parameters.AddWithValue("@OdaDurumu", 0);

                }
                else if (comboBox2.SelectedItem == "Dolu")
                {
                    komut.Parameters.AddWithValue("@OdaDurumu", 1);

                }
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
                SqlCommand komut = new SqlCommand("Delete From Odalar where OdaID=@OdaID");
                komut.Connection = conn;
                komut.Parameters.AddWithValue("OdaID", label3.Text);
                komut.ExecuteNonQuery();
                conn.Close();
                ToList();
            }
            catch 
            {

                MessageBox.Show("Dorğu Veri Giriniz!");
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ind = e.RowIndex;
                DataGridViewRow selectedRows = dataGridView1.Rows[ind];
                label3.Text = selectedRows.Cells[0].Value.ToString();
                textBox3.Text = selectedRows.Cells[1].Value.ToString();
                textBox1.Text = selectedRows.Cells[2].Value.ToString();
                textBox2.Text = selectedRows.Cells[3].Value.ToString();

                if (selectedRows.Cells[4].Value.ToString()=="False")
                {
                    comboBox2.SelectedItem = "Boş";
                }
                
                else if (selectedRows.Cells[4].Value.ToString()=="True")
                {
                    comboBox2.SelectedItem = "Dolu";
                }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void SearchData()
        {
            if (comboBox1.SelectedItem=="OdaNO")
            {
                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * from Odalar where OdaNO like '%" + textBox4.Text + "%'", conn);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                conn.Close();
            }
            if (comboBox1.SelectedItem == "OdaKat")
            {
                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * from Odalar where OdaKat like '%" + textBox4.Text + "%'", conn);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                conn.Close();
            }
            if (comboBox1.SelectedItem == "KişiSayısı")
            {
                DataTable tablo = new DataTable();
                conn.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("Select * from Odalar where OdaKisiSayisi like '%" + textBox4.Text + "%'", conn);
                adtr.Fill(tablo);
                dataGridView1.DataSource = tablo;
                conn.Close();
            }
            if (comboBox1.SelectedItem == "OdaDurumu")
            {
                if (textBox4.Text=="Dolu")
                {

                    DataTable tablo = new DataTable();
                    conn.Open();
                    SqlDataAdapter adtr = new SqlDataAdapter("Select * from Odalar where OdaDurumu like '%" + 1 + "%'", conn);
                    adtr.Fill(tablo);
                    dataGridView1.DataSource = tablo;
                    conn.Close();

                }
                else if (textBox4.Text=="Boş")
                {

                    DataTable tablo = new DataTable();
                    conn.Open();
                    SqlDataAdapter adtr = new SqlDataAdapter("Select * from Odalar where OdaDurumu like '%" + 0 + "%'", conn);
                    adtr.Fill(tablo);
                    dataGridView1.DataSource = tablo;
                    conn.Close();
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void musterilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Musteriler musteriler = new Musteriler();
            musteriler.Show();
            this.Hide();
        }

        private void calisanlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calisanlar calisanlar = new Calisanlar();
            calisanlar.Show();
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
