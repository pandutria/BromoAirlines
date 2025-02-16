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
    public partial class FormUbahStatusPenerbangan : Form
    {
        private DataBaseDataContext db = new DataBaseDataContext();
        int currentSelectedRow = -1;
        string mode = "";
        string terakhirDiUbah = "";
        int durasi = 0;
        public FormUbahStatusPenerbangan()
        {
            InitializeComponent();
        }

        private void loaCboData()
        {
            var query = db.StatusPenerbangans;

            cboStatus.DataSource = query;
            cboStatus.ValueMember = "ID";
            cboStatus.DisplayMember = "Nama";
        }

        private void loadDgvData()
        {
            dgvData.Columns.Clear();
            dgvData.Rows.Clear();

            var btnUbah = new DataGridViewButtonColumn();
            btnUbah.Name = "btnUbah";
            btnUbah.Text = "Ubah";
            btnUbah.HeaderText = "";
            btnUbah.UseColumnTextForButtonValue = true;

            var query = db.JadwalPenerbangans.Select(x => new
            {
                x.KodePenerbangan,
                BandaraKeberangkatan = x.Bandara.Nama,
                BandaraTujuan = x.Bandara1.Nama,
                Maskapai = x.Maskapai.Nama,
                TanggalKeberangkatan = x.TanggalWaktuKeberangkatan.ToShortDateString().Replace('/', '-'),
                WaktuKeberangkatan = x.TanggalWaktuKeberangkatan.ToShortTimeString().Replace('.', ':'),
                DurasiPenerbangan = (x.DurasiPenerbangan / 60) + " Jam " + (x.DurasiPenerbangan % 60) + " Menit",
                x.HargaPerTiket,

                StatusTerakhir = db.PerubahanStatusJadwalPenerbangans
                .Where(y => y.JadwalPenerbanganID == x.ID)
                .Select(y => y.StatusPenerbangan.Nama)
                .FirstOrDefault() == "Delay" ?
                "Delay " + (x.DurasiPenerbangan / 60) + " Jam " + (x.DurasiPenerbangan % 60) + " Menit" :
                "Sesuai Jadwal",

                TerakhirDiubah = db.PerubahanStatusJadwalPenerbangans
                .Where(y => y.JadwalPenerbanganID == x.ID)
                .Select(y => y.WaktuPerubahanTerjadi.ToString())
                .FirstOrDefault().ToString() ?? "-",
                x.ID,
            });

            dgvData.DataSource = query;

            dgvData.Columns["ID"].Visible = false;
            dgvData.Columns["DurasiPenerbangan"].Visible = false;
 
            dgvData.Columns.Add(btnUbah);

        }

        private void FormUbahStatusPenerbangan_Load(object sender, EventArgs e)
        {
            loadDgvData();
            loaCboData();
            visible(true);
        }

        private void visible(bool v)
        {
            lblStatus.Visible = cboStatus.Visible = !v;
            btnBatal.Visible = btnSave.Visible = !v;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (terakhirDiUbah == "-")
                {
                    var query = new PerubahanStatusJadwalPenerbangan();
                    query.JadwalPenerbanganID = currentSelectedRow;
                    query.StatusPenerbanganID = Convert.ToInt32(cboStatus.SelectedValue);
                    query.WaktuPerubahanTerjadi = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyy HH:mm:ss"));
                    if (cboStatus.Text == "Delay")
                    {
                        query.PerkiraanDurasiDelay = Convert.ToInt32(tbPerkiraanDelay.Text);
                    }
                    else
                    {
                        query.PerkiraanDurasiDelay = null;
                    }

                    db.PerubahanStatusJadwalPenerbangans.InsertOnSubmit(query);
                    db.SubmitChanges();
                    loadDgvData();
                    Support.msi("berhasil");
                    visible(true);

                }
                else
                {

                    var queryUpdate = db.PerubahanStatusJadwalPenerbangans.FirstOrDefault(x => x.JadwalPenerbanganID == currentSelectedRow);

                    if (queryUpdate != null)
                    {
                        queryUpdate.JadwalPenerbanganID = currentSelectedRow;
                        queryUpdate.StatusPenerbanganID = Convert.ToInt32(cboStatus.SelectedValue);
                        queryUpdate.WaktuPerubahanTerjadi = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyy HH:mm:ss"));
                        if (cboStatus.Text == "Delay")
                        {
                            queryUpdate.PerkiraanDurasiDelay = Convert.ToInt32(durasi);
                        }
                        else
                        {
                            queryUpdate.PerkiraanDurasiDelay = null;
                        }

                        db.SubmitChanges();
                        loadDgvData();
                        Support.msi("berhasil");
                        visible(true);
                    }


                }


            }
            catch (Exception ex)
            {
                Support.mse(ex.Message);
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvData.Columns["btnUbah"].Index && e.RowIndex >= 0)
            {
                visible(false);
            }

            if (e.RowIndex >= 0)
            {
                currentSelectedRow = e.RowIndex;
                currentSelectedRow =  (int) dgvData.Rows[e.RowIndex].Cells["ID"].Value;
                terakhirDiUbah = dgvData.Rows[e.RowIndex].Cells["TerakhirDiubah"].Value.ToString();
                cboStatus.Text = dgvData.Rows[e.RowIndex].Cells["StatusTerakhir"].Value.ToString();
                tbPerkiraanDelay.Text = dgvData.Rows[e.RowIndex].Cells["DurasiPenerbangann"].Value.ToString();
                durasi = (int) dgvData.Rows[e.RowIndex].Cells["DurasiPenerbangan"].Value;
            }
        }

        private void cboStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboStatus.Text == "Delay")
            {

                lblDelay.Visible = tbPerkiraanDelay.Visible = true;
            }
            else
            {

                lblDelay.Visible = tbPerkiraanDelay.Visible = false;
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            visible(true);
            lblDelay.Visible = tbPerkiraanDelay.Visible = false;
        }
    }
}
