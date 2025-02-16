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
    public partial class FormListPenerbangan : Form
    {
        private DataBaseDataContext db = new DataBaseDataContext();

        DateTime dtp;
        decimal nup;
        int berangkatId;
        int keId;

        public FormListPenerbangan(int berangkatId, int keId, DateTime dtp, decimal nup)
        {
            InitializeComponent();
            this.dtp = dtp;
            this.nup = nup;
            this.berangkatId = berangkatId;
            this.keId = keId;
        }

        private void loadCbo()
        {

            var list = new List<string>();
            list.Add("Harga Terendah");
            list.Add("Keberangkatan paling awal");
            list.Add("Keberangkatan paling akhir");
            list.Add("Kedatangan paling awal");
            list.Add("Kedatangan paling akhir");
            list.Add("Durasi Tercepat");

            cboUrutkan.DataSource = list;
        }

        private void loadDgvData()
        {
            dgvData.Columns.Clear();

            var btnBeliTiket = new DataGridViewButtonColumn();
            btnBeliTiket.Text = "BeliTiket";
            btnBeliTiket.HeaderText = "";
            btnBeliTiket.Name = "btnBeliTiket";
            btnBeliTiket.UseColumnTextForButtonValue = true;

            var startTime = TimeSpan.MinValue;
            var endTime = TimeSpan.MaxValue;

            if (checkBox1.Checked)
            {
                startTime = TimeSpan.FromHours(0);
                endTime = TimeSpan.FromHours(6);
            }

            if (checkBox2.Checked)
            {
                startTime = TimeSpan.FromHours(6);
                endTime = TimeSpan.FromHours(12);
            }

            if (checkBox3.Checked)
            {
                startTime = TimeSpan.FromHours(12);
                endTime = TimeSpan.FromHours(18);
            }

            if (checkBox4.Checked)
            {
                startTime = TimeSpan.FromHours(18);
                endTime = TimeSpan.FromHours(24);
            }


            if (cboUrutkan.Text == "Harga Terendah")
            {
                var query = db.JadwalPenerbangans.Where(x => (x.TanggalWaktuKeberangkatan.TimeOfDay >= startTime && x.TanggalWaktuKeberangkatan.TimeOfDay <= endTime) && x.BandaraKeberangkatanID == berangkatId && x.BandaraTujuanID == keId && x.TanggalWaktuKeberangkatan.Date == dtp.Date)
                    .OrderBy(x => x.HargaPerTiket).Select(x => new {
                    x.KodePenerbangan,
                    Maskapai = x.Maskapai.Nama,
                    BandaraKeberangkatan = x.Bandara.Nama,
                    BandaraTujuan = x.Bandara1.Nama,
                    x.HargaPerTiket,
                    x.ID,

                    TanggalKeberangkatan = x.TanggalWaktuKeberangkatan.ToShortDateString().Replace('/', '-'),
                    WaktuPenerbangan = $"{(x.TanggalWaktuKeberangkatan.ToShortTimeString().Replace(".", ":"))} - {x.TanggalWaktuKeberangkatan.AddMinutes(x.DurasiPenerbangan).ToShortTimeString().Replace('.', ':')}",
                });

                dgvData.DataSource = query.ToList();
                dgvData.Columns.Add(btnBeliTiket);

                dgvData.Columns["ID"].Visible = false;
            }

            if (cboUrutkan.Text == "Keberangkatan paling awal")
            {
                var query = db.JadwalPenerbangans.Where(x => (x.TanggalWaktuKeberangkatan.TimeOfDay >= startTime && x.TanggalWaktuKeberangkatan.TimeOfDay <= endTime) && x.BandaraKeberangkatanID == berangkatId && x.BandaraTujuanID == keId && x.TanggalWaktuKeberangkatan.Date == dtp.Date)
                    .OrderBy(x => x.TanggalWaktuKeberangkatan).Select(x => new {
                    x.KodePenerbangan,
                    Maskapai = x.Maskapai.Nama,
                    BandaraKeberangkatan = x.Bandara.Nama,
                    BandaraTujuan = x.Bandara1.Nama,
                    x.HargaPerTiket,
                    x.ID,
                    TanggalKeberangkatan = x.TanggalWaktuKeberangkatan.ToShortDateString().Replace('/', '-'),
                    WaktuPenerbangan = $"{(x.TanggalWaktuKeberangkatan.ToShortTimeString().Replace(".", ":"))} - {x.TanggalWaktuKeberangkatan.AddMinutes(x.DurasiPenerbangan).ToShortTimeString().Replace('.', ':')}"

                });

                dgvData.DataSource = query;
                dgvData.Columns.Add(btnBeliTiket);

                dgvData.Columns["ID"].Visible = false;
            }

            if (cboUrutkan.Text == "Keberangkatan paling akhir")
            {
                var query = db.JadwalPenerbangans.Where(x => (x.TanggalWaktuKeberangkatan.TimeOfDay >= startTime && x.TanggalWaktuKeberangkatan.TimeOfDay <= endTime) && x.BandaraKeberangkatanID == berangkatId && x.BandaraTujuanID == keId && x.TanggalWaktuKeberangkatan.Date == dtp.Date)
                    .OrderByDescending(x => x.TanggalWaktuKeberangkatan).Select(x => new {
                    x.KodePenerbangan,
                    Maskapai = x.Maskapai.Nama,
                    BandaraKeberangkatan = x.Bandara.Nama,
                    BandaraTujuan = x.Bandara1.Nama,
                    x.HargaPerTiket,
                    x.ID,
                    TanggalKeberangkatan = x.TanggalWaktuKeberangkatan.ToShortDateString().Replace('/', '-'),
                    WaktuPenerbangan = $"{(x.TanggalWaktuKeberangkatan.ToShortTimeString().Replace(".", ":"))} - {x.TanggalWaktuKeberangkatan.AddMinutes(x.DurasiPenerbangan).ToShortTimeString().Replace('.', ':')}"

                });

                dgvData.DataSource = query;
                dgvData.Columns.Add(btnBeliTiket);

                dgvData.Columns["ID"].Visible = false;
            }

            if (cboUrutkan.Text == "Kedatangan paling awal")
            {
                var query = db.JadwalPenerbangans.Where(x => (x.TanggalWaktuKeberangkatan.TimeOfDay >= startTime && x.TanggalWaktuKeberangkatan.TimeOfDay <= endTime) && x.BandaraKeberangkatanID == berangkatId && x.BandaraTujuanID == keId )
                    .OrderBy(x => x.TanggalWaktuKeberangkatan.AddMinutes(x.DurasiPenerbangan)).Select(x => new {
                    x.KodePenerbangan,
                    Maskapai = x.Maskapai.Nama,
                    BandaraKeberangkatan = x.Bandara.Nama,
                    BandaraTujuan = x.Bandara1.Nama,
                    x.HargaPerTiket,
                    x.ID,
                    TanggalKeberangkatan = x.TanggalWaktuKeberangkatan.ToShortDateString().Replace('/', '-'),
                    WaktuPenerbangan = $"{(x.TanggalWaktuKeberangkatan.ToShortTimeString().Replace(".", ":"))} - {x.TanggalWaktuKeberangkatan.AddMinutes(x.DurasiPenerbangan).ToShortTimeString().Replace('.', ':')}"

                });

                dgvData.DataSource = query;
                dgvData.Columns.Add(btnBeliTiket);

                dgvData.Columns["ID"].Visible = false;
            }

            if (cboUrutkan.Text == "Kedatangan paling akhir")
            {
                var query = db.JadwalPenerbangans.Where(x => (x.TanggalWaktuKeberangkatan.TimeOfDay >= startTime && x.TanggalWaktuKeberangkatan.TimeOfDay <= endTime) && x.BandaraKeberangkatanID == berangkatId && x.BandaraTujuanID == keId && x.TanggalWaktuKeberangkatan.Date == dtp.Date)
                    .OrderByDescending(x => x.TanggalWaktuKeberangkatan.AddMinutes(x.DurasiPenerbangan)).Select(x => new {
                    x.KodePenerbangan,
                    Maskapai = x.Maskapai.Nama,
                    BandaraKeberangkatan = x.Bandara.Nama,
                    BandaraTujuan = x.Bandara1.Nama,
                    x.HargaPerTiket,
                    x.ID,
                    TanggalKeberangkatan = x.TanggalWaktuKeberangkatan.ToShortDateString().Replace('/', '-'),
                    WaktuPenerbangan = $"{(x.TanggalWaktuKeberangkatan.ToShortTimeString().Replace(".", ":"))} - {x.TanggalWaktuKeberangkatan.AddMinutes(x.DurasiPenerbangan).ToShortTimeString().Replace('.', ':')}"

                });

                dgvData.DataSource = query;
                dgvData.Columns.Add(btnBeliTiket);

                dgvData.Columns["ID"].Visible = false;
            }

            if (cboUrutkan.Text == "Durasi Tercepat")
            {
                var query = db.JadwalPenerbangans.Where(x => (x.TanggalWaktuKeberangkatan.TimeOfDay >= startTime && x.TanggalWaktuKeberangkatan.TimeOfDay <= endTime) && x.BandaraKeberangkatanID == berangkatId && x.BandaraTujuanID == keId && x.TanggalWaktuKeberangkatan.Date == dtp.Date)
                    .OrderBy(x => x.DurasiPenerbangan).Select(x => new {
                    x.KodePenerbangan,
                    Maskapai = x.Maskapai.Nama,
                    BandaraKeberangkatan = x.Bandara.Nama,
                    BandaraTujuan = x.Bandara1.Nama,
                    x.HargaPerTiket,
                    x.ID,
                    TanggalKeberangkatan = x.TanggalWaktuKeberangkatan.ToShortDateString().Replace('/', '-'),
                    WaktuPenerbangan = $"{(x.TanggalWaktuKeberangkatan.ToShortTimeString().Replace(".", ":"))} - {x.TanggalWaktuKeberangkatan.AddMinutes(x.DurasiPenerbangan).ToShortTimeString().Replace('.', ':')}"

                });

                dgvData.DataSource = query;
                dgvData.Columns.Add(btnBeliTiket);
                dgvData.Columns["ID"].Visible = false;

            }
        }
        private void FormListPenerbangan_Load(object sender, EventArgs e)
        {

            loadCbo();
            loadDgvData();

            var query = db.Bandaras.FirstOrDefault(x => x.ID == berangkatId);
            var query2 = db.Bandaras.FirstOrDefault(x => x.ID == keId);

            if (query != null && query2 != null)
            {
                lblPenerbangan.Text = query.Nama + " ( " + query.KodeIATA + " )" + "  ->  " + query2.Nama + " ( " + query2.KodeIATA + " )" + "  *  " + dtp.ToString("ddd, d MMM yyyy") + "  *  "+ nup + "  Penumpang";
            }

            //var query = db.Bandaras.FirstOrDefault(x => x.ID )

            //lblPenerbangan.Text = berangkatDari + "  ->  " + tujuan + "  *  " + dtp.ToString("ddd, d MMM yyyy") + "  *  "+ nup + "  Penumpang";

        }

        private void cboUrutkan_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnTerapkan_Click(object sender, EventArgs e)
        {
            loadDgvData();

        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvData.Columns["btnBeliTiket"].Index && e.RowIndex >= 0)
            {
                var maskapai = dgvData.Rows[e.RowIndex].Cells["Maskapai"].Value.ToString();
                var waktu = dgvData.Rows[e.RowIndex].Cells["WaktuPenerbangan"].Value.ToString();
                var harga = Convert.ToInt32( dgvData.Rows[e.RowIndex].Cells["HargaPerTiket"].Value);
                var jadwalPenerbanganId = (int) dgvData.Rows[e.RowIndex].Cells["ID"].Value;
                new FormBeliTiket(berangkatId, keId, maskapai, dtp, waktu, nup, harga, keId=jadwalPenerbanganId).Show();
                Hide();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var query = db.Akuns.FirstOrDefault(x => x.ID == Support.akunId);

            if (query != null)
            {
                new FormMainCustomer(query.Nama).Show();
                Hide();
            }
        }
    }
}
