namespace BromoAirlines
{
    partial class FormMasterKodePromo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbDesc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.nupPresentaseDiskon = new System.Windows.Forms.NumericUpDown();
            this.tbKodePromo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.dtpBerlaku = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nupMaximumDiskon = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nupPresentaseDiskon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMaximumDiskon)).BeginInit();
            this.SuspendLayout();
            // 
            // tbDesc
            // 
            this.tbDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDesc.Location = new System.Drawing.Point(495, 294);
            this.tbDesc.Multiline = true;
            this.tbDesc.Name = "tbDesc";
            this.tbDesc.Size = new System.Drawing.Size(187, 106);
            this.tbDesc.TabIndex = 59;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label8.Location = new System.Drawing.Point(406, 294);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 20);
            this.label8.TabIndex = 58;
            this.label8.Text = "Deskripsi";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(573, 410);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 34);
            this.btnSave.TabIndex = 57;
            this.btnSave.Text = "Simpan";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBatal
            // 
            this.btnBatal.Location = new System.Drawing.Point(451, 410);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(109, 34);
            this.btnBatal.TabIndex = 56;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = true;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label7.Location = new System.Drawing.Point(31, 377);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 20);
            this.label7.TabIndex = 55;
            this.label7.Text = "Presentase Diskon";
            // 
            // nupPresentaseDiskon
            // 
            this.nupPresentaseDiskon.Location = new System.Drawing.Point(198, 378);
            this.nupPresentaseDiskon.Name = "nupPresentaseDiskon";
            this.nupPresentaseDiskon.Size = new System.Drawing.Size(187, 22);
            this.nupPresentaseDiskon.TabIndex = 54;
            // 
            // tbKodePromo
            // 
            this.tbKodePromo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbKodePromo.Location = new System.Drawing.Point(198, 298);
            this.tbKodePromo.Name = "tbKodePromo";
            this.tbKodePromo.Size = new System.Drawing.Size(187, 22);
            this.tbKodePromo.TabIndex = 51;
            this.tbKodePromo.TextChanged += new System.EventHandler(this.tbKodePromo_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label1.Location = new System.Drawing.Point(32, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 50;
            this.label1.Text = "Kode Promo";
            // 
            // dgvData
            // 
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(37, 113);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(669, 167);
            this.dgvData.TabIndex = 49;
            this.dgvData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellClick);
            // 
            // dtpBerlaku
            // 
            this.dtpBerlaku.Location = new System.Drawing.Point(198, 339);
            this.dtpBerlaku.Name = "dtpBerlaku";
            this.dtpBerlaku.Size = new System.Drawing.Size(183, 22);
            this.dtpBerlaku.TabIndex = 60;
            this.dtpBerlaku.Value = new System.DateTime(2025, 2, 9, 9, 26, 22, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label2.Location = new System.Drawing.Point(32, 338);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 20);
            this.label2.TabIndex = 52;
            this.label2.Text = "Berlaku Sampai";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label5.Location = new System.Drawing.Point(31, 416);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 20);
            this.label5.TabIndex = 62;
            this.label5.Text = "Maximum Diskon";
            // 
            // nupMaximumDiskon
            // 
            this.nupMaximumDiskon.Location = new System.Drawing.Point(198, 417);
            this.nupMaximumDiskon.Name = "nupMaximumDiskon";
            this.nupMaximumDiskon.Size = new System.Drawing.Size(187, 22);
            this.nupMaximumDiskon.TabIndex = 61;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label6.Location = new System.Drawing.Point(33, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(387, 23);
            this.label6.TabIndex = 64;
            this.label6.Text = "Semua kode promo yang terdaftar muncul di sini";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(29, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(279, 38);
            this.label9.TabIndex = 63;
            this.label9.Text = "Master Kode Promo";
            // 
            // FormMasterKodePromo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(756, 505);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nupMaximumDiskon);
            this.Controls.Add(this.dtpBerlaku);
            this.Controls.Add(this.tbDesc);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnBatal);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nupPresentaseDiskon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbKodePromo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvData);
            this.Name = "FormMasterKodePromo";
            this.Text = "FormMasterKodePromo";
            this.Load += new System.EventHandler(this.FormMasterKodePromo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nupPresentaseDiskon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMaximumDiskon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDesc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nupPresentaseDiskon;
        private System.Windows.Forms.TextBox tbKodePromo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DateTimePicker dtpBerlaku;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nupMaximumDiskon;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
    }
}