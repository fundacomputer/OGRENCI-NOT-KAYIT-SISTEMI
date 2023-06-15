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

namespace Ogrenci_Not_Kayit
{
    public partial class FrmOgretmen : Form
    {
        public FrmOgretmen()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        public string numara;
        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void FrmOgretmen_Load(object sender, EventArgs e)
        {
            TxtNumara.Text = numara;
            SqlCommand komut = new SqlCommand("Select* from  TblOgretmen where NUMARA=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",numara);
            SqlDataReader da = komut.ExecuteReader();
            while (da.Read())
            {
                TxtAd.Text = da[1] + " " + da[2];

            }
            bgl.baglanti().Close();

           

            OgrenciListesi();
            NotListesi();
        }
        void OgrenciListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select *from TblOgrenci", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        void NotListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute Ogrenciler", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        public string Fotograf;
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Fotograf = openFileDialog1.FileName;
            pictureBox1.ImageLocation = Fotograf;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblOgrenci (AD,SOYAD,NUMARA,SIFRE,FOTOGRAF) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtName.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", TxtSifre.Text);
            komut.Parameters.AddWithValue("@p4", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p5", Fotograf);
            komut.ExecuteNonQuery();
           
            MessageBox.Show("Öğrenci Bilgileri Kayıt Edildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

            bgl.baglanti().Close();
            OgrenciListesi();
            NotListesi();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtName.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

            SqlCommand komut = new SqlCommand("select * from TblNotlar where OGRID=(Select ID  from TblOgrenci where NUMARA=@p1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtSınav1.Text = dr[1].ToString();
                TxtSınav2.Text = dr[2].ToString();
                TxtSınav3.Text = dr[3].ToString();
                TxtProje.Text = dr[4].ToString();
                TxtOrtalama.Text = dr[5].ToString();
                TxtDurum.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //öğrenci bilgiler güncelleme
     SqlCommand komut1 = new  SqlCommand("Update TblOgrenci set AD=@p1,SOYAD=@p2,SIFRE=@p3,FOTOGRAF=@p4 where NUMARA=@p5", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", TxtName.Text);
            komut1.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut1.Parameters.AddWithValue("@p3", TxtSifre.Text);
            komut1.Parameters.AddWithValue("@p4", Fotograf);
            komut1.Parameters.AddWithValue("@p5", maskedTextBox1.Text);
            
           



            //notlar tablosunun güncellenmesi
            SqlCommand komut2 = new SqlCommand("update TblNotlar set SINAV1=@p6,SINAV2=@p7,SINAV3=@p8,PROJE=@p9,ORTALAMA=@p10,DURUM=@p11 where OGRID=(Select ID  from TblOgrenci where NUMARA=@p12)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p6", TxtSınav1.Text);
            komut2.Parameters.AddWithValue("@p7", TxtSınav2.Text);
            komut2.Parameters.AddWithValue("@p8", TxtSınav3.Text);
            komut2.Parameters.AddWithValue("@p9", TxtProje.Text);
            komut2.Parameters.AddWithValue("@p10",Convert.ToDecimal(TxtOrtalama.Text));
            komut2.Parameters.AddWithValue("@p11", TxtDurum.Text);
            komut2.Parameters.AddWithValue("@p12", maskedTextBox1.Text);

            komut1.ExecuteNonQuery();
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Öğrenci Bilgileri Güncellendi");








            OgrenciListesi();
            NotListesi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double sinav1, sinav2, sinav3, proje, ortalama;
            sinav1 =Convert.ToDouble(TxtSınav1.Text);
            sinav2 = Convert.ToDouble(TxtSınav2.Text);
            sinav3 = Convert.ToDouble(TxtSınav3.Text);
            proje = Convert.ToDouble(TxtProje.Text);
            ortalama = (sinav1 + sinav2 + sinav3 + proje) / 4;
            TxtOrtalama.Text = ortalama.ToString();
            if (ortalama >= 50)
            {
                TxtDurum.Text = "True";
            }
            else
            {
                TxtDurum.Text = "False";
            }

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            FrmDuyuruOlustur frmDuyuruOlustur = new FrmDuyuruOlustur();
            frmDuyuruOlustur.Show();

        }

        private void BtnDuyuruList_Click(object sender, EventArgs e)
        {
            FrmDuyuruListesi frmDuyuruListesi = new FrmDuyuruListesi();
            frmDuyuruListesi.Show();
        }

        private void BtnMesaj_Click(object sender, EventArgs e)
        {
            FrmMesajlar frmMesajlar = new FrmMesajlar();
            frmMesajlar.numara = numara;
            frmMesajlar.Show();
        }

        private void BtnYardım_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
