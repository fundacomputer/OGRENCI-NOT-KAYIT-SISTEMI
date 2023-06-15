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
    public partial class FrmMesajlar : Form
    {
        public FrmMesajlar()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string numara;



        void GonderenMesajlar()
        {
            SqlCommand komut = new SqlCommand("select * from TblMesajlar where GONDEREN = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",numara);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            
            da.Fill(dt);
            dataGridView1.DataSource = dt;




        }

        void GelenMesajlar()
        {
            SqlCommand komut = new SqlCommand("select * from TblMesajlar where ALICI = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", numara);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);

            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        private void FrmMesajlar_Load(object sender, EventArgs e)
        {
            maskedTextBox1.Text = numara;
            GonderenMesajlar();
            GelenMesajlar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblMesajlar (GONDEREN,ALICI,BASLIK,ICERİK) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", maskedTextBox2.Text);
            komut.Parameters.AddWithValue("@p3", TxtKonu.Text);
            komut.Parameters.AddWithValue("@p4", richTextBox1.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Mesaj Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GonderenMesajlar();
            GelenMesajlar();
        }
    }
}
