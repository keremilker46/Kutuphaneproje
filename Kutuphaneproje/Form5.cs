using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphaneproje
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=tsomtal.com;Initial Catalog=kutuphane149;Integrated Security=True;");

        public void listele()
        {
            SqlCommand komut = new SqlCommand("select * from odunc_kitaplar", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

        }


        private void Form5_Load(object sender, EventArgs e)
        {
            listele();

            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }


            SqlCommand komut = new SqlCommand("select kitap_adi from kitaplar", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["kitap_adi"].ToString());
            }
            reader.Close();

            listele();




            SqlCommand komut1 = new SqlCommand("select ogrenci_no,ad from ogrenciler", baglanti);
            SqlDataReader reader1 = komut1.ExecuteReader();
            while (reader1.Read())
            {
                comboBox2.Items.Add(reader1["ad"].ToString());
                //comboBox2.Items.Add(reader1["ogrenci_no"].ToString());
            }
            reader1.Close();


       




            baglanti.Close();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open) baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from odunc_kitaplar where id=@id", baglanti);
                komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                // baglanti.Open();
                komut.ExecuteNonQuery();
                listele();
                baglanti.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open) baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into odunc_kitaplar(ogr_no,kitap_id,verilis_tarihi)values(@ogr_no,@id,@verilis)", baglanti);
                komut.Parameters.AddWithValue("@ogr_no", label8.Text);
                komut.Parameters.AddWithValue("@id", label4.Text);
                komut.Parameters.AddWithValue("@verilis", DateTime.Now);
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
            finally
            {
                baglanti.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells["teslim_tarihi"].Value.ToString() == string.Empty)
                {
                    if (baglanti.State != ConnectionState.Open) baglanti.Open();
                    SqlCommand komut = new SqlCommand("update odunc_kitaplar set teslim_tarihi=@teslim,aciklama=@aciklama where id=@id", baglanti);

                    komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                    komut.Parameters.AddWithValue("@teslim", DateTime.Now);
                    komut.Parameters.AddWithValue("@aciklama", richTextBox1.Text);

                    // baglanti.Open();
                    komut.ExecuteNonQuery();
                    listele();
                    baglanti.Close();
                }
                else
                {
                    MessageBox.Show("Bu kitap teslim alınmış", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listele();

            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            SqlCommand komut = new SqlCommand("select kitap_id from kitaplar where kitap_adi=@kitap_adi", baglanti);
            komut.Parameters.AddWithValue("@kitap_adi", comboBox1.Text);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                label4.Text = (reader["kitap_id"].ToString());
            }
            reader.Close();




            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listele();




            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            SqlCommand komut = new SqlCommand("select * from odunc_kitaplar where like ogr_no", baglanti);
            //komut.Parameters.AddWithValue("@deger", textBox2.Text);
            listele();
            baglanti.Close();

        }






        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listele();

            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            SqlCommand komut = new SqlCommand("select ogrenci_no from ogrenciler where ad=@ad", baglanti);
            komut.Parameters.AddWithValue("@ad", comboBox2.Text);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                label8.Text = (reader["ogrenci_no"].ToString());
            }
            reader.Close();




            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            arama(textBox1.Text);
        }


        public void arama(string ara)
        {
            if (baglanti.State != ConnectionState.Open) 
            {
                baglanti.Open();
            }
            SqlCommand Ara = new SqlCommand("select id,ogrenci_no,ad,soyad,kitap_adi,verilis_tarihi,teslim_tarihi,aciklama From kitaplar, ogrenciler, odunc_kitaplar where ogr_no=ogrenci_no and kitaplar.kitap_id=odunc_kitaplar.kitap_id and ad LIKE '"+ara+"%'",baglanti);
            Ara.Parameters.AddWithValue("@ara", ara);
            Ara.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(Ara);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
