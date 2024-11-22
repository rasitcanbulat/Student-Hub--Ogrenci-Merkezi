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

namespace OkulNotProje
{
    public partial class FrmOgrenciNotlar : Form
    {
        public FrmOgrenciNotlar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=RASIT\\SQLEXPRESS;Initial Catalog=Okul;Integrated Security=True;TrustServerCertificate=True");
        public string numara;
        private void FrmOgrenciNotlar_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select dersad as 'Ders', sinav1 as '1.Sınav', sinav2 as '2.Sınav', sinav3 as '3.Sınav', proje as 'Proje', ortalama as 'Ortalama', durum as 'Geçti/Kaldı' from tbl_notlar inner join tbl_dersler on tbl_notlar.dersID = tbl_dersler.dersID where ogrID=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1",numara);
            //this.Text = numara.ToString();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //ad soyad forum ismi yapmak
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select ogrAd + '' + ogrSoyad from tbl_ogrenciler where ogrID =@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                this.Text = dr2[0].ToString();
            }
            baglanti.Close();
        }
    }
}
