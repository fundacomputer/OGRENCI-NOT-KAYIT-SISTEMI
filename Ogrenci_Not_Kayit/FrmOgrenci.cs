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
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }
        public string numara;
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            label4numara.Text = numara;
            //kişi ad ve soyad çekme
            SqlCommand komut = new SqlCommand("select*from TblOgrenci where NUMARA=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", label4numara.Text);
            SqlDataReader da = komut.ExecuteReader();
            while (da.Read())
            {
                label3Ad.Text = da[1] + " " + da[2];
                pictureBox1.ImageLocation = da[5].ToString();
            }
            bgl.baglanti().Close();



            //not listesini çekme
            SqlCommand komut2 = new SqlCommand("select * from TblNotlar where OGRID=(Select ID from TblOgrenci where NUMARA=@p2)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p2", label4numara.Text);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                label6.Text = dr2[1].ToString();
                label7.Text = dr2[2].ToString();
                label9.Text = dr2[3].ToString();
                label11.Text = dr2[4].ToString();
                label13.Text = dr2[5].ToString();

            }
            bgl.baglanti().Close();
            if (Convert.ToDouble(label13.Text )>= 50)
            {
                label15.Text = "Geçti";
            }


            else
            {
                label15.Text = "Kaldı";
            }
        }

        private void BtnMesaj_Click(object sender, EventArgs e)
        {
            FrmMesajlar frm = new FrmMesajlar();
            frm.numara = label4numara.Text;
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmDuyuruListesi frm = new FrmDuyuruListesi();
            frm.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc.Exe");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Kapatmak İstiyor Musunuz", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
           
        }
    }
}
