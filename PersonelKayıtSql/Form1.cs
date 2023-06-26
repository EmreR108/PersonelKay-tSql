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

namespace PersonelKayıtSql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-M0QQMPM\\SQLEXPRESS;Initial Catalog=PersonelKayit;Integrated Security=True");
        void temizle()
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtId.Text = "";
            txtMeslek.Text = "";
            cmbSehir.Text = "";
            mskMaas.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtAd.Focus();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tbl_PersonellTableAdapter.Fill(this.personelKayitDataSet2.Tbl_Personell);
            label8.Visible= false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tbl_PersonellTableAdapter.Fill(this.personelKayitDataSet2.Tbl_Personell);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Personell (PerAd,PerSoyad,PerSehir,PerMaas,PerMeslek)values (@p1,@p2,@p3,@p4,@p5)",baglanti);
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbSehir.Text);
            komut.Parameters.AddWithValue("@p4", mskMaas.Text);
            komut.Parameters.AddWithValue("@p5", txtMeslek.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Eklendi.");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            cmbSehir.Text = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            mskMaas.Text = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            label8.Text = dataGridView1.Rows[secim].Cells[5].Value.ToString();
            txtMeslek.Text = dataGridView1.Rows[secim].Cells[6].Value.ToString();


        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if (label8.Text == "True")
            {
                radioButton1.Checked = true;
            }
            if (label8.Text == "False")
            {
                radioButton2.Checked = true;
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label8.Text = "True";
            }
                
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label8.Text = "False";
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sil=new SqlCommand("Delete From Tbl_Personell Where PersonelId=@p1",baglanti);
            sil.Parameters.AddWithValue("@p1",txtId.Text);
            sil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Silindi");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("Update Tbl_Personell Set PerAd=@p1,PerSoyad=@p2,PerSehir=@p3,PerMaas=@p4,PerDurum=@p5,PerMeslek=@p6 where personelid=@p7", baglanti);
            guncelle.Parameters.AddWithValue("p1", txtAd.Text);
            guncelle.Parameters.AddWithValue("p2", txtSoyad.Text);
            guncelle.Parameters.AddWithValue("p3", cmbSehir.Text);
            guncelle.Parameters.AddWithValue("p4", mskMaas.Text);
            guncelle.Parameters.AddWithValue("p5", label8.Text);
            guncelle.Parameters.AddWithValue("p6", txtMeslek.Text);
            guncelle.Parameters.AddWithValue("p7", txtId.Text);
            guncelle.ExecuteNonQuery();
            baglanti.Close() ;
            MessageBox.Show("Personel Güncellemesi Tamamlandı");
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
