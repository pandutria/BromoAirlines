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
    public partial class FormMainAdmin : Form
    {
        public FormMainAdmin()
        {
            InitializeComponent();
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            new FormLogin().Show();
            Hide();
        }

        private void FormMainAdmin_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panelBandara_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panelBandara_MouseClick(object sender, MouseEventArgs e)
        {

            panelContainer.Controls.Clear();
            var f = new FormMasterBandara();

            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;

            panelContainer.Controls.Add(f);
            f.Show();
        }

        private void panelMaskapai_MouseClick(object sender, MouseEventArgs e)
        {
            panelContainer.Controls.Clear();
            var f = new FormMasterMaskapai();

            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;

            panelContainer.Controls.Add(f);
            f.Show();
        }

        private void panelJadwalPenerbangan_MouseClick(object sender, MouseEventArgs e)
        {
            panelContainer.Controls.Clear();
            var f = new FormMasterJadwalPenerbangan();

            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;

            panelContainer.Controls.Add(f);
            f.Show();
        }

        private void panelKodePromo_MouseClick(object sender, MouseEventArgs e)
        {
            panelContainer.Controls.Clear();
            var f = new FormMasterKodePromo();

            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;

            panelContainer.Controls.Add(f);
            f.Show();
        }

        private void panel8_MouseClick(object sender, MouseEventArgs e)
        {
            panelContainer.Controls.Clear();
            var f = new FormUbahStatusPenerbangan();

            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;

            panelContainer.Controls.Add(f);
            f.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            new FormLogin().Show();
            Hide();
        }
    }
}
