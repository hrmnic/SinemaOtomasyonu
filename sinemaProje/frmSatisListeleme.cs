using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sinemaProje
{
    public partial class frmSatisListeleme : Form
    {
        public frmSatisListeleme()
        {
            InitializeComponent();
        }
        sinemaTableAdapters.Satis_BilgileriTableAdapter satislistesi = new sinemaTableAdapters.Satis_BilgileriTableAdapter();

        private void frmSatisListeleme_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = satislistesi.TarihListeleme2(dateTimePicker1.Text);
            ToplamUcretHesapla();
        }
        private void ToplamUcretHesapla()
        {           
            int ucrettoplami = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ucrettoplami += Convert.ToInt32(dataGridView1.Rows[i].Cells["ucret"].Value);

            }
            label1.Text = "Toplam Satış=" + ucrettoplami + "TL";
        }

        private void btnTumSatislar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = satislistesi.SatisListesi2();
            ToplamUcretHesapla();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = satislistesi.TarihListeleme2(dateTimePicker1.Text);
            ToplamUcretHesapla();
        }       
    }
}
