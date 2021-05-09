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
    public partial class frmFilmEkle : Form
    {
        public frmFilmEkle()
        {
            InitializeComponent();
        }
        sinemaTableAdapters.Film_BilgileriTableAdapter film = new sinemaTableAdapters.Film_BilgileriTableAdapter();       
        private void btnFilmEkle_Click(object sender, EventArgs e)
        {            
            try
            {
                film.FilmEkleme(txtFilmAdi.Text, txtYonetmen.Text, comboFilmTuru.Text, txtFilmSure.Text, txtFilmYil.Text, dateTimePicker1.Text, pictureBox1.ImageLocation);
                MessageBox.Show("Film bilgileri eklendi","Kayıt");
            }
            catch(Exception)
            {
                MessageBox.Show("Bu film daha önce eklendi","Uyarı");
            }
            foreach (Control item in Controls) if (item is TextBox) item.Text = "";          
            comboFilmTuru.Text = "";
            pictureBox1.ImageLocation = "";
        }        
        private void btnAfisSec_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }       
    }
}
