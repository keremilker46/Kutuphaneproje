using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Kutuphaneproje
{
    public partial class Form2: Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        SqlConnection baglanti = new SqlConnection("Data Source=tsomtal.com;Initial Catalog=kutuphane149;Integrated Security=True;");
        public void listele()
        {
            SqlCommand komut = new SqlCommand("select * from kitap_turleri", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into kitap_turleri(tur_adi)values(@tur_adi)",baglanti);
            komut.Parameters.AddWithValue("@tur_adi", textBox1.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            listele();
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if(baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                SqlCommand komut = new SqlCommand("delete from kitap_turleri where tur_id=@tur_id", baglanti);
                komut.Parameters.AddWithValue("@tur_id", dataGridView1.CurrentRow.Cells["tur_id"].Value.ToString());
                komut.ExecuteNonQuery();
                listele();
                baglanti.Close();
            
            }
            catch(Exception ex)
            {
                MessageBox.Show("Hata!" + ex.Message);
                
            }
            finally
            {
                if (baglanti.State != ConnectionState.Closed)
                {
                    baglanti.Close();
                }
            }
           

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update kitap_turleri set tur_adi=@adi where tur_id=@tur_id", baglanti);
            komut.Parameters.AddWithValue("@tur_id", dataGridView1.CurrentRow.Cells["tur_id"].Value.ToString());
            komut.Parameters.AddWithValue("@adi",textBox1.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            listele();
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            arama(textBox2.Text);
        }
        public void arama(string ara)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                }
                SqlCommand Ara = new SqlCommand("select * from kitap_turleri where tur_adi LIKE '" + ara + "%'",baglanti);
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
