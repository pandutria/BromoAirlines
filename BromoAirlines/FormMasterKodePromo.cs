using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BromoAirlines
{
    public partial class FormMasterKodePromo : Form
    {
        private DataBaseDataContext db = new DataBaseDataContext();
        int currentSelectedRow = -1;
        string mode = "";

        public FormMasterKodePromo()
        {
            InitializeComponent();
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

            var query = db.KodePromos.Select(x => new
            {
                x.Kode,
                x.PersentaseDiskon,
                x.MaksimumDiskon,
                BerlakuSampai = Convert.ToDateTime(x.BerlakuSampai).ToLocalTime().ToString("dd-MM-yyyy"),
                x.Deskripsi,
                x.ID
            });


            dgvData.DataSource = query;
            dgvData.Columns["ID"].Visible = false;

            dgvData.Columns.Add(btnUbah);
            dgvData.Columns.Add(btnHapus);

        }

        private bool validation()
        {
            if (tbKodePromo.Text == string.Empty || tbDesc.Text == string.Empty)
            {
                Support.msw("all field must be filled");
                return false;
            }

            if (nupPresentaseDiskon.Value < 1)
            {
                Support.msw("Persentase diskon minimal satu (1).");
                return false;
            }

            if (nupMaximumDiskon.Value < 1)
            {
                Support.msw("Maksimum diskon minimal satu (1).");
                return false;
            }

            if (tbKodePromo.Text != tbKodePromo.Text.ToString().ToUpper())
            {
                Support.msw("kode harus besar");
                return false;
            }

            return true;
        }


        private void FormMasterKodePromo_Load(object sender, EventArgs e)
        {
            loadDgvData();

            nupMaximumDiskon.Minimum = 0;
            nupMaximumDiskon.Maximum = 9999999;

            nupPresentaseDiskon.Minimum = 0;
            nupPresentaseDiskon.Maximum = 9999999;
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                currentSelectedRow = e.RowIndex;
                currentSelectedRow = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["ID"].Value);
                tbKodePromo.Text = dgvData.Rows[e.RowIndex].Cells["Kode"].Value.ToString();
                tbDesc.Text = dgvData.Rows[e.RowIndex].Cells["Deskripsi"].Value.ToString();
                nupPresentaseDiskon.Value = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["PersentaseDiskon"].Value);
                nupMaximumDiskon.Value = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["MaksimumDiskon"].Value);
                dtpBerlaku.Value = Convert.ToDateTime(dgvData.Rows[e.RowIndex].Cells["BerlakuSampai"].Value);

                dtpBerlaku.CustomFormat = "dd-MM-yyyy";
                dtpBerlaku.Format = DateTimePickerFormat.Custom;
                 
            }

            if (e.ColumnIndex == dgvData.Columns["btnHapus"].Index && e.RowIndex >= 0)
            {
                var msgDialog = MessageBox.Show("are sure want to delete this data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgDialog == DialogResult.Yes)
                {
                    var queryDelete = db.KodePromos.FirstOrDefault(x => x.ID == currentSelectedRow);
                    if (queryDelete != null)
                    {
                        db.KodePromos.DeleteOnSubmit(queryDelete);
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
                        var queryUpdate = db.KodePromos.FirstOrDefault(x => x.ID == currentSelectedRow);

                        if (queryUpdate != null)
                        {
                            queryUpdate.Kode = tbKodePromo.Text;
                            queryUpdate.PersentaseDiskon = Convert.ToInt32( nupPresentaseDiskon.Value);
                            queryUpdate.MaksimumDiskon = Convert.ToInt32(nupMaximumDiskon.Value);
                            queryUpdate.Deskripsi = tbDesc.Text;
                            queryUpdate.BerlakuSampai = dtpBerlaku.Value;


                            db.SubmitChanges();
                            Support.msi("ubah data berhasi");
                            loadDgvData();
                            Support.clearField(this);
                            mode = "";
                        }
                    }
                    
                    else
                    {
                        var validationKode = db.KodePromos.FirstOrDefault(x => x.Kode == tbKodePromo.Text);

                        if (validationKode != null)
                        {
                            Support.msw("Kode promo yang diinput harus unik dan tidak boleh duplikat dengan data yang sudah ada\r\nsebelumnya.");
                        } else
                        {

                            var query = new KodePromo();
                            query.Kode = tbKodePromo.Text;
                            query.PersentaseDiskon = Convert.ToInt32(nupPresentaseDiskon.Value);
                            query.MaksimumDiskon = Convert.ToInt32(nupMaximumDiskon.Value);
                            query.Deskripsi = tbDesc.Text;
                            query.BerlakuSampai = dtpBerlaku.Value;
                            db.KodePromos.InsertOnSubmit(query);
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

        private void tbKodePromo_TextChanged(object sender, EventArgs e)
        {

            //tbKodePromo.Text = tbKodePromo.Text.ToUpper();

        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            Support.clearField(this);
            mode = "";
        }
    }
}
