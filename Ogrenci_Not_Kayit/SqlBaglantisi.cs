using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Ogrenci_Not_Kayit
{
   public class SqlBaglantisi
    {

        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=LENOVO\SQLEXPRESS;Initial Catalog=OgrenciNotKayıtDB;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
