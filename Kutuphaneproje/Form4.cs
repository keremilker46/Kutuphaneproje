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

namespace Kutuphaneproje
{
    public partial class Form4: Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=tsomtal.com;Initial Catalog=kutuphane149;Integrated Security=True;");

        public void listele()
        {
            SqlCommand komut = new SqlCommand("select * from kitaplar", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open) baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into kitaplar(tur_id,kitap_adi,yazar,yayinevi,sayfa_sayisi)values(@tur,@ad,@yazar,@yayinevi,@sayfa)", baglanti);
                komut.Parameters.AddWithValue("@tur", label7.Text);
                komut.Parameters.AddWithValue("@ad", textBox2.Text);
                komut.Parameters.AddWithValue("@yazar", textBox3.Text);
                komut.Parameters.AddWithValue("@yayinevi", textBox4.Text);
                komut.Parameters.AddWithValue("@sayfa", textBox5.Text);
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

        private void Form4_Load(object sender, EventArgs e)
        {
            listele();
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            SqlCommand komut = new SqlCommand("select tur_adi from kitap_turleri", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["tur_adi"].ToString());
            }
            reader.Close();
            baglanti.Close();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listele();
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            SqlCommand komut = new SqlCommand("select tur_id from kitap_turleri where tur_adi=@tur_adi", baglanti);
            komut.Parameters.AddWithValue("@tur_adi", comboBox1.Text);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                label7.Text=(reader["tur_id"].ToString());
            }
            reader.Close();
            baglanti.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open) baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from kitaplar where kitap_id=@kitap_id", baglanti);
                komut.Parameters.AddWithValue("@kitap_id", dataGridView1.CurrentRow.Cells["kitap_id"].Value.ToString());
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open) baglanti.Open();
                SqlCommand komut = new SqlCommand("update kitaplar set tur_id=@tur,kitap_adi=@kitap_adi,yazar=@yazar,yayinevi=@yayinevi,sayfa_sayisi=@sayfa where kitap_id=@id", baglanti);
                komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells["kitap_id"].Value.ToString());
                komut.Parameters.AddWithValue("@tur", label7.Text);
                komut.Parameters.AddWithValue("@kitap_adi", textBox2.Text);
                komut.Parameters.AddWithValue("@yazar", textBox3.Text);
                komut.Parameters.AddWithValue("@yayinevi", textBox4.Text);
                komut.Parameters.AddWithValue("@sayfa", textBox5.Text);
                // baglanti.Open();
                komut.ExecuteNonQuery();
                listele();
                baglanti.Close();
            }
            catch (Exception ex)
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            arama(textBox1.Text);
        }
        public void arama(string ara)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                SqlCommand Ara = new SqlCommand("select kitap_id,tur_adi,kitap_adi,yazar,yayinevi,sayfa_sayisi from kitaplar,kitap_turleri where kitap_turleri.tur_id = kitaplar.tur_id and kitap_adi LIKE '"+ara+"%'", baglanti);
                SqlDataAdapter adapter = new SqlDataAdapter(Ara);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;

                if(baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show("hata olustu" + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
    

        }

    }
}
