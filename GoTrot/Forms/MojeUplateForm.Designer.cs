namespace GoTrot.Forms
{
    partial class MojeUplateForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            panelTop = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            panelContent = new System.Windows.Forms.Panel();
            dgvUplate = new System.Windows.Forms.DataGridView();
            panelBottom = new System.Windows.Forms.Panel();
            lblStats = new System.Windows.Forms.Label();
            btnZatvori = new System.Windows.Forms.Button();
            panelTop.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUplate).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();

            // panelTop
            panelTop.BackColor = System.Drawing.Color.FromArgb(13, 158, 138);
            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(lblSubtitle);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Location = new System.Drawing.Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new System.Drawing.Size(900, 70);
            panelTop.TabIndex = 0;

            // lblTitle
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location = new System.Drawing.Point(15, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(500, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "💳  Moje uplate";

            // lblSubtitle
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(200, 255, 245);
            lblSubtitle.Location = new System.Drawing.Point(15, 42);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new System.Drawing.Size(500, 20);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Historija vaših uplata kredita";

            // panelContent
            panelContent.BackColor = System.Drawing.Color.FromArgb(245, 248, 250);
            panelContent.Controls.Add(dgvUplate);
            panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContent.Location = new System.Drawing.Point(0, 70);
            panelContent.Name = "panelContent";
            panelContent.Padding = new System.Windows.Forms.Padding(15);
            panelContent.Size = new System.Drawing.Size(900, 462);
            panelContent.TabIndex = 1;

            // dgvUplate
            dgvUplate.AllowUserToAddRows = false;
            dgvUplate.AllowUserToDeleteRows = false;
            dgvUplate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom |
                               System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvUplate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvUplate.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(13, 158, 138);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dgvUplate.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvUplate.ColumnHeadersHeight = 38;
            dgvUplate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dgvUplate.GridColor = System.Drawing.Color.FromArgb(220, 230, 240);
            dgvUplate.Location = new System.Drawing.Point(12, 18);
            dgvUplate.MultiSelect = false;
            dgvUplate.Name = "dgvUplate";
            dgvUplate.ReadOnly = true;
            dgvUplate.RowHeadersVisible = false;
            dgvUplate.RowTemplate.Height = 32;
            dgvUplate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvUplate.Size = new System.Drawing.Size(870, 426);
            dgvUplate.TabIndex = 0;

            // panelBottom
            panelBottom.BackColor = System.Drawing.Color.White;
            panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelBottom.Controls.Add(lblStats);
            panelBottom.Controls.Add(btnZatvori);
            panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelBottom.Location = new System.Drawing.Point(0, 532);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new System.Drawing.Size(900, 68);
            panelBottom.TabIndex = 2;

            // lblStats
            lblStats.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblStats.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            lblStats.Location = new System.Drawing.Point(15, 15);
            lblStats.Name = "lblStats";
            lblStats.Size = new System.Drawing.Size(700, 22);
            lblStats.TabIndex = 0;

            // btnZatvori
            btnZatvori.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            btnZatvori.Cursor = System.Windows.Forms.Cursors.Hand;
            btnZatvori.FlatAppearance.BorderSize = 0;
            btnZatvori.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnZatvori.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnZatvori.ForeColor = System.Drawing.Color.White;
            btnZatvori.Location = new System.Drawing.Point(763, 15);
            btnZatvori.Name = "btnZatvori";
            btnZatvori.Size = new System.Drawing.Size(118, 40);
            btnZatvori.TabIndex = 1;
            btnZatvori.Text = "✖  Zatvori";
            btnZatvori.UseVisualStyleBackColor = false;
            btnZatvori.Click += BtnZatvori_Click;
            btnZatvori.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;

            // MojeUplateForm
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(900, 600);
            Controls.Add(panelContent);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            MinimumSize = new System.Drawing.Size(800, 500);
            Name = "MojeUplateForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "GoTrot – Moje uplate";
            panelTop.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUplate).EndInit();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.DataGridView dgvUplate;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Button btnZatvori;
    }
}
