namespace GoTrot.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            panelTop = new Panel();
            lblWelcome = new Label();
            lblBalance = new Label();
            btnMojeVoznje = new Button();
            btnMojeUplate = new Button();
            btnRezervisi = new Button();
            btnOtkaziRez = new Button();
            btnUplata = new Button();
            panelContent = new Panel();
            lblTableTitle = new Label();
            dgvScooters = new DataGridView();
            panelBottom = new Panel();
            btnStartRide = new Button();
            btnEndRide = new Button();
            btnOdjava = new Button();
            btnIzlaz = new Button();
            lblStatus = new Label();
            lblTimer = new Label();
            rideTimer = new System.Windows.Forms.Timer(components);
            panelTop.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvScooters).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(26, 82, 118);
            panelTop.Controls.Add(lblWelcome);
            panelTop.Controls.Add(lblBalance);
            panelTop.Controls.Add(btnMojeVoznje);
            panelTop.Controls.Add(btnMojeUplate);
            panelTop.Controls.Add(btnRezervisi);
            panelTop.Controls.Add(btnOtkaziRez);
            panelTop.Controls.Add(btnUplata);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1110, 65);
            panelTop.TabIndex = 2;
            // 
            // lblWelcome
            // 
            lblWelcome.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(15, 8);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(320, 28);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Dobrodošli!";
            // 
            // lblBalance
            // 
            lblBalance.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lblBalance.ForeColor = Color.FromArgb(180, 230, 180);
            lblBalance.Location = new Point(15, 38);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(220, 22);
            lblBalance.TabIndex = 1;
            lblBalance.Text = "Kredit: -- KM";
            // 
            // btnMojeVoznje
            // 
            btnMojeVoznje.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMojeVoznje.BackColor = Color.FromArgb(52, 152, 219);
            btnMojeVoznje.Cursor = Cursors.Hand;
            btnMojeVoznje.FlatAppearance.BorderSize = 0;
            btnMojeVoznje.FlatStyle = FlatStyle.Flat;
            btnMojeVoznje.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnMojeVoznje.ForeColor = Color.White;
            btnMojeVoznje.Location = new Point(570, 10);
            btnMojeVoznje.Name = "btnMojeVoznje";
            btnMojeVoznje.Size = new Size(130, 45);
            btnMojeVoznje.TabIndex = 10;
            btnMojeVoznje.Text = "\U0001f6f4 Moje vožnje";
            btnMojeVoznje.UseVisualStyleBackColor = false;
            btnMojeVoznje.Click += BtnMojeVoznje_Click;
            // 
            // btnMojeUplate
            // 
            btnMojeUplate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMojeUplate.BackColor = Color.FromArgb(13, 158, 138);
            btnMojeUplate.Cursor = Cursors.Hand;
            btnMojeUplate.FlatAppearance.BorderSize = 0;
            btnMojeUplate.FlatStyle = FlatStyle.Flat;
            btnMojeUplate.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnMojeUplate.ForeColor = Color.White;
            btnMojeUplate.Location = new Point(708, 10);
            btnMojeUplate.Name = "btnMojeUplate";
            btnMojeUplate.Size = new Size(130, 45);
            btnMojeUplate.TabIndex = 11;
            btnMojeUplate.Text = "💳 Moje uplate";
            btnMojeUplate.UseVisualStyleBackColor = false;
            btnMojeUplate.Click += BtnMojeUplate_Click;
            // 
            // btnRezervisi
            // 
            btnRezervisi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRezervisi.BackColor = Color.FromArgb(155, 89, 182);
            btnRezervisi.Cursor = Cursors.Hand;
            btnRezervisi.FlatAppearance.BorderSize = 0;
            btnRezervisi.FlatStyle = FlatStyle.Flat;
            btnRezervisi.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnRezervisi.ForeColor = Color.White;
            btnRezervisi.Location = new Point(846, 10);
            btnRezervisi.Name = "btnRezervisi";
            btnRezervisi.Size = new Size(120, 45);
            btnRezervisi.TabIndex = 12;
            btnRezervisi.Text = "🔖 Rezerviši";
            btnRezervisi.UseVisualStyleBackColor = false;
            btnRezervisi.Click += BtnRezervisi_Click;
            // 
            // btnOtkaziRez
            // 
            btnOtkaziRez.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOtkaziRez.BackColor = Color.FromArgb(231, 76, 60);
            btnOtkaziRez.Cursor = Cursors.Hand;
            btnOtkaziRez.FlatAppearance.BorderSize = 0;
            btnOtkaziRez.FlatStyle = FlatStyle.Flat;
            btnOtkaziRez.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnOtkaziRez.ForeColor = Color.White;
            btnOtkaziRez.Location = new Point(846, 10);
            btnOtkaziRez.Name = "btnOtkaziRez";
            btnOtkaziRez.Size = new Size(125, 45);
            btnOtkaziRez.TabIndex = 13;
            btnOtkaziRez.Text = "❌ Otkaži rez.";
            btnOtkaziRez.UseVisualStyleBackColor = false;
            btnOtkaziRez.Visible = false;
            btnOtkaziRez.Click += BtnOtkaziRez_Click;
            // 
            // btnUplata
            // 
            btnUplata.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUplata.BackColor = Color.FromArgb(39, 174, 96);
            btnUplata.Cursor = Cursors.Hand;
            btnUplata.FlatAppearance.BorderSize = 0;
            btnUplata.FlatStyle = FlatStyle.Flat;
            btnUplata.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnUplata.ForeColor = Color.White;
            btnUplata.Location = new Point(974, 10);
            btnUplata.Name = "btnUplata";
            btnUplata.Size = new Size(135, 45);
            btnUplata.TabIndex = 14;
            btnUplata.Text = "➕ Uplati kredit";
            btnUplata.UseVisualStyleBackColor = false;
            btnUplata.Click += BtnUplata_Click;
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(245, 248, 250);
            panelContent.Controls.Add(lblTableTitle);
            panelContent.Controls.Add(dgvScooters);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 65);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(15);
            panelContent.Size = new Size(1110, 476);
            panelContent.TabIndex = 0;
            // 
            // lblTableTitle
            // 
            lblTableTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            lblTableTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTableTitle.Location = new Point(15, 11);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(250, 24);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "📋  Dostupni trotineti";
            // 
            // dgvScooters
            // 
            dgvScooters.AllowUserToAddRows = false;
            dgvScooters.AllowUserToDeleteRows = false;
            dgvScooters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvScooters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvScooters.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvScooters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvScooters.ColumnHeadersHeight = 35;
            dgvScooters.EnableHeadersVisualStyles = false;
            dgvScooters.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dgvScooters.GridColor = Color.FromArgb(220, 230, 240);
            dgvScooters.Location = new Point(15, 39);
            dgvScooters.MultiSelect = false;
            dgvScooters.Name = "dgvScooters";
            dgvScooters.ReadOnly = true;
            dgvScooters.RowHeadersVisible = false;
            dgvScooters.RowTemplate.Height = 30;
            dgvScooters.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvScooters.Size = new Size(1077, 431);
            dgvScooters.TabIndex = 1;
            // 
            // panelBottom
            // 
            panelBottom.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBottom.BackColor = Color.White;
            panelBottom.BorderStyle = BorderStyle.FixedSingle;
            panelBottom.Controls.Add(btnStartRide);
            panelBottom.Controls.Add(btnEndRide);
            panelBottom.Controls.Add(btnOdjava);
            panelBottom.Controls.Add(btnIzlaz);
            panelBottom.Controls.Add(lblStatus);
            panelBottom.Controls.Add(lblTimer);
            panelBottom.Location = new Point(10, 469);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1090, 66);
            panelBottom.TabIndex = 1;
            // 
            // btnStartRide
            // 
            btnStartRide.BackColor = Color.FromArgb(39, 174, 96);
            btnStartRide.Cursor = Cursors.Hand;
            btnStartRide.FlatAppearance.BorderSize = 0;
            btnStartRide.FlatStyle = FlatStyle.Flat;
            btnStartRide.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnStartRide.ForeColor = Color.White;
            btnStartRide.Location = new Point(14, 8);
            btnStartRide.Name = "btnStartRide";
            btnStartRide.Size = new Size(151, 45);
            btnStartRide.TabIndex = 0;
            btnStartRide.Text = "▶  Započni vožnju";
            btnStartRide.UseVisualStyleBackColor = false;
            btnStartRide.Click += BtnStartRide_Click;
            // 
            // btnEndRide
            // 
            btnEndRide.BackColor = Color.FromArgb(192, 57, 43);
            btnEndRide.Cursor = Cursors.Hand;
            btnEndRide.Enabled = false;
            btnEndRide.FlatAppearance.BorderSize = 0;
            btnEndRide.FlatStyle = FlatStyle.Flat;
            btnEndRide.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnEndRide.ForeColor = Color.White;
            btnEndRide.Location = new Point(171, 8);
            btnEndRide.Name = "btnEndRide";
            btnEndRide.Size = new Size(151, 45);
            btnEndRide.TabIndex = 1;
            btnEndRide.Text = "⏹  Završi vožnju";
            btnEndRide.UseVisualStyleBackColor = false;
            btnEndRide.Click += BtnEndRide_Click;
            // 
            // btnOdjava
            // 
            btnOdjava.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOdjava.BackColor = Color.FromArgb(52, 73, 94);
            btnOdjava.Cursor = Cursors.Hand;
            btnOdjava.FlatAppearance.BorderSize = 0;
            btnOdjava.FlatStyle = FlatStyle.Flat;
            btnOdjava.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnOdjava.ForeColor = Color.White;
            btnOdjava.Location = new Point(850, 8);
            btnOdjava.Name = "btnOdjava";
            btnOdjava.Size = new Size(120, 45);
            btnOdjava.TabIndex = 5;
            btnOdjava.Text = "🔓 Odjava";
            btnOdjava.UseVisualStyleBackColor = false;
            btnOdjava.Click += BtnOdjava_Click;
            // 
            // btnIzlaz
            // 
            btnIzlaz.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnIzlaz.BackColor = Color.FromArgb(120, 40, 31);
            btnIzlaz.Cursor = Cursors.Hand;
            btnIzlaz.FlatAppearance.BorderSize = 0;
            btnIzlaz.FlatStyle = FlatStyle.Flat;
            btnIzlaz.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnIzlaz.ForeColor = Color.White;
            btnIzlaz.Location = new Point(978, 8);
            btnIzlaz.Name = "btnIzlaz";
            btnIzlaz.Size = new Size(120, 45);
            btnIzlaz.TabIndex = 6;
            btnIzlaz.Text = "✖ Izlaz";
            btnIzlaz.UseVisualStyleBackColor = false;
            btnIzlaz.Click += BtnIzlaz_Click;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblStatus.ForeColor = Color.FromArgb(80, 80, 80);
            lblStatus.Location = new Point(340, 32);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(500, 20);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "\U0001f7e2  Spreman za vožnju. Odaberite trotinet iz liste.";
            // 
            // lblTimer
            // 
            lblTimer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTimer.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            lblTimer.ForeColor = Color.FromArgb(211, 84, 0);
            lblTimer.Location = new Point(340, 8);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(500, 22);
            lblTimer.TabIndex = 3;
            // 
            // rideTimer
            // 
            rideTimer.Interval = 1000;
            rideTimer.Tick += RideTimer_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1110, 541);
            Padding = new Padding(0, 0, 0, 76);
            Controls.Add(panelContent);
            Controls.Add(panelTop);
            Controls.Add(panelBottom);
            MinimumSize = new Size(900, 580);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "\U0001f6f4 GoTrot Sistem";
            panelTop.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvScooters).EndInit();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Button btnUplata;
        private System.Windows.Forms.Button btnMojeVoznje;
        private System.Windows.Forms.Button btnMojeUplate;
        private System.Windows.Forms.Button btnRezervisi;
        private System.Windows.Forms.Button btnOtkaziRez;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView dgvScooters;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnStartRide;
        private System.Windows.Forms.Button btnEndRide;
        private System.Windows.Forms.Button btnOdjava;
        private System.Windows.Forms.Button btnIzlaz;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Timer rideTimer;
    }
}