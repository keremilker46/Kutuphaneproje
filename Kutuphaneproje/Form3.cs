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

namespace Kutuphaneproje
{
    public partial class Form3: Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=tsomtal.com;Initial Catalog=kutuphane149;Integrated Security=True;");

        public void listele()
        {
            SqlCommand komut = new SqlCommand("select * from ogrenciler", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

        }
        private void Form3_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open) baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into ogrenciler(ogrenci_no,ad,soyad,sinif,cinsiyet,telefon)values(@no,@ad,@soyad,@sinif,@cinsiyet,@tel)", baglanti);
                komut.Parameters.AddWithValue("@no", textBox1.Text);
                komut.Parameters.AddWithValue("@ad", textBox2.Text);
                komut.Parameters.AddWithValue("@soyad", textBox3.Text);
                komut.Parameters.AddWithValue("@sinif", comboBox1.Text);
                komut.Parameters.AddWithValue("@cinsiyet", comboBox2.Text);
                komut.Parameters.AddWithValue("@tel", textBox4.Text);
                MessageBox.Show("bilgiler kaydedildi");

                //baglanti.Open();
                komut.ExecuteNonQuery();
                listele();
                baglanti.Close();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open) baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from ogrenciler where ogrenci_no=@ogr_no", baglanti);
                komut.Parameters.AddWithValue("@ogr_no", dataGridView1.CurrentRow.Cells["ogrenci_no"].Value.ToString());
               // baglanti.Open();
                komut.ExecuteNonQuery();
                listele();
                baglanti.Close();
            }
     
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open) baglanti.Open();
                SqlCommand komut = new SqlCommand("update ogrenciler set ad=@ad,soyad=@soyad,sinif=@sinif,cinsiyet=@cinsiyet,telefon=@tel where ogrenci_no=@no", baglanti);
                komut.Parameters.AddWithValue("@no", dataGridView1.CurrentRow.Cells["ogrenci_no"].Value.ToString());
                komut.Parameters.AddWithValue("@ad", textBox2.Text);
                komut.Parameters.AddWithValue("@soyad", textBox3.Text);
                komut.Parameters.AddWithValue("@sinif", comboBox1.Text);
                komut.Parameters.AddWithValue("@cinsiyet", comboBox2.Text);
                komut.Parameters.AddWithValue("@tel", textBox4.Text);
               // baglanti.Open();
                komut.ExecuteNonQuery();
                listele();
                baglanti.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            arama(textBox5.Text);
        }

        public void arama(string ara)
        {
            try
            {
                if(baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                SqlCommand Ara = new SqlCommand("select * from ogrenciler where ad LIKE '"+ara+"%'", baglanti);
                SqlDataAdapter adapter = new SqlDataAdapter(Ara);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;

            }
            catch(Exception ex)
            {
                MessageBox.Show("Hata : " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

    }
}
