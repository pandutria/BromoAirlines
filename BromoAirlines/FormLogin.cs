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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            tbPassword.Text = "admin123";
            tbUsername.Text = "admin";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var db = new DataBaseDataContext();

            var query = db.Akuns.FirstOrDefault(x => x.Username == tbUsername.Text && x.Password == tbPassword.Text);
            if (query != null)
            {
                if (query.MerupakanAdmin == true)
                {
                    Support.akunId = query.ID;
                    new FormMainCustomer(query.Nama).Show();
                    Hide();
                } else
                {
                    Support.akunId = query.ID;
                    new FormMainCustomer(query.Nama).Show();
                    Hide();
                }

            }
            else Support.msw("username/password is wrong");
        }

        private void linkDaftar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new FormDaftarAkun().ShowDialog();
            Hide();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            
        }
    }
}
