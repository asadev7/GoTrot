namespace GoTrot.Forms
{
    partial class MojeVoznjeForm
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
            panelTop = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            panelContent = new Panel();
            dgvVoznje = new DataGridView();
            panelBottom = new Panel();
            lblStats = new Label();
            btnZatvori = new Button();
            panelTop.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVoznje).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(26, 82, 118);
            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(lblSubtitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(900, 70);
            panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(15, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📋  Moje vožnje";
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblSubtitle.ForeColor = Color.FromArgb(180, 220, 255);
            lblSubtitle.Location = new Point(15, 42);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(400, 20);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Historija vaših prethodnih vožnji";
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(245, 248, 250);
            panelContent.Controls.Add(dgvVoznje);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 70);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(15);
            panelContent.Size = new Size(900, 462);
            panelContent.TabIndex = 1;
            // 
            // dgvVoznje
            // 
            dgvVoznje.AllowUserToAddRows = false;
            dgvVoznje.AllowUserToDeleteRows = false;
            dgvVoznje.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvVoznje.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVoznje.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(41, 128, 185);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dgvVoznje.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvVoznje.ColumnHeadersHeight = 38;
            dgvVoznje.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dgvVoznje.GridColor = Color.FromArgb(220, 230, 240);
            dgvVoznje.Location = new Point(12, 18);
            dgvVoznje.MultiSelect = false;
            dgvVoznje.Name = "dgvVoznje";
            dgvVoznje.ReadOnly = true;
            dgvVoznje.RowHeadersVisible = false;
            dgvVoznje.RowTemplate.Height = 32;
            dgvVoznje.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVoznje.Size = new Size(870, 426);
            dgvVoznje.TabIndex = 0;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.White;
            panelBottom.BorderStyle = BorderStyle.FixedSingle;
            panelBottom.Controls.Add(lblStats);
            panelBottom.Controls.Add(btnZatvori);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 532);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(900, 68);
            panelBottom.TabIndex = 2;
            // 
            // lblStats
            // 
            lblStats.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblStats.ForeColor = Color.FromArgb(44, 62, 80);
            lblStats.Location = new Point(15, 23);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(600, 22);
            lblStats.TabIndex = 0;
            // 
            // btnZatvori
            // 
            btnZatvori.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnZatvori.BackColor = Color.FromArgb(52, 73, 94);
            btnZatvori.Cursor = Cursors.Hand;
            btnZatvori.FlatAppearance.BorderSize = 0;
            btnZatvori.FlatStyle = FlatStyle.Flat;
            btnZatvori.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnZatvori.ForeColor = Color.White;
            btnZatvori.Location = new Point(763, 15);
            btnZatvori.Name = "btnZatvori";
            btnZatvori.Size = new Size(118, 40);
            btnZatvori.TabIndex = 1;
            btnZatvori.Text = "✖  Zatvori";
            btnZatvori.UseVisualStyleBackColor = false;
            btnZatvori.Click += BtnZatvori_Click;
            // 
            // MojeVoznjeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(900, 600);
            Controls.Add(panelContent);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            MinimumSize = new Size(800, 500);
            Name = "MojeVoznjeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GoTrot – Moje vožnje";
            panelTop.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVoznje).EndInit();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.DataGridView dgvVoznje;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Button btnZatvori;
    }
}
