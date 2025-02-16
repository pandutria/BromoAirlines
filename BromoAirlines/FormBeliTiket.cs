using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace BromoAirlines
{
    public partial class FormBeliTiket : Form
    {
        private DataBaseDataContext db = new DataBaseDataContext();

        int berangkatId;
        int keId;
        string maskapai;
        DateTime dtp;
        string waktu;
        decimal nup;
        int harga;
        int jadwalPenerbanganId;

        int presentaseDiskon;
        int totalHarga;
        int maximunDiskon;
        public FormBeliTiket(int berangkatId, int keId, string maskapai, DateTime dtp, string waktu, decimal nup, int harga, int jadwalPenerbanganId)
        {
            InitializeComponent();
            this.berangkatId = berangkatId;
            this.keId = keId;
            this.maskapai = maskapai;
            this.dtp = dtp;
            this.waktu = waktu;
            this.nup = nup;
            this.harga = harga;
            this.jadwalPenerbanganId = jadwalPenerbanganId;
        }

        private void loadCbo()
        {
            var list1 = new List<string>();
            list1.Add("-Pilih Titel-");
            list1.Add("Tuan");
            list1.Add("Nyonya");

            var list2 = new List<string>();
            list2.Add("-Pilih Titel-");
            list2.Add("Tuan");
            list2.Add("Nyonya");

            var list3 = new List<string>();
            list3.Add("-Pilih Titel-");
            list3.Add("Tuan");
            list3.Add("Nyonya");

            var list4 = new List<string>();
            list4.Add("-Pilih Titel-");
            list4.Add("Tuan");
            list4.Add("Nyonya");

            var list5 = new List<string>();
            list5.Add("-Pilih Titel-");
            list5.Add("Tuan");
            list5.Add("Nyonya");

            cboTitelP1.DataSource = list1;
            cboTitelP2.DataSource = list2;
            cboTitelP3.DataSource = list3;
            cboTitelP4.DataSource = list4;
            cboTitelP5.DataSource = list5;
        }

        private void FormBeliTiket_Load(object sender, EventArgs e)
        {
            loadCbo();
            var query = db.Bandaras.FirstOrDefault(x => x.ID == berangkatId);
            var query2 = db.Bandaras.FirstOrDefault(x => x.ID == keId);

            if (query != null && query2 != null)
            {
                lblBandara.Text = query.Nama + " (" + query.KodeIATA + ")" + "  ->  " + query2.Nama + " (" + query2.KodeIATA + ")";
            }

            lblMaskapai.Text = maskapai;
            lblDate.Text = dtp.ToString("dddd, dd MMM yyyy");
            lblPenumpang.Text = nup.ToString() + " Penumpang";
            lblTime.Text = waktu.ToString();

            totalHarga = ((int)nup * harga);
            lblTotal.Text = "IDR " + totalHarga.ToString("N0").Replace(',', '.');

            panel1.AutoScroll = true;
        }

        private void btnPakai_Click(object sender, EventArgs e)
        {
            if (tbKodePromo.Text == string.Empty)
            {
                Support.msw("field kode promo must be filled");
            }
            else
            {
                var query = db.KodePromos.FirstOrDefault(x => x.Kode == tbKodePromo.Text);

                if (query != null)
                {
                    if (dtp.Date > query.BerlakuSampai.Date)
                    {
                        Support.msw("kode sudah kadaluwarsa");
                        tbKodePromo.Text = string.Empty;
                    }
                    else
                    {
                        var diskon = ((int)nup * harga) * presentaseDiskon / 100;

                        presentaseDiskon = (int)query.PersentaseDiskon;
                        maximunDiskon = (int)query.MaksimumDiskon;

                        if (diskon > maximunDiskon)
                        {
                            diskon = maximunDiskon;
                            totalHarga = ((int)nup * harga) - diskon;
                            lblTotal.Text = "IDR " + totalHarga.ToString("N0").Replace(',', '.');
                        }
                        else
                        {
                            totalHarga = ((int)nup * harga) - diskon;
                            lblTotal.Text = "IDR " + totalHarga.ToString("N0").Replace(',', '.');
                        }
                    }

                }
                else
                {
                    Support.msw("kode not found");
                    tbKodePromo.Text = string.Empty;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnKonfirmasi_Click(object sender, EventArgs e)
        {

            if (tbNamaLengkapP1.Text == string.Empty && cboTitelP1.Text == "-Pilih Titel-")
            {
                Support.msw("data penumpang dan titel wajib di isi");

            }

            else
            {
                var newHeader = new TransaksiHeader();
                newHeader.AkunID = Support.akunId;
                newHeader.TanggalTransaksi = DateTime.Now;
                newHeader.JadwalPenerbanganID = jadwalPenerbanganId;
                newHeader.JumlahPenumpang = (int)nup;
                newHeader.TotalHarga = totalHarga;

                if (tbKodePromo.Text == string.Empty)
                {
                    newHeader.KodePromoID = null;
                }

                else
                {
                    var queryKode = db.KodePromos.FirstOrDefault(x => x.Kode == tbKodePromo.Text);

                    if (queryKode != null)
                    {
                        newHeader.KodePromoID = queryKode.ID;
                    }
                }

                db.TransaksiHeaders.InsertOnSubmit(newHeader);
                db.SubmitChanges();

                var newDetailMain = new TransaksiDetail();
                newDetailMain.TransaksiHeaderID = newHeader.ID;
                newDetailMain.TitelPenumpang = cboTitelP1.Text;
                newDetailMain.NamaLengkapPenumpang = tbNamaLengkapP1.Text;

                db.TransaksiDetails.InsertOnSubmit(newDetailMain);
                db.SubmitChanges();

                if (tbNamaLengkapP2.Text != string.Empty && cboTitelP2.Text != "-Pilih Titel-")
                {
                    var newDetail = new TransaksiDetail();
                    newDetail.TransaksiHeaderID = newHeader.ID;
                    newDetail.TitelPenumpang = cboTitelP2.Text;
                    newDetail.NamaLengkapPenumpang = tbNamaLengkapP2.Text;

                    db.TransaksiDetails.InsertOnSubmit(newDetail);
                    db.SubmitChanges();
                }

                if (tbNamaLengkapP3.Text != string.Empty && cboTitelP3.Text != "-Pilih Titel-")
                {
                    var newDetail = new TransaksiDetail();
                    newDetail.TransaksiHeaderID = newHeader.ID;
                    newDetail.TitelPenumpang = cboTitelP3.Text;
                    newDetail.NamaLengkapPenumpang = tbNamaLengkapP3.Text;

                    db.TransaksiDetails.InsertOnSubmit(newDetail);
                    db.SubmitChanges();
                }

                if (tbNamaLengkapP4.Text != string.Empty && cboTitelP4.Text != "-Pilih Titel-")
                {
                    var newDetail = new TransaksiDetail();
                    newDetail.TransaksiHeaderID = newHeader.ID;
                    newDetail.TitelPenumpang = cboTitelP4.Text;
                    newDetail.NamaLengkapPenumpang = tbNamaLengkapP4.Text;

                    db.TransaksiDetails.InsertOnSubmit(newDetail);
                    db.SubmitChanges();
                }

                if (tbNamaLengkapP5.Text != string.Empty && cboTitelP5.Text != "-Pilih Titel-")
                {
                    var newDetail = new TransaksiDetail();
                    newDetail.TransaksiHeaderID = newHeader.ID;
                    newDetail.TitelPenumpang = cboTitelP5.Text;
                    newDetail.NamaLengkapPenumpang = tbNamaLengkapP5.Text;

                    db.TransaksiDetails.InsertOnSubmit(newDetail);
                    db.SubmitChanges();
                }

                var queryName = db.Akuns.FirstOrDefault(x => x.ID == Support.akunId);
                Support.msi("berhasil");
                new FormMainCustomer(queryName.Nama).Show();
                Hide();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new FormListPenerbangan(berangkatId, keId,dtp, nup).Show();
            Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}
