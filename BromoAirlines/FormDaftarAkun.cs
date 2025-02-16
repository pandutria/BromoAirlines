using System;
using System.Collections;
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
    public partial class FormDaftarAkun : Form
    {
        public FormDaftarAkun()
        {
            InitializeComponent();
        }

        private void FormDaftarAkun_Load(object sender, EventArgs e)
        {
            dtpTanggalLahir.CustomFormat = "dd-MM-yyyy";
            dtpTanggalLahir.Format = DateTimePickerFormat.Custom;
        }

        private bool validation()
        {
            if (tbUsername.Text == string.Empty|| tbPassword.Text == string.Empty || 
                tbNomorPassword.Text == string.Empty || tbPassword.Text == string.Empty || dtpTanggalLahir.Value == DateTime.Now)
            {
                Support.msw("all field must be filled");
                return false;
            }
            
            if (tbPassword.Text.Length < 8)
            {
                Support.msi("Password akun minimal harus berjumlah 8 karakter.");
                return false;
            }

            if (tbNomorPassword.Text.Length < 10 && tbNomorPassword.Text.Length > 15)
            {
                Support.msw("Nomor telepon yang dimasukkan harus berupa angka (berjumlah 10-15 digit).");
                return false ;
            }

            return true;
        }


        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new FormLogin().Show();
            Hide();
        }

        private void linkLogin_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new FormLogin().Show();
            Hide();
        }

        private void btnDaftar_Click_1(object sender, EventArgs e)
        {
            var db = new DataBaseDataContext();

            if (validation())
            {
                try
                {
                    var queryValidationUsername = db.Akuns.FirstOrDefault(x => x.Username == tbUsername.Text);

                    if (queryValidationUsername != null)
                    {
                        Support.mse("Username yang dimasukkan oleh user harus unik dan tidak boleh duplikat.");
                    }
                    else
                    {
                        var newQuery = new Akun();
                        newQuery.Username = tbUsername.Text;
                        newQuery.Nama = tbNama.Text;
                        newQuery.Password = tbPassword.Text;
                        newQuery.TanggalLahir = dtpTanggalLahir.Value;
                        newQuery.NomorTelepon = tbNomorPassword.Text;
                        newQuery.MerupakanAdmin = false;

                        db.Akuns.InsertOnSubmit(newQuery);
                        db.SubmitChanges();

                        Support.akunId = newQuery.ID;
                        new FormMainCustomer(tbNama.Text).Show();
                        Hide();
                        Support.msi("berhasil");

                    }

                }
                catch (Exception ex)
                {
                    Support.mse(ex.Message);
                }
            }

        }
    }
}
