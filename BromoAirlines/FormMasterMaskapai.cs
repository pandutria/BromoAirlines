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
    public partial class FormMasterMaskapai : Form
    {
        private DataBaseDataContext db = new DataBaseDataContext();
        int currentSelectedRow = -1;
        string mode = "";
        public FormMasterMaskapai()
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

            var query = db.Maskapais.OrderBy(x => x.Nama).Select(x => new
            {
                x.Nama,
                x.Perusahaan,
                x.JumlahKru,
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
            if (tbName.Text == string.Empty || tbDesc.Text == string.Empty
                || tbPerusahaan.Text == string.Empty)
            {
                Support.msw("all field must be filled");
                return false;
            }

            if (nupJumlahKru.Value < 1)
            {
                Support.msw("Jumlah terminal yang diinput minimal satu (1).");
                return false;
            }

            return true;
        }

        private void FormMasterMaskapai_Load(object sender, EventArgs e)
        {
            loadDgvData();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                currentSelectedRow = e.RowIndex;
                currentSelectedRow = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["ID"].Value);
                tbName.Text = dgvData.Rows[e.RowIndex].Cells["Nama"].Value.ToString();
                tbDesc.Text = dgvData.Rows[e.RowIndex].Cells["Deskripsi"].Value.ToString();
                tbPerusahaan.Text = dgvData.Rows[e.RowIndex].Cells["Perusahaan"].Value.ToString();
                nupJumlahKru.Value = Convert.ToInt32( dgvData.Rows[e.RowIndex].Cells["JumlahKru"].Value);

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
                        var queryUpdate = db.Maskapais.FirstOrDefault(x => x.ID == currentSelectedRow);

                        if (queryUpdate != null)
                        {
                            queryUpdate.Nama = tbName.Text;
                            queryUpdate.Perusahaan = tbPerusahaan.Text;
                            queryUpdate.JumlahKru = Convert.ToInt32(nupJumlahKru.Value);
                            queryUpdate.Deskripsi = tbDesc.Text;


                            db.SubmitChanges();
                            Support.msi("ubah data berhasi");
                            loadDgvData();
                            Support.clearField(this);
                            mode = "";
                        }
                    }

                    else
                    {

                        var query = new Maskapai();
                        query.Nama = tbName.Text;
                        query.Perusahaan = tbPerusahaan.Text;
                        query.Deskripsi = tbDesc.Text;
                        query.JumlahKru = Convert.ToInt32(nupJumlahKru.Value);
                        db.Maskapais.InsertOnSubmit(query);
                        db.SubmitChanges();
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

        private void btnBatal_Click(object sender, EventArgs e)
        {
            Support.clearField(this);
            mode = "";
        }
    }
}
