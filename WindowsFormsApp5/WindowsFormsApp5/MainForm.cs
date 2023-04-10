using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace WindowsFormsApp5
{
    public partial class MainForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=USER25;Initial Catalog=otelkayit;Integrated Security=True");
        DataSet ds = new DataSet();
        int second = 0;
        public MainForm()
        {
            InitializeComponent();
        }

   



        private void odalarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 odalar = new Form1();
            odalar.Show();
        }

        private void musterilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Musteriler musteriler = new Musteriler();
            musteriler.Show();
        }

        private void calisanlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calisanlar calisanlar = new Calisanlar();
            calisanlar.Show();
        }

        private void sirketlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sirketler sirketler = new Sirketler();
            sirketler.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
      
            string[] odalar = new string[] { "101", "102", "103", "201", "202", "203", "301", "302", "303", "401", "402", "403", "501", "502", "503", "601", "602", "603" };
            string[] butonlar = new string[] { "button16", "button17", "button18", "button13", "button14", "button15", "button10", "button11", "button12", "button7", "button8", "button9", "button4", "button5", "button6", "button1", "button2", "button3" };

            for (int i = 0; i < odalar.Length; i++)
            {
                renkDegistir(odalar[i], butonlar[i]);
            }
            this.odaislemTableAdapter.Fill(this.otelkayitDataSet.odaislem);

            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT Musteriisim FROM Musteriler";
            komut.Connection = conn;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            conn.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["Musteriisim"]);
            }
            conn.Close();


            komut.CommandText = "SELECT CalisanAD FROM Calisanlar";
            komut.Connection = conn;
            komut.CommandType = CommandType.Text;


            conn.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["CalisanAD"]);
            }
            conn.Close();

            komut.CommandText = "SELECT SirketAD FROM Sirketler";
            komut.Connection = conn;
            komut.CommandType = CommandType.Text;

            conn.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr["SirketAD"]);
            }
            conn.Close();
            second = 2;
            timer1.Start();
           

        }

        public void CikisKontrol()
        {
            int i = 0;
            string[] odadizi = new string[18];
            conn.Open();
            SqlCommand komut = new SqlCommand("Select OdaNO from odaislem where CikisTarih < @Bugun");
            komut.Parameters.AddWithValue("@Bugun", DateTime.Today);
            komut.Connection = conn;
            SqlDataReader dataReader = komut.ExecuteReader();
            while (dataReader.Read())
            {
                
                odadizi[i] = dataReader[0].ToString();
                i++;
            }
            foreach (string item in odadizi)
            {
                if (item==null)
                {
                    break;
                }
                else
                {
                    CikisSil(item);
                }
            }
            conn.Close();
            ToList();
        }

        public void CikisSil(string gecmisOda)
        {
            conn.Close();
            conn.Open();
            SqlCommand komut = new SqlCommand("Delete From odaislem where OdaNO=@OdaNO");
            komut.Connection = conn;
            komut.Parameters.AddWithValue("@OdaNO", gecmisOda);
            komut.ExecuteNonQuery();
            komut.Dispose();
            komut.CommandText = "Update Odalar set OdaDurumu = 'False' WHERE OdaNo = " + gecmisOda;
            komut.ExecuteNonQuery();

            conn.Close();
            string[] odalar = new string[] { "101", "102", "103", "201", "202", "203", "301", "302", "303", "401", "402", "403", "501", "502", "503", "601", "602", "603" };
            string[] butonlar = new string[] { "button16", "button17", "button18", "button13", "button14", "button15", "button10", "button11", "button12", "button7", "button8", "button9", "button4", "button5", "button6", "button1", "button2", "button3" };

            for (int i = 0; i < odalar.Length; i++)
            {
                renkDegistir(odalar[i], butonlar[i]);
            }
            ToList();
        }
        public void Güncelle()
        {
            if (label1.Text == "000")
            {
                MessageBox.Show("Kayıt Seçiniz");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand komut = new SqlCommand("update odaislem SET MusteriAdi = @MusteriAdi, MusteriTelefon = @MusteriTelefon,GirisTarih=@GirisTarih,CikisTarih=@CikisTarih,CalisanAdi=@CalisanAdi,Sirket=@Sirket where OdaislemID = @OdaislemID");
                    komut.Parameters.AddWithValue("@MusteriAdi", comboBox1.Text);
                    komut.Parameters.AddWithValue("@MusteriTelefon", textBox2.Text);
                    komut.Parameters.AddWithValue("@GirisTarih", dateTimePicker1.Value);
                    komut.Parameters.AddWithValue("@CikisTarih", dateTimePicker2.Value);
                    komut.Parameters.AddWithValue("@CalisanAdi", comboBox2.Text);
                    komut.Parameters.AddWithValue("@Sirket", comboBox3.Text);
                    komut.Connection = conn;

                    komut.Parameters.AddWithValue("@OdaislemID", label8.Text);
                    komut.ExecuteNonQuery();
                    conn.Close();
                    ToList();
                }
                catch 
                {
                    conn.Close();
                    MessageBox.Show("Kayıt Seçiniz!");
                }

                

            }
        }

        public void BtnToList()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from odaislem", conn);
            ds.Clear();
            adapter.Fill(ds, "odaislem");
            dataGridView1.DataSource = ds.Tables["odaislem"];

            conn.Close();
            string[] odalar = new string[] { "101", "102", "103", "201", "202", "203", "301", "302", "303", "401", "402", "403", "501", "502", "503", "601", "602", "603" };
            string[] butonlar = new string[] { "button16", "button17", "button18", "button13", "button14", "button15", "button10", "button11", "button12", "button7", "button8", "button9", "button4", "button5", "button6", "button1", "button2", "button3" };

            for (int i = 0; i < odalar.Length; i++)
            {
                renkDegistir(odalar[i], butonlar[i]);
            }
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox2.Text = "05** *** ****";
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
        }
   
        public void ToList()

        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from odaislem", conn);
            ds.Clear();
            adapter.Fill(ds, "odaislem");
            dataGridView1.DataSource = ds.Tables["odaislem"];

            conn.Close();
            string[] odalar = new string[] { "101", "102", "103", "201", "202", "203", "301", "302", "303", "401", "402", "403", "501", "502", "503", "601", "602", "603" };
            string[] butonlar = new string[] { "button16", "button17", "button18", "button13", "button14", "button15", "button10", "button11", "button12", "button7", "button8", "button9", "button4", "button5", "button6", "button1", "button2", "button3" };

            for (int i = 0; i < odalar.Length; i++)
            {
                renkDegistir(odalar[i], butonlar[i]);
            }
            label1.Text = "000";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox2.Text = "05** *** ****";
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
        }

        public void Uyari()
        {
            MessageBox.Show("Oda Dolu");
        }
        public bool odaKontrol(string OdaNO)
        {
           
                conn.Open();
                SqlCommand command = new SqlCommand("Select OdaDurumu from Odalar Where OdaNO = " + OdaNO, conn);
                bool isOda = (bool)command.ExecuteScalar();
                conn.Close();
                return isOda;
           
        

            
        }
        public void renkDegistir(string OdaNO, string odaAdı)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("Select OdaDurumu from Odalar Where OdaNO = " + OdaNO, conn);
            bool isOda = (bool)command.ExecuteScalar();
            conn.Close();
            if (odaAdı == "button16" && isOda)
            {
                button16.BackColor = Color.Red;
            }
            else if (odaAdı == "button16" && !isOda)
            {
                button16.BackColor = Color.White;

            }
            if (odaAdı == "button17" && isOda)
            {
                button17.BackColor = Color.Red;
            }
            else if (odaAdı == "button17" && !isOda)
            {
                button17.BackColor = Color.White;
            }
                if (odaAdı == "button18" && isOda)
                button18.BackColor = Color.Red;
            else if (odaAdı == "button18" && !isOda)
                button18.BackColor = Color.White;
            if (odaAdı == "button1" && isOda)
            {
                button1.BackColor = Color.Red;
            }
            else if (odaAdı == "button1" && !isOda)
            {
                button1.BackColor = Color.White;
            }
            if (odaAdı == "button2" && isOda)
            {
                button2.BackColor = Color.Red;
            }
            else if (odaAdı == "button2" && !isOda)
            {
                button2.BackColor = Color.White;
            }
            if (odaAdı == "button3" && isOda)
            {
                button3.BackColor = Color.Red;
            }
            else if (odaAdı == "button3" && !isOda)
            {
                button3.BackColor = Color.White;
            }
            if (odaAdı == "button4" && isOda)
            {
                button4.BackColor = Color.Red;
            }
            else if (odaAdı == "button4" && !isOda)
            {
                button4.BackColor = Color.White;
            }
            if (odaAdı == "button5" && isOda)
            {
                button5.BackColor = Color.Red;
            }
            else if (odaAdı == "button5" && !isOda)
            {
                button5.BackColor = Color.White;
            }
            if (odaAdı == "button6" && isOda)
            {
                button6.BackColor = Color.Red;
            }
            else if (odaAdı == "button6" && !isOda)
            {
                button6.BackColor = Color.White;
            }
            if (odaAdı == "button7" && isOda)
            {
                button7.BackColor = Color.Red;
            }
            else if (odaAdı == "button7" && !isOda)
            {
                button7.BackColor = Color.White;
            }
            if (odaAdı == "button8" && isOda)
            {
                button8.BackColor = Color.Red;
            }
            else if (odaAdı == "button8" && !isOda)
            {
                button8.BackColor = Color.White;
            }
            if (odaAdı == "button9" && isOda)
            {
                button9.BackColor = Color.Red;
            }
            else if (odaAdı == "button9" && !isOda)
            {
                button9.BackColor = Color.White;
            }
            if (odaAdı == "button10" && isOda)
            {
                button10.BackColor = Color.Red;
            }
            else if (odaAdı == "button10" && !isOda)
            {
                button10.BackColor = Color.White;
            }
            if (odaAdı == "button11" && isOda)
            {
                button11.BackColor = Color.Red;
            }
            else if (odaAdı == "button11" && !isOda)
            {
                button11.BackColor = Color.White;
            }
            if (odaAdı == "button12" && isOda)
            {
                button12.BackColor = Color.Red;
            }
            else if (odaAdı == "button12" && !isOda)
            {
                button12.BackColor = Color.White;
            }
            if (odaAdı == "button13" && isOda)
            {
                button13.BackColor = Color.Red;
            }
            else if (odaAdı == "button13" && !isOda)
            {
                button13.BackColor = Color.White;
            }
            if (odaAdı == "button14" && isOda)
            {
                button14.BackColor = Color.Red;
            }
            else if (odaAdı == "button14" && !isOda)
            {
                button14.BackColor = Color.White;
            }
            if (odaAdı == "button15" && isOda)
            {
                button15.BackColor = Color.Red;
            }
            else if (odaAdı == "button15" && !isOda)
            {
                button15.BackColor = Color.White;
            }
        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (label1.Text == "000")
            {
                MessageBox.Show("Oda Seçiniz");
            }
            else if (label1.Text != "000")
            {
                bool odadurumu = odaKontrol(label1.Text);

                if (odadurumu)
                {
                    ToList();
                    MessageBox.Show("Oda Dolu Ekleyemezsiniz");
                }

                else
                {
                    try
                    {
                        conn.Open();
                        SqlCommand komut = new SqlCommand("INSERT into odaislem(OdaNO,MusteriAdi,MusteriTelefon,GirisTarih,CikisTarih,CalisanAdi,Sirket) Values(@OdaNo,@MusteriAdi,@MusteriTelefon,@GirisTarih,@CikisTarih,@CalisanAdi,@Sirket)");
                        komut.Parameters.AddWithValue("@OdaNO", label1.Text);
                        komut.Parameters.AddWithValue("@MusteriAdi", comboBox1.Text);
                        komut.Parameters.AddWithValue("@MusteriTelefon", textBox2.Text);
                        komut.Parameters.AddWithValue("@GirisTarih", dateTimePicker1.Value);
                        komut.Parameters.AddWithValue("@CikisTarih", dateTimePicker2.Value);
                        komut.Parameters.AddWithValue("@CalisanAdi", comboBox2.Text);
                        komut.Parameters.AddWithValue("@Sirket", comboBox3.Text);
                        komut.Connection = conn;
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                        komut.CommandText = "Update Odalar set OdaDurumu = 'True' WHERE OdaNo = " + label1.Text;
                        komut.ExecuteNonQuery();
                        conn.Close();
                        ToList();
                    }
                    catch 
                    {
                        conn.Close();
                        MessageBox.Show("Gerekli Alanları Doldurun!");
                        ToList();
                    }
                   
                }
            }

           
           
            


        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut = new SqlCommand("Select MusteriTelefon From Musteriler Where Musteriisim = '"+ comboBox1.Text+"'",conn);
            string telefonNo = komut.ExecuteScalar().ToString();
            textBox2.Text = telefonNo;
            conn.Close();


        }

        private void button16_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button16.Text);
            if (odaDurum)
            {
                label1.Text = button16.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button16.Text;
                BtnToList();
            }



        }


        private void button17_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button17.Text);
            if (odaDurum)
            {
                label1.Text = button17.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button17.Text;
                BtnToList();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button18.Text);
            if (odaDurum)
            {
                label1.Text = button18.Text;
                Uyari();
                ToList();
            }

            else
            {
                label1.Text = button18.Text;
                BtnToList();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button13.Text);
            if (odaDurum)
            {
                label1.Text = button13.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button13.Text;
                BtnToList();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button14.Text);
            if (odaDurum)
            {
                label1.Text = button14.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button14.Text;
                BtnToList();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button15.Text);
            if (odaDurum)
            {
                label1.Text = button15.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button15.Text;
                BtnToList();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button10.Text);
            if (odaDurum)
            {
                label1.Text = button10.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button10.Text;
                BtnToList();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button11.Text);
            if (odaDurum)
            {
                label1.Text = button11.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button11.Text;
                BtnToList();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button12.Text);
            if (odaDurum)
            {
                label1.Text = button12.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button12.Text;
                BtnToList();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button7.Text);
            if (odaDurum)
            {
                label1.Text = button7.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button7.Text;
                BtnToList();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button18.Text);
            if (odaDurum)
            {
                label1.Text = button8.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button8.Text;
                BtnToList();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button9.Text);
            if (odaDurum)
            {
                label1.Text = button9.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button9.Text;
                BtnToList();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button4.Text);
            if (odaDurum)
            {
                label1.Text = button4.Text;
                Uyari();
                ToList();
            }

            else
            {
                label1.Text = button4.Text;
                BtnToList();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button5.Text);
            if (odaDurum)
            {
                label1.Text = button5.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button5.Text;
                BtnToList();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button6.Text);
            if (odaDurum)
            {
                label1.Text = button6.Text;
                Uyari();
                ToList();
            }

            else
            {
                label1.Text = button6.Text;
                BtnToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button1.Text);
            if (odaDurum)
            {
                label1.Text = button1.Text;

                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button1.Text;
                BtnToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button2.Text);
            if (odaDurum)
            {
                label1.Text = button2.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button2.Text;
                BtnToList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool odaDurum = odaKontrol(button3.Text);
            if (odaDurum)
            {
                label1.Text = button3.Text;
                Uyari();
                ToList();
            }
            else
            {
                label1.Text = button3.Text;
                BtnToList();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ind = e.RowIndex;
                DataGridViewRow selectedRows = dataGridView1.Rows[ind];
                label8.Text = selectedRows.Cells[0].Value.ToString();
                label1.Text = selectedRows.Cells[1].Value.ToString();
                comboBox1.Text = selectedRows.Cells[2].Value.ToString();
                textBox2.Text = selectedRows.Cells[3].Value.ToString();
                dateTimePicker1.Text = selectedRows.Cells[4].Value.ToString();
                dateTimePicker2.Text = selectedRows.Cells[5].Value.ToString();
                comboBox2.Text = selectedRows.Cells[6].Value.ToString();
                comboBox3.Text = selectedRows.Cells[7].Value.ToString();
            }
            catch
            {

                ToList();
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Güncelle();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("Delete From odaislem where OdaislemID=@OdaislemID");
                komut.Connection = conn;
                komut.Parameters.AddWithValue("@OdaislemID", label8.Text);
                komut.ExecuteNonQuery();
                komut.Dispose();
                komut.CommandText = "Update Odalar set OdaDurumu = 'False' WHERE OdaNo = " + label1.Text;
                komut.ExecuteNonQuery();

                conn.Close();
                string[] odalar = new string[] { "101", "102", "103", "201", "202", "203", "301", "302", "303", "401", "402", "403", "501", "502", "503", "601", "602", "603" };
                string[] butonlar = new string[] { "button16", "button17", "button18", "button13", "button14", "button15", "button10", "button11", "button12", "button7", "button8", "button9", "button4", "button5", "button6", "button1", "button2", "button3" };

                for (int i = 0; i < odalar.Length; i++)
                {
                    renkDegistir(odalar[i], butonlar[i]);
                }
                ToList();
            }
            catch 
            {
                conn.Close();
                MessageBox.Show("Seçiniz!");
            }
           
           
        }

        private void button22_Click(object sender, EventArgs e)
        {
            CikisKontrol();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblSecond.Text = second--.ToString();
            if (lblSecond.Text == "2")
            {
                DialogResult dialogResult = MessageBox.Show("Tarihi Geçmiş Kayıtları Silmek İster misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dialogResult == DialogResult.Yes)
                {
                    CikisKontrol();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //Bomboş DEVAAAAAAAAM
                }
            }
            timer1.Stop();
        }
    }
}
