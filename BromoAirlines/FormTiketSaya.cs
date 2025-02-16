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
    public partial class FormTiketSaya : Form
    {
        
        public FormTiketSaya()
        {
            InitializeComponent();
        }

        private void loadDgv()
        {
            dgvData.Columns.Clear();
            var db = new DataBaseDataContext();

            var query = db.TransaksiHeaders.Where(x => x.AkunID == Support.akunId)
                .Select(x => new
                {
                    KodePenerbangan = x.JadwalPenerbangan.KodePenerbangan,
                    Maskapai = x.JadwalPenerbangan.Maskapai.Nama,
                    BandaraKeberangkatan = x.JadwalPenerbangan.Bandara.Nama,
                    BandaraTujuan = x.JadwalPenerbangan.Bandara1.Nama,
                    TanggalKeberangkatan = x.JadwalPenerbangan.TanggalWaktuKeberangkatan.ToShortDateString().Replace('/', '-'),
                    WaktuPenerbangan = $"{(x.JadwalPenerbangan.TanggalWaktuKeberangkatan.ToShortTimeString().Replace(".", ":"))} - {x.JadwalPenerbangan.TanggalWaktuKeberangkatan.AddMinutes(x.JadwalPenerbangan.DurasiPenerbangan).ToShortTimeString().Replace('.', ':')}",

                    StatusTerakhir = db.PerubahanStatusJadwalPenerbangans
                    .Where(y => y.JadwalPenerbanganID == x.JadwalPenerbanganID)
                    .Select(y => y.StatusPenerbangan.Nama)
                    .FirstOrDefault() == "Delay"?
                    "Delay (selama" + (x.JadwalPenerbangan.DurasiPenerbangan / 60) + " jam " + (x.JadwalPenerbangan.DurasiPenerbangan % 60) + " Menit)" :
                    "Sesuai Jadwal",


                });

            dgvData.DataSource = query;
        }

        private void FormTiketSaya_Load(object sender, EventArgs e)
        {
            loadDgv();
        }
    }
}
