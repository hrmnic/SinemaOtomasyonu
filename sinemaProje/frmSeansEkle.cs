using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sinemaProje
{
    public partial class frmSeansEkle : Form
    {
        public frmSeansEkle()
        {
            InitializeComponent();
        }
        sinemaTableAdapters.Seans_BilgileriTableAdapter filmseansi = new sinemaTableAdapters.Seans_BilgileriTableAdapter();
        SqlConnection baglanti = new SqlConnection("Data Source=.\\;Initial Catalog=Sinema_Bileti;Integrated Security=True");
     
        private void frmSeansEkle_Load_1(object sender, EventArgs e)
        {

            FilmveSalonGoster(comboFilm, "select *from film_bilgileri", "filmadi");
            FilmveSalonGoster(comboSalon, "select *from salon_bilgileri", "salonadi");
        }
        
        private void FilmveSalonGoster(ComboBox combo, string sql, string sql2)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sql, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read()==true)
            {
                //veritabanında okunan değer
                combo.Items.Add(read[sql2].ToString());
            }
            baglanti.Close();
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control item3 in groupBox1.Controls)
            {
                item3.Enabled = true;
            }           
            DateTime bugun = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime yeni = DateTime.Parse(dateTimePicker2.Text);
            if (yeni == bugun)
            {
                foreach (Control item1 in groupBox1.Controls)
                {
                    if (item1== dateTimePicker2 && DateTime.Parse(DateTime.Now.ToShortTimeString()) > DateTime.Parse(item1.Text))
                    {                       
                        item1.Enabled = false;
                    }
                }
                Tarihi_Karsilastir();
            }
            else if (yeni > bugun)
            {
                Tarihi_Karsilastir();
            }
            else if (yeni < bugun)
            {
                MessageBox.Show("Geriye dönük işlem yapılamaz", "Uyarı");
                dateTimePicker2.Text = DateTime.Now.ToShortDateString();
            }
        }
        private void Tarihi_Karsilastir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from seans_bilgileri where salonadi='"+comboSalon.Text+"' and tarih='" + dateTimePicker2.Text+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read()==true)
            {
                foreach (Control item2 in groupBox1.Controls)
                {
                    if (read["seans"].ToString() == item2.Text)
                    {
                        item2.Enabled = false;
                    }
                }
            }
            baglanti.Close();
        }
        string Seans = "";
        private void RadioButtonSeçiliyse()
        {
            if (radioButton1.Checked == true) Seans = radioButton1.Text;
            else if (radioButton2.Checked == true) Seans = radioButton2.Text;
            else if (radioButton3.Checked == true) Seans = radioButton3.Text;
            else if (radioButton4.Checked == true) Seans = radioButton4.Text;
            else if (radioButton5.Checked == true) Seans = radioButton5.Text;
            else if (radioButton6.Checked == true) Seans = radioButton6.Text;
            else if (radioButton7.Checked == true) Seans = radioButton7.Text;
            else if (radioButton8.Checked == true) Seans = radioButton8.Text;
            else if (radioButton9.Checked == true) Seans = radioButton9.Text;
            else if (radioButton10.Checked == true) Seans = radioButton10.Text;
            else if (radioButton11.Checked == true) Seans = radioButton11.Text;
            else if (radioButton12.Checked == true) Seans = radioButton12.Text;
        }

        private void comboSalon_SelectedIndexChanged(object sender, EventArgs e)
        {            
            dateTimePicker2.Text = DateTime.Now.ToShortDateString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            RadioButtonSeçiliyse();
            if (Seans !="")
            {
                filmseansi.SeansEkleme(comboFilm.Text, comboSalon.Text, dateTimePicker2.Text, Seans);
                MessageBox.Show("Seans ekleme işlemi yapıldı", "Kayıt");
            }
            else if (Seans == "")
            {
                MessageBox.Show("Seans seçimi yapmadınız", "Uyarı");
            }
            comboFilm.Text = "";
            comboSalon.Text = "";
            dateTimePicker2.Text = DateTime.Now.ToShortDateString();
        }
    }
}
