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
using System.Data.SqlClient;

namespace OkulNotProje
{
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=RASIT\\SQLEXPRESS;Initial Catalog=Okul;Integrated Security=True;TrustServerCertificate=True");
        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from tbl_kulupler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            TxtOgrenciKulup.DisplayMember = "KULUPAD";
            TxtOgrenciKulup.ValueMember = "KULUPID";
            TxtOgrenciKulup.DataSource = dt;
            baglanti.Close();


        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            string c ="";
            if (radioButton1.Checked=true)
            {
               c = "Erkek";
            }
            if (radioButton2.Checked = true)
            {
                c = "Kadın";
            }
            ds.OgrenciEkle(TxtOgrenciAdi.Text, TxtOgrenciSoyadi.Text, byte.Parse(TxtOgrenciKulup.SelectedValue.ToString()), c);
            MessageBox.Show("Öğrenci Ekleme Yapıldı");
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
        }

        private void TxtOgrenciKulup_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtOgrenciID.Text = TxtOgrenciKulup.SelectedValue.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSil(int.Parse(TxtOgrenciID.Text));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtOgrenciAdi.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtOgrenciSoyadi.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
           dataGridView1.DataSource = ds.OgrenciGetir(TxtAra.Text);

        }
    }
}
