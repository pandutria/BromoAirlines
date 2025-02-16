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
    public partial class FormMasterBandara : Form
    {
        private DataBaseDataContext db = new DataBaseDataContext();
        int currentSelectedRow = -1;
        string mode = "";

        public FormMasterBandara()
        {
            InitializeComponent();
        }

        //private void enableField(bool e)
        //{
        //    tbName.Enabled = !e;
        //    tbKode.Enabled = !e;
        //    tbAlamat.Enabled = !e;
        //    tbKota.Enabled = !e;
        //    nupJumlahTerminal.Enabled = !e;
        //    cboNegara.Enabled = !e;
        //}

        //private void enableButton(bool e)
        //{
        //    btnSave.Enabled = !e;
        //    btnBatal.Enabled = !e;
        //}

        //private void enableFieldAndButton(bool e)
        //{
        //    enableField(e);
        //    enableButton(e);
        //}

        private bool validation()
        {
            if (tbName.Text == string.Empty || tbKota.Text == string.Empty ||
                tbKode.Text == string.Empty || tbAlamat.Text == string.Empty)
            {
                Support.msw("all field must be filled");
                return false;
            }

            if (tbKode.Text.Length != 3)
            {
                Support.msw("Kode IATA yang diinput harus berupa 3 huruf.");
                return false;
            }

            if (nupJumlahTerminal.Value > 1)
            {
                Support.msw("Jumlah terminal yang diinput minimal satu (1).");
                return false;
            }

            if (tbKode.Text != tbKode.Text.ToString().ToUpper())  
            {
                Support.msw("kode harus gede semua");
                return false;
            }

            return true;
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

            var query = db.Bandaras.OrderBy(x => x.Nama).Select(x => new
            {
                x.Nama,
                x.KodeIATA,
                x.Kota,
                Negara = x.Negara.Nama,
                x.JumlahTerminal,
                x.Alamat,
                x.ID
            });

            dgvData.DataSource = query;
            dgvData.Columns["ID"].Visible = false;

            dgvData.Columns.Add(btnUbah);
            dgvData.Columns.Add(btnHapus);

        }

        private void loadCboNegara()
        {
            cboNegara.DataSource = db.Negaras;
            cboNegara.ValueMember = "ID";
            cboNegara.DisplayMember = "Nama";
        }

        private void FormMasterBandara_Load(object sender, EventArgs e)
        {
            loadDgvData();
            loadCboNegara();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                currentSelectedRow = e.RowIndex;
                currentSelectedRow = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["ID"].Value);
                tbName.Text = dgvData.Rows[e.RowIndex].Cells["Nama"].Value.ToString();
                tbAlamat.Text = dgvData.Rows[e.RowIndex].Cells["Alamat"].Value.ToString();
                cboNegara.Text = dgvData.Rows[e.RowIndex].Cells["Negara"].Value.ToString();
                tbKode.Text = dgvData.Rows[e.RowIndex].Cells["KodeIATA"].Value.ToString();
                tbName.Text = dgvData.Rows[e.RowIndex].Cells["Nama"].Value.ToString();
                tbKota.Text = dgvData.Rows[e.RowIndex].Cells["Kota"].Value.ToString();
                nupJumlahTerminal.Value = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["JumlahTerminal"].Value);
            }

            if (e.ColumnIndex == dgvData.Columns["btnHapus"].Index && e.RowIndex >= 0)
            {
                var msgDialog = MessageBox.Show("are sure want to delete this data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgDialog == DialogResult.Yes)
                {
                    var queryDelete = db.Bandaras.FirstOrDefault(x => x.ID == currentSelectedRow);
                    if (queryDelete != null)
                    {
                        db.Bandaras.DeleteOnSubmit(queryDelete);
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
                        var queryUpdate = db.Bandaras.FirstOrDefault(x => x.ID == currentSelectedRow);

                        if (queryUpdate != null)
                        {
                            queryUpdate.Nama = tbName.Text;
                            queryUpdate.KodeIATA = tbKode.Text;
                            queryUpdate.Kota = tbKota.Text;
                            queryUpdate.NegaraID = Convert.ToInt32(cboNegara.SelectedValue);
                            queryUpdate.JumlahTerminal = Convert.ToInt32(nupJumlahTerminal.Value);
                            queryUpdate.Alamat = tbAlamat.Text;
                            db.SubmitChanges();
                            Support.msi("ubah data berhasi");
                            loadDgvData();
                            Support.clearField(this);
                            mode = "";
                        }
                    }

                    else
                    {
                        var validationName = db.Bandaras.FirstOrDefault(x => x.Nama == tbName.Text);

                        if (validationName != null)
                        {
                            Support.msw("Nama bandara yang diinput harus unik dan tidak boleh duplikat dengan data yang sudah\r\nada");

                        }

                        var validationKode = db.Bandaras.FirstOrDefault(x => x.KodeIATA == tbKode.Text);

                        if (validationKode != null)
                        {
                            Support.msw("Kode IATA yang diinput harus berupa 3 huruf. Kode IATA yang diinput juga harus unik dan\r\ntidak boleh duplikat dengan data yang sudah ada.");
                        }

                        else
                        {
                            var query = new Bandara();
                            query.Nama = tbName.Text;
                            query.KodeIATA = tbKode.Text;
                            query.Kota = tbKota.Text;
                            query.NegaraID = Convert.ToInt32(cboNegara.SelectedValue);
                            query.JumlahTerminal = Convert.ToInt32(nupJumlahTerminal.Value);
                            query.Alamat = tbAlamat.Text;
                            db.Bandaras.InsertOnSubmit(query);
                            db.SubmitChanges();
                            Support.clearField(this);
                            Support.msi("insert data berhasi");
                            loadDgvData();

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Support.mse(ex.Message);
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            Support.clearField(this);
            mode = "";
        }
    }
}
