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

namespace sinemaProje
{
    public partial class frmAnasayfa : Form
    {
        public frmAnasayfa()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.\\;Initial Catalog=Sinema_Bileti;Integrated Security=True");

        private void button6_Click(object sender, EventArgs e)
        {
            frmFilmEkle frm = new frmFilmEkle();
            frm.Show();
            
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            frmSalonEkle frm = new frmSalonEkle();
            frm.Show();
            this.Hide();
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            frmSeansEkle frm = new frmSeansEkle();
            frm.Show();
            
        }
        int sayac = 0;
        private void FilmveSalonGetir(ComboBox combo,string sql1,string sql2)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sql1,baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                combo.Items.Add(read[sql2].ToString());
            }
            baglanti.Close();
        }
        private void FilmAfisiGoster()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from film_bilgileri where filmadi='"+comboFilmAdi.SelectedItem+"'",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                pictureBox1.ImageLocation = read["resim"].ToString();
            }
            baglanti.Close();
        }
        private void Combo_Dolu_Koltuklar()
        {
            comboKoltukIptal.Items.Clear();
            comboKoltukIptal.Text = "";
            foreach (Control item in panelKoltuk.Controls)
            {
                if (item is Button)
                {
                    if (item.BackColor == Color.Red)
                    {
                        comboKoltukIptal.Items.Add(item.Text);
                    }
                }               
            }
        }
        private void YenidenRenklendir()
        {
            foreach (Control item in panelKoltuk.Controls)
            {
                if (item is Button)
                {
                    item.BackColor = Color.White;
                }
            }
        }       
        private void Veritabani_Dolu_Koltuklar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from satis_bilgileri where filmadi='"+comboFilmAdi.SelectedItem+"'and salonadi='"+comboSalonAdi.Text+"'and tarih='"+comboFilmTarihi.SelectedItem+"' and saat='"+comboFilmSeansi.SelectedItem+"'",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in panelKoltuk.Controls)
                {
                    if (item is Button)
                    {
                        if (read["koltukno"].ToString()==item.Text)
                        {
                            item.BackColor = Color.Red;
                        }
                            
                    }
                }
            }
            baglanti.Close();
        }

        private void frmAnasayfa_Load(object sender, EventArgs e)
        {
            comboUcret.Items.Add(15);
            comboUcret.Items.Add(20);
            Bos_Koltuklar();
            FilmveSalonGetir(comboFilmAdi,"select *from film_bilgileri","filmadi");
            FilmveSalonGetir(comboSalonAdi, "select *from salon_bilgileri","salonadi");
        }
        private void Bos_Koltuklar()
        {
            sayac = 1;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(30, 30);
                    btn.BackColor = Color.White;
                    btn.Location = new Point(j * 30 + 20, i * 30 + 30);
                    btn.Name = sayac.ToString();
                    btn.Text = sayac.ToString();
                    if (j == 4)
                    {
                        continue;
                    }
                    sayac++;
                    this.panelKoltuk.Controls.Add(btn);
                    btn.Click += Btn_Click;
                }
            }
        }
        private void Btn_Click(object sender, EventArgs e)
        {         
            Button b = (Button)sender;
            if (b.BackColor==Color.White)
            {
                txtKoltukNo.Text = b.Text;
            }
        }
        private void btnSatislar_Click(object sender, EventArgs e)
        {
            frmSatisListeleme satis = new frmSatisListeleme();
            satis.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmSeansListele frm = new frmSeansListele();
            frm.Show();           
        }
        private void comboFilmAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboFilmSeansi.Items.Clear();
            comboFilmTarihi.Items.Clear();
            comboFilmSeansi.Text = "";
            comboFilmTarihi.Text = "";
            comboSalonAdi.Text = "";
            foreach (Control item in groupBox1.Controls) if (item is TextBox) item.Text = "";           
            FilmAfisiGoster();
            YenidenRenklendir();
            Combo_Dolu_Koltuklar();
        }
        sinemaTableAdapters.Satis_BilgileriTableAdapter satis = new sinemaTableAdapters.Satis_BilgileriTableAdapter();
        private void btnBiletSat_Click(object sender, EventArgs e)
        {           
            if (txtKoltukNo.Text!="")
            {
                try
                {
                    
                    if (txtAd.Text!="" || txtSoyad.Text!="")
                    {
                        satis.Satis_Yap(txtKoltukNo.Text, comboSalonAdi.Text, comboFilmAdi.Text, comboFilmTarihi.Text, comboFilmSeansi.Text, txtAd.Text, txtSoyad.Text, comboUcret.Text, DateTime.Now.ToShortDateString());
                    }
                    else
                    {
                        MessageBox.Show("Ad,Soyad girin");
                    }
                    
                    foreach (Control item in groupBox1.Controls) if (item is TextBox) item.Text = "";
                    YenidenRenklendir();
                    Veritabani_Dolu_Koltuklar();
                    Combo_Dolu_Koltuklar();
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Hata oluştu!!!"+hata.Message, "Uyarı");
                } 
            }
            else
            {
                MessageBox.Show("Koltuk seçimi yapmadınız!!!","Uyarı");
            }
        }
        private void Film_Tarihi_Getir()
        {
            comboFilmTarihi.Text = "";
            comboFilmSeansi.Text = "";
            comboFilmTarihi.Items.Clear();
            comboFilmSeansi.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from seans_bilgileri where filmadi='"+comboFilmAdi.SelectedItem+"'and salonadi='"+comboSalonAdi.SelectedItem+"'",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (DateTime.Parse(read["tarih"].ToString())>=DateTime.Parse(DateTime.Now.ToShortDateString()))
                {
                    if (!comboFilmTarihi.Items.Contains(read["tarih"].ToString()))
                    {
                        comboFilmTarihi.Items.Add(read["tarih"].ToString());
                    }
                    
                }
                
            }
            baglanti.Close();
        }
        private void comboSalonAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Film_Tarihi_Getir();
        }
        private void Film_Seansi_Getir()
        {
            comboFilmSeansi.Text = "";
            comboFilmSeansi.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from seans_bilgileri where filmadi='" + comboFilmAdi.SelectedItem+ "' and salonadi='" + comboSalonAdi.SelectedItem + "' and tarih='"+comboFilmTarihi.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                
                if (DateTime.Parse(read["tarih"].ToString()) == DateTime.Parse(DateTime.Now.ToShortDateString()))
                {
                    
                    if (DateTime.Parse(read["seans"].ToString())> DateTime.Parse(DateTime.Now.ToShortTimeString()))
                    {
                        comboFilmSeansi.Items.Add(read["seans"].ToString());
                    }
                                            
                }
                else if (DateTime.Parse(read["tarih"].ToString()) > DateTime.Parse(DateTime.Now.ToShortDateString()))
                {                   
                        comboFilmSeansi.Items.Add(read["seans"].ToString());                  
                }
            }
            baglanti.Close();
        }
        private void comboFilmTarihi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Film_Seansi_Getir();
        }
        private void comboFilmSeansi_SelectedIndexChanged(object sender, EventArgs e)
        {
            YenidenRenklendir();
            Veritabani_Dolu_Koltuklar();
            Combo_Dolu_Koltuklar();

        }
        private void btnBiletIptal_Click(object sender, EventArgs e)
        {
            if (comboKoltukIptal.Text!="")
            {
                try
                {
                    satis.Satis_Iptal(comboFilmAdi.Text, comboSalonAdi.Text, comboFilmTarihi.Text, comboFilmSeansi.Text, comboKoltukIptal.Text);
                    YenidenRenklendir();
                    Veritabani_Dolu_Koltuklar();
                    Combo_Dolu_Koltuklar();
                }
                catch (Exception hata)
                {

                    MessageBox.Show("Hata oluştu!"+hata.Message, "Uyarı");
                }               
            }
            else
            {
                MessageBox.Show("Koltuk seçimi yapmadınız!", "Uyarı");
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }       
    }
}
