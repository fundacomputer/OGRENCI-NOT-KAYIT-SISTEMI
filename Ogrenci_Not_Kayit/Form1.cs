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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        private void BtnOgretmenGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from TblOgretmen  where NUMARA=@p1 and SIFRE=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskOgretmenNumara.Text);
            komut.Parameters.AddWithValue("@p2", TxtOgretmenSifre.Text);
            SqlDataReader da = komut.ExecuteReader();
            if (da.Read())
            {
                FrmOgretmen frmOgretmen = new FrmOgretmen();
                frmOgretmen.numara = MskOgretmenNumara.Text;
                frmOgretmen.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            bgl.baglanti().Close();
        }

        private void BtnOgrenciGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select*from TblOgrenci where NUMARA=@p1 and SIFRE=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskOgrenciNumara.Text);
            komut.Parameters.AddWithValue("@p2", TxtOgrenciSifre.Text);
            SqlDataReader da = komut.ExecuteReader();
            if (da.Read())
            {
                FrmOgrenci frmOgrenci = new FrmOgrenci();
                frmOgrenci.numara = MskOgrenciNumara.Text;
                frmOgrenci.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
