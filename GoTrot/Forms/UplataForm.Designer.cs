namespace GoTrot.Forms
{
    partial class UplataForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelTop = new Panel();
            lblTitle = new Label();
            lblTrenutniKredit = new Label();
            lblIznos = new Label();
            txtIznos = new TextBox();
            lblValuta = new Label();
            panelBrzaUplata = new Panel();
            lblBrzaUplata = new Label();
            btn5km = new Button();
            btn10km = new Button();
            btn20km = new Button();
            btn50km = new Button();
            btnUplati = new Button();
            btnOdustani = new Button();
            panelTop.SuspendLayout();
            panelBrzaUplata.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(39, 174, 96);
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(400, 65);
            panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(15, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(370, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "💳  Uplata kredita";
            // 
            // lblTrenutniKredit
            // 
            lblTrenutniKredit.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            lblTrenutniKredit.ForeColor = Color.FromArgb(39, 174, 96);
            lblTrenutniKredit.Location = new Point(30, 80);
            lblTrenutniKredit.Name = "lblTrenutniKredit";
            lblTrenutniKredit.Size = new Size(340, 24);
            lblTrenutniKredit.TabIndex = 1;
            lblTrenutniKredit.Text = "Trenutni kredit: -- KM";
            // 
            // lblIznos
            // 
            lblIznos.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblIznos.Location = new Point(30, 120);
            lblIznos.Name = "lblIznos";
            lblIznos.Size = new Size(200, 22);
            lblIznos.TabIndex = 2;
            lblIznos.Text = "Unesite iznos za uplatu:";
            // 
            // txtIznos
            // 
            txtIznos.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txtIznos.Location = new Point(30, 145);
            txtIznos.Name = "txtIznos";
            txtIznos.PlaceholderText = "0.00";
            txtIznos.Size = new Size(284, 32);
            txtIznos.TabIndex = 3;
            txtIznos.TextAlign = HorizontalAlignment.Right;
            txtIznos.KeyPress += TxtIznos_KeyPress;
            // 
            // lblValuta
            // 
            lblValuta.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblValuta.ForeColor = Color.FromArgb(80, 80, 80);
            lblValuta.Location = new Point(320, 148);
            lblValuta.Name = "lblValuta";
            lblValuta.Size = new Size(50, 28);
            lblValuta.TabIndex = 4;
            lblValuta.Text = "KM";
            // 
            // panelBrzaUplata
            // 
            panelBrzaUplata.BackColor = Color.FromArgb(245, 248, 250);
            panelBrzaUplata.BorderStyle = BorderStyle.FixedSingle;
            panelBrzaUplata.Controls.Add(lblBrzaUplata);
            panelBrzaUplata.Controls.Add(btn5km);
            panelBrzaUplata.Controls.Add(btn10km);
            panelBrzaUplata.Controls.Add(btn20km);
            panelBrzaUplata.Controls.Add(btn50km);
            panelBrzaUplata.Location = new Point(30, 195);
            panelBrzaUplata.Name = "panelBrzaUplata";
            panelBrzaUplata.Size = new Size(340, 86);
            panelBrzaUplata.TabIndex = 5;
            // 
            // lblBrzaUplata
            // 
            lblBrzaUplata.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblBrzaUplata.ForeColor = Color.Gray;
            lblBrzaUplata.Location = new Point(8, 6);
            lblBrzaUplata.Name = "lblBrzaUplata";
            lblBrzaUplata.Size = new Size(100, 18);
            lblBrzaUplata.TabIndex = 0;
            lblBrzaUplata.Text = "Brza uplata:";
            // 
            // btn5km
            // 
            btn5km.BackColor = Color.FromArgb(41, 128, 185);
            btn5km.Cursor = Cursors.Hand;
            btn5km.FlatAppearance.BorderSize = 0;
            btn5km.FlatStyle = FlatStyle.Flat;
            btn5km.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btn5km.ForeColor = Color.White;
            btn5km.Location = new Point(8, 32);
            btn5km.Name = "btn5km";
            btn5km.Size = new Size(70, 34);
            btn5km.TabIndex = 1;
            btn5km.Text = "5 KM";
            btn5km.UseVisualStyleBackColor = false;
            btn5km.Click += Btn5km_Click;
            // 
            // btn10km
            // 
            btn10km.BackColor = Color.FromArgb(41, 128, 185);
            btn10km.Cursor = Cursors.Hand;
            btn10km.FlatAppearance.BorderSize = 0;
            btn10km.FlatStyle = FlatStyle.Flat;
            btn10km.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btn10km.ForeColor = Color.White;
            btn10km.Location = new Point(88, 32);
            btn10km.Name = "btn10km";
            btn10km.Size = new Size(70, 34);
            btn10km.TabIndex = 2;
            btn10km.Text = "10 KM";
            btn10km.UseVisualStyleBackColor = false;
            btn10km.Click += Btn10km_Click;
            // 
            // btn20km
            // 
            btn20km.BackColor = Color.FromArgb(41, 128, 185);
            btn20km.Cursor = Cursors.Hand;
            btn20km.FlatAppearance.BorderSize = 0;
            btn20km.FlatStyle = FlatStyle.Flat;
            btn20km.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btn20km.ForeColor = Color.White;
            btn20km.Location = new Point(168, 32);
            btn20km.Name = "btn20km";
            btn20km.Size = new Size(70, 34);
            btn20km.TabIndex = 3;
            btn20km.Text = "20 KM";
            btn20km.UseVisualStyleBackColor = false;
            btn20km.Click += Btn20km_Click;
            // 
            // btn50km
            // 
            btn50km.BackColor = Color.FromArgb(41, 128, 185);
            btn50km.Cursor = Cursors.Hand;
            btn50km.FlatAppearance.BorderSize = 0;
            btn50km.FlatStyle = FlatStyle.Flat;
            btn50km.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btn50km.ForeColor = Color.White;
            btn50km.Location = new Point(248, 32);
            btn50km.Name = "btn50km";
            btn50km.Size = new Size(70, 34);
            btn50km.TabIndex = 4;
            btn50km.Text = "50 KM";
            btn50km.UseVisualStyleBackColor = false;
            btn50km.Click += Btn50km_Click;
            // 
            // btnUplati
            // 
            btnUplati.BackColor = Color.FromArgb(39, 174, 96);
            btnUplati.Cursor = Cursors.Hand;
            btnUplati.FlatAppearance.BorderSize = 0;
            btnUplati.FlatStyle = FlatStyle.Flat;
            btnUplati.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnUplati.ForeColor = Color.White;
            btnUplati.Location = new Point(30, 306);
            btnUplati.Name = "btnUplati";
            btnUplati.Size = new Size(160, 42);
            btnUplati.TabIndex = 6;
            btnUplati.Text = "💳  Uplati";
            btnUplati.UseVisualStyleBackColor = false;
            btnUplati.Click += BtnUplati_Click;
            // 
            // btnOdustani
            // 
            btnOdustani.BackColor = Color.FromArgb(220, 220, 220);
            btnOdustani.Cursor = Cursors.Hand;
            btnOdustani.FlatAppearance.BorderSize = 0;
            btnOdustani.FlatStyle = FlatStyle.Flat;
            btnOdustani.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnOdustani.Location = new Point(210, 306);
            btnOdustani.Name = "btnOdustani";
            btnOdustani.Size = new Size(160, 42);
            btnOdustani.TabIndex = 7;
            btnOdustani.Text = "Odustani";
            btnOdustani.UseVisualStyleBackColor = false;
            btnOdustani.Click += BtnOdustani_Click;
            // 
            // UplataForm
            // 
            AcceptButton = btnUplati;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(400, 360);
            Controls.Add(panelTop);
            Controls.Add(lblTrenutniKredit);
            Controls.Add(lblIznos);
            Controls.Add(txtIznos);
            Controls.Add(lblValuta);
            Controls.Add(panelBrzaUplata);
            Controls.Add(btnUplati);
            Controls.Add(btnOdustani);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UplataForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "\U0001f6f4 GoTrot Uplata kredita";
            panelTop.ResumeLayout(false);
            panelBrzaUplata.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTrenutniKredit;
        private System.Windows.Forms.Label lblIznos;
        private System.Windows.Forms.TextBox txtIznos;
        private System.Windows.Forms.Label lblValuta;
        private System.Windows.Forms.Panel panelBrzaUplata;
        private System.Windows.Forms.Label lblBrzaUplata;
        private System.Windows.Forms.Button btn5km;
        private System.Windows.Forms.Button btn10km;
        private System.Windows.Forms.Button btn20km;
        private System.Windows.Forms.Button btn50km;
        private System.Windows.Forms.Button btnUplati;
        private System.Windows.Forms.Button btnOdustani;
    }
}
