using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BromoAirlines
{
    public partial class FormMasterJadwalPenerbangan : Form
    {
        private DataBaseDataContext db = new DataBaseDataContext();
        int currentSelectedRow = -1;
        string mode = "";

        public FormMasterJadwalPenerbangan()
        {
            InitializeComponent();
            dtpTanggal.CustomFormat = "dd-MM-yyyy";
            dtpTanggal.Format = DateTimePickerFormat.Custom;
        }

        private void loadCboData()
        {
            var queryBandara = db.Bandaras.OrderBy(x => x.Nama);

            cboDari.DataSource = queryBandara;
            cboDari.ValueMember = "ID";
            cboDari.DisplayMember = "Nama";

            cboKe.DataSource = queryBandara;
            cboKe.ValueMember = "ID";
            cboKe.DisplayMember = "Nama";

            var queryMaskapai = db.Maskapais;

            cboMaskapai.DataSource = queryMaskapai;
            cboMaskapai.ValueMember = "ID";
            cboMaskapai.DisplayMember = "Nama";

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

            var btnHapus = new DataGridViewButtonColumn();
            btnHapus.Name = "btnHapus";
            btnHapus.Text = "Hapus";
            btnHapus.HeaderText = "";
            btnHapus.UseColumnTextForButtonValue = true;

            var query = db.JadwalPenerbangans.OrderByDescending(x => x.TanggalWaktuKeberangkatan).Select(x => new
            {
                x.KodePenerbangan,
                BandaraKeberangkatan = x.Bandara.Nama,
                BandaraTujuan = x.Bandara1.Nama,
                Maskapai = x.Maskapai.Nama,
                TanggalKeberangkatan = x.TanggalWaktuKeberangkatan.ToLocalTime().ToString("dd-MM-yyy"),
                WaktuKeberangkatan = x.TanggalWaktuKeberangkatan.ToShortTimeString().Replace('.', ':'),
                DurasiPenerbangan = (x.DurasiPenerbangan / 60) + " jam " + (x.DurasiPenerbangan % 60) + " menit",
                x.HargaPerTiket,
                x.ID,
            });

            dgvData.DataSource = query;
            dgvData.Columns["ID"].Visible = false;

            dgvData.Columns.Add(btnUbah);
            dgvData.Columns.Add(btnHapus);

        }

        private void FormMasterJadwalPenerbangan_Load(object sender, EventArgs e)
        {
            loadDgvData();
            loadCboData();

            tbKodePenerbangan.Text = "_,_";
            tbWaktuKeberangkatan.Text = "00:00";
            tbDurasiPenerbangan.Text = "_jam_menit";
            nupHargPerTiket.Value = 1;

            nupHargPerTiket.Minimum = 0;
            nupHargPerTiket.Maximum = 9999999;


        }

        private bool validation()
        {
            if (tbKodePenerbangan.Text == string.Empty || tbDurasiPenerbangan.Text == string.Empty
                || tbWaktuKeberangkatan.Text == string.Empty || nupHargPerTiket.Value == 0

                )
            {
                Support.msw("all field must be filled");
                return false;
            }

            if (nupHargPerTiket.Value < 1)
            {
                Support.msw("Jumlah terminal yang diinput minimal satu (1).");
                return false;
            }

            if (cboDari.SelectedValue == cboKe.SelectedValue)
            {
                Support.msw("Bandara keberangkatan dan tujuan yang dipilih tidak boleh sama.");
                return false;
            }

            return true;
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                currentSelectedRow = e.RowIndex;
                currentSelectedRow = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["ID"].Value);
                tbKodePenerbangan.Text = dgvData.Rows[e.RowIndex].Cells["KodePenerbangan"].Value.ToString();
                tbDurasiPenerbangan.Text = dgvData.Rows[e.RowIndex].Cells["DurasiPenerbangan"].Value.ToString();
                tbWaktuKeberangkatan.Text = dgvData.Rows[e.RowIndex].Cells["WaktuKeberangkatan"].Value.ToString();
                nupHargPerTiket.Value = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["HargaPerTiket"].Value);
                cboDari.Text = dgvData.Rows[e.RowIndex].Cells["BandaraKeberangkatan"].Value.ToString();
                cboKe.Text = dgvData.Rows[e.RowIndex].Cells["BandaraTujuan"].Value.ToString();


            }

            if (e.ColumnIndex == dgvData.Columns["btnHapus"].Index && e.RowIndex >= 0)
            {
                var msgDialog = MessageBox.Show("are sure want to delete this data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgDialog == DialogResult.Yes)
                {
                    var queryDelete = db.JadwalPenerbangans.FirstOrDefault(x => x.ID == currentSelectedRow);
                    if (queryDelete != null)
                    {
                        db.JadwalPenerbangans.DeleteOnSubmit(queryDelete);
                        db.SubmitChanges();
                        Support.msi("delete data berhasil");
                        Support.clearField(this);
                        loadDgvData();
                    }
                }
            }

            if (e.ColumnIndex == dgvData.Columns["btnUbah"].Index && e.RowIndex >= 0)
            {
                mode = "ubah";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (validation())
                {
                    if (mode == "ubah")
                    {
                        var queryUpdate = db.JadwalPenerbangans.FirstOrDefault(x => x.ID == currentSelectedRow);

                        if (queryUpdate != null)
                        {
                            queryUpdate.KodePenerbangan = tbKodePenerbangan.Text;
                            queryUpdate.BandaraKeberangkatanID = Convert.ToInt32(cboDari.SelectedValue);
                            queryUpdate.BandaraTujuanID = Convert.ToInt32(cboKe.SelectedValue);
                            queryUpdate.MaskapaiID = Convert.ToInt32(cboMaskapai.SelectedValue);
                            queryUpdate.TanggalWaktuKeberangkatan = dtpTanggal.Value;
                            queryUpdate.DurasiPenerbangan = Convert.ToInt32( tbDurasiPenerbangan.Text);
                            queryUpdate.HargaPerTiket = Convert.ToInt32(nupHargPerTiket.Value);


                            db.SubmitChanges();
                            Support.msi("ubah data berhasi");
                            loadDgvData();
                            Support.clearField(this);
                            mode = "";
                        }
                    }

                    else
                    {

                        var query = new JadwalPenerbangan();
                        query.KodePenerbangan = tbKodePenerbangan.Text;
                        query.BandaraKeberangkatanID = Convert.ToInt32(cboDari.SelectedValue);
                        query.BandaraTujuanID = Convert.ToInt32(cboKe.SelectedValue);
                        query.MaskapaiID = Convert.ToInt32(cboMaskapai.SelectedValue);
                        query.TanggalWaktuKeberangkatan = dtpTanggal.Value;
                        query.DurasiPenerbangan = Convert.ToInt32(tbDurasiPenerbangan.Text);
                        query.HargaPerTiket = Convert.ToInt32(nupHargPerTiket.Value);
                        db.JadwalPenerbangans.InsertOnSubmit(query);
                        Support.clearField(this);
                        Support.msi("insert data berhasi");
                        loadDgvData();


                    }

                }

            }
            catch (Exception ex)
            {
                Support.mse(ex.Message);
            }
        }
    }
}
