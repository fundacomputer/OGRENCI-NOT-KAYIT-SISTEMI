﻿using System;
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
    public partial class FrmDuyuruListesi : Form
    {
        public FrmDuyuruListesi()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void FrmDuyuruListesi_Load(object sender, EventArgs e)
        {
            ListBox lst = new ListBox();
            Point lstkonum = new Point(10, 10);
            lst.Name = "Listbox1";
            lst.Height = 250;
            lst.Width = 450;
            lst.Font = new Font("Georgia", 14, FontStyle.Regular);
            lst.Location = lstkonum;
            this.Controls.Add(lst);
            //duyurular listesi
            SqlCommand komut = new SqlCommand("select *from TblDuyurular", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.baglanti().Close();
        }
    }
}
