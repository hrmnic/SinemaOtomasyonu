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
    public partial class frmSalonEkle : Form
    {
        public frmSalonEkle()
        {
            InitializeComponent();
        }
        private void frmSalonEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmAnasayfa anasayfa = new frmAnasayfa();
            anasayfa.ShowDialog();
        }
        sinemaTableAdapters.Salon_BilgileriTableAdapter salon = new sinemaTableAdapters.Salon_BilgileriTableAdapter();

        private void frmSalonEkle_Load(object sender, EventArgs e)
        {

        }
        private void btnSalonEkle_Click(object sender, EventArgs e)
        {
            try
            {
                salon.SalonEkleme(txtSalonAdi.Text);
                MessageBox.Show("Salon eklendi", "Kayıt");


            }
            catch (Exception ex)
            {

                MessageBox.Show("Aynı salonu daha önce eklediniz!", "Uyarı");
            }
            txtSalonAdi.Text = "";
        }
    }
}
