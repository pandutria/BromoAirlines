using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BromoAirlines
{
    public partial class FormMainCustomer : Form
    {
        private DataBaseDataContext db = new DataBaseDataContext();
        string nama;

        int berangkatId;
        int keId;
        public FormMainCustomer(string nama)
        {
            InitializeComponent();
            this.nama = nama;
        }

        private void FormMainCustomer_Load(object sender, EventArgs e)
        {
            label4.Text = "Mau terbang ke mana hari ini?, " + nama;
            autoCompelete();
            dtpTanggalBerangkat.Format = DateTimePickerFormat.Custom;
            dtpTanggalBerangkat.CustomFormat = DateTime.Now.ToString("dddd ,d MMMM yyyy");
        }

        private void autoCompelete()
        { 
            var query = db.Bandaras.Select(x => x.Nama + ", " + x.Kota + " " + "(" + x.KodeIATA + ")");

            tbBerangkatDari.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbBerangkatDari.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tbBerangkatDari.AutoCompleteCustomSource.Clear();
            tbBerangkatDari.AutoCompleteCustomSource.AddRange(query.ToArray());

            tbTujuan.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbTujuan.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tbTujuan.AutoCompleteCustomSource.Clear();
            tbTujuan.AutoCompleteCustomSource.AddRange(query.ToArray());
        }
      

        private bool validation()
        {
            if (tbBerangkatDari.Text == string.Empty || tbTujuan.Text == string.Empty)
            {
                Support.msw("all field must be filled");
                return false;
            } 

            if (numericUpDown1.Value < 1 && numericUpDown1.Value > 3)
            {
                Support.msw("Jumlah penumpang minimal satu (1) & maximal 3");
                return false;
            }

            return true;
        }

        private void tbBerangkatDari_TextChanged(object sender, EventArgs e)
        {
            var query = db.Bandaras.FirstOrDefault(x => (x.Nama + ", " + x.Kota + " " + "(" + x.KodeIATA + ")") == tbBerangkatDari.Text);

            if (query != null)
            {
                berangkatId = query.ID;
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                new FormListPenerbangan(berangkatId,keId ,dtpTanggalBerangkat.Value, numericUpDown1.Value).Show();
                Hide();
            }
        }

        private void tbTujuan_TextChanged(object sender, EventArgs e)
        {
            var query = db.Bandaras.FirstOrDefault(x => (x.Nama + ", " + x.Kota + " " + "(" + x.KodeIATA + ")") == tbTujuan.Text);

            if (query != null)
            {
                keId = query.ID;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            new FormTiketSaya().Show();
            Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            new FormLogin().Show();
            Hide();
        }
    }
}
