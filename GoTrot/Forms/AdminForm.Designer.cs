namespace GoTrot.Forms
{
    partial class AdminForm
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panelTop = new Panel();
            lblTitle = new Label();
            btnAdminOdjava = new Button();
            btnAdminZatvori = new Button();
            tabControl = new TabControl();
            tabScooters = new TabPage();
            label1 = new Label();
            dgvManageScooters = new DataGridView();
            panelAddScooter = new Panel();
            lblModel = new Label();
            txtModel = new TextBox();
            lblBattery = new Label();
            numBattery = new NumericUpDown();
            lblLocation = new Label();
            txtLocation = new TextBox();
            btnAddScooter = new Button();
            btnDeleteScooter = new Button();
            btnPostavljenaNaPunjenje = new Button();
            tabHistory = new TabPage();
            lblHistoryTitle = new Label();
            dgvHistory = new DataGridView();
            tabNotifications = new TabPage();
            lblNotifTitle = new Label();
            dgvNotifications = new DataGridView();
            btnOznačiProcitano = new Button();
            btnObrišiNotifikacije = new Button();
            panelTop.SuspendLayout();
            tabControl.SuspendLayout();
            tabScooters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvManageScooters).BeginInit();
            panelAddScooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numBattery).BeginInit();
            tabHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            tabNotifications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNotifications).BeginInit();
            SuspendLayout();
            // panelTop
            panelTop.BackColor = Color.FromArgb(44, 62, 80);
            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(btnAdminOdjava);
            panelTop.Controls.Add(btnAdminZatvori);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(924, 55);
            panelTop.TabIndex = 1;
            // lblTitle
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(15, 13);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(600, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "⚙  Admin Panel – GoTrot Sistem";
            // btnAdminOdjava
            btnAdminOdjava.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdminOdjava.BackColor = Color.FromArgb(52, 73, 94);
            btnAdminOdjava.Cursor = Cursors.Hand;
            btnAdminOdjava.FlatAppearance.BorderSize = 0;
            btnAdminOdjava.FlatStyle = FlatStyle.Flat;
            btnAdminOdjava.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnAdminOdjava.ForeColor = Color.White;
            btnAdminOdjava.Location = new Point(650, 8);
            btnAdminOdjava.Name = "btnAdminOdjava";
            btnAdminOdjava.Size = new Size(130, 38);
            btnAdminOdjava.TabIndex = 1;
            btnAdminOdjava.Text = "🔓 Odjava";
            btnAdminOdjava.UseVisualStyleBackColor = false;
            btnAdminOdjava.Click += BtnAdminOdjava_Click;
            // btnAdminZatvori
            btnAdminZatvori.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdminZatvori.BackColor = Color.FromArgb(120, 40, 31);
            btnAdminZatvori.Cursor = Cursors.Hand;
            btnAdminZatvori.FlatAppearance.BorderSize = 0;
            btnAdminZatvori.FlatStyle = FlatStyle.Flat;
            btnAdminZatvori.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnAdminZatvori.ForeColor = Color.White;
            btnAdminZatvori.Location = new Point(790, 8);
            btnAdminZatvori.Name = "btnAdminZatvori";
            btnAdminZatvori.Size = new Size(120, 38);
            btnAdminZatvori.TabIndex = 2;
            btnAdminZatvori.Text = "✖ Zatvori";
            btnAdminZatvori.UseVisualStyleBackColor = false;
            btnAdminZatvori.Click += BtnAdminZatvori_Click;
            // tabControl
            tabControl.Controls.Add(tabScooters);
            tabControl.Controls.Add(tabHistory);
            tabControl.Controls.Add(tabNotifications);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            tabControl.Location = new Point(0, 55);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(924, 566);
            tabControl.TabIndex = 0;
            // tabScooters
            tabScooters.BackColor = Color.FromArgb(245, 248, 250);
            tabScooters.Controls.Add(label1);
            tabScooters.Controls.Add(dgvManageScooters);
            tabScooters.Controls.Add(panelAddScooter);
            tabScooters.Location = new Point(4, 26);
            tabScooters.Name = "tabScooters";
            tabScooters.Size = new Size(916, 536);
            tabScooters.TabIndex = 0;
            tabScooters.Text = "\U0001f6f4  Upravljanje trotinetima";
            // label1
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(10, 11);
            label1.Name = "label1";
            label1.Size = new Size(300, 22);
            label1.TabIndex = 2;
            label1.Text = "Dostupni električni trotineti na stanju:";
            label1.Click += label1_Click;
            // dgvManageScooters
            dgvManageScooters.AllowUserToAddRows = false;
            dgvManageScooters.AllowUserToDeleteRows = false;
            dgvManageScooters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvManageScooters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvManageScooters.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvManageScooters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvManageScooters.ColumnHeadersHeight = 35;
            dgvManageScooters.EnableHeadersVisualStyles = false;
            dgvManageScooters.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 245, 255);
            dgvManageScooters.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dgvManageScooters.Location = new Point(11, 36);
            dgvManageScooters.MultiSelect = false;
            dgvManageScooters.Name = "dgvManageScooters";
            dgvManageScooters.ReadOnly = true;
            dgvManageScooters.RowHeadersVisible = false;
            dgvManageScooters.RowTemplate.Height = 30;
            dgvManageScooters.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvManageScooters.Size = new Size(897, 405);
            dgvManageScooters.TabIndex = 0;
            dgvManageScooters.SelectionChanged += DgvManageScooters_SelectionChanged;
            // panelAddScooter
            panelAddScooter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelAddScooter.BackColor = Color.White;
            panelAddScooter.BorderStyle = BorderStyle.FixedSingle;
            panelAddScooter.Controls.Add(lblModel);
            panelAddScooter.Controls.Add(txtModel);
            panelAddScooter.Controls.Add(lblBattery);
            panelAddScooter.Controls.Add(numBattery);
            panelAddScooter.Controls.Add(lblLocation);
            panelAddScooter.Controls.Add(txtLocation);
            panelAddScooter.Controls.Add(btnAddScooter);
            panelAddScooter.Controls.Add(btnDeleteScooter);
            panelAddScooter.Controls.Add(btnPostavljenaNaPunjenje);
            panelAddScooter.Location = new Point(10, 447);
            panelAddScooter.Name = "panelAddScooter";
            panelAddScooter.Size = new Size(898, 80);
            panelAddScooter.TabIndex = 1;
            // lblModel
            lblModel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblModel.Location = new Point(10, 10);
            lblModel.Name = "lblModel";
            lblModel.Size = new Size(50, 18);
            lblModel.TabIndex = 0;
            lblModel.Text = "Model:";
            // txtModel
            txtModel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtModel.Location = new Point(10, 30);
            txtModel.Name = "txtModel";
            txtModel.PlaceholderText = "npr. Xiaomi Pro";
            txtModel.Size = new Size(150, 25);
            txtModel.TabIndex = 1;
            // lblBattery
            lblBattery.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblBattery.Location = new Point(175, 10);
            lblBattery.Name = "lblBattery";
            lblBattery.Size = new Size(80, 18);
            lblBattery.TabIndex = 2;
            lblBattery.Text = "Baterija (%):";
            // numBattery
            numBattery.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            numBattery.Location = new Point(175, 30);
            numBattery.Name = "numBattery";
            numBattery.Size = new Size(70, 25);
            numBattery.TabIndex = 3;
            numBattery.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // lblLocation
            lblLocation.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblLocation.Location = new Point(260, 10);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(70, 18);
            lblLocation.TabIndex = 4;
            lblLocation.Text = "Lokacija:";
            // txtLocation
            txtLocation.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtLocation.Location = new Point(260, 30);
            txtLocation.Name = "txtLocation";
            txtLocation.PlaceholderText = "npr. Centar";
            txtLocation.Size = new Size(163, 25);
            txtLocation.TabIndex = 5;
            // btnAddScooter
            btnAddScooter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddScooter.BackColor = Color.FromArgb(39, 174, 96);
            btnAddScooter.Cursor = Cursors.Hand;
            btnAddScooter.FlatAppearance.BorderSize = 0;
            btnAddScooter.FlatStyle = FlatStyle.Flat;
            btnAddScooter.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnAddScooter.ForeColor = Color.White;
            btnAddScooter.Location = new Point(441, 19);
            btnAddScooter.Name = "btnAddScooter";
            btnAddScooter.Size = new Size(139, 36);
            btnAddScooter.TabIndex = 6;
            btnAddScooter.Text = "➕ Dodaj";
            btnAddScooter.UseVisualStyleBackColor = false;
            btnAddScooter.Click += BtnAddScooter_Click;
            // btnDeleteScooter
            btnDeleteScooter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDeleteScooter.BackColor = Color.FromArgb(192, 57, 43);
            btnDeleteScooter.Cursor = Cursors.Hand;
            btnDeleteScooter.FlatAppearance.BorderSize = 0;
            btnDeleteScooter.FlatStyle = FlatStyle.Flat;
            btnDeleteScooter.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnDeleteScooter.ForeColor = Color.White;
            btnDeleteScooter.Location = new Point(586, 19);
            btnDeleteScooter.Name = "btnDeleteScooter";
            btnDeleteScooter.Size = new Size(139, 36);
            btnDeleteScooter.TabIndex = 7;
            btnDeleteScooter.Text = "🗑 Obriši";
            btnDeleteScooter.UseVisualStyleBackColor = false;
            btnDeleteScooter.Click += BtnDeleteScooter_Click;
            // btnPostavljenaNaPunjenje
            btnPostavljenaNaPunjenje.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPostavljenaNaPunjenje.BackColor = Color.FromArgb(41, 128, 185);
            btnPostavljenaNaPunjenje.Cursor = Cursors.Hand;
            btnPostavljenaNaPunjenje.FlatAppearance.BorderSize = 0;
            btnPostavljenaNaPunjenje.FlatStyle = FlatStyle.Flat;
            btnPostavljenaNaPunjenje.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnPostavljenaNaPunjenje.ForeColor = Color.White;
            btnPostavljenaNaPunjenje.Location = new Point(731, 19);
            btnPostavljenaNaPunjenje.Name = "btnPostavljenaNaPunjenje";
            btnPostavljenaNaPunjenje.Size = new Size(155, 36);
            btnPostavljenaNaPunjenje.TabIndex = 8;
            btnPostavljenaNaPunjenje.Text = "🔌 Napuni trotinet";
            btnPostavljenaNaPunjenje.TextAlign = ContentAlignment.MiddleCenter;
            btnPostavljenaNaPunjenje.UseVisualStyleBackColor = false;
            btnPostavljenaNaPunjenje.Click += BtnPostavljenaNaPunjenje_Click;
            // tabHistory
            tabHistory.BackColor = Color.FromArgb(245, 248, 250);
            tabHistory.Controls.Add(lblHistoryTitle);
            tabHistory.Controls.Add(dgvHistory);
            tabHistory.Location = new Point(4, 26);
            tabHistory.Name = "tabHistory";
            tabHistory.Size = new Size(916, 536);
            tabHistory.TabIndex = 1;
            tabHistory.Text = "📋  Historija vožnji";
            // lblHistoryTitle
            lblHistoryTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblHistoryTitle.Location = new Point(10, 10);
            lblHistoryTitle.Name = "lblHistoryTitle";
            lblHistoryTitle.Size = new Size(300, 22);
            lblHistoryTitle.TabIndex = 0;
            lblHistoryTitle.Text = "Historija vožnji u sistemu:";
            // dgvHistory
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.BackColor = Color.White;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvHistory.ColumnHeadersHeight = 35;
            dgvHistory.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dgvHistory.Location = new Point(11, 35);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.RowTemplate.Height = 30;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.Size = new Size(897, 493);
            dgvHistory.TabIndex = 1;
            // tabNotifications
            tabNotifications.BackColor = Color.FromArgb(245, 248, 250);
            tabNotifications.Controls.Add(lblNotifTitle);
            tabNotifications.Controls.Add(dgvNotifications);
            tabNotifications.Controls.Add(btnOznačiProcitano);
            tabNotifications.Controls.Add(btnObrišiNotifikacije);
            tabNotifications.Location = new Point(4, 26);
            tabNotifications.Name = "tabNotifications";
            tabNotifications.Size = new Size(916, 536);
            tabNotifications.TabIndex = 2;
            tabNotifications.Text = "🔔 Notifikacije";
            // lblNotifTitle
            lblNotifTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblNotifTitle.Location = new Point(10, 10);
            lblNotifTitle.Name = "lblNotifTitle";
            lblNotifTitle.Size = new Size(500, 22);
            lblNotifTitle.TabIndex = 0;
            lblNotifTitle.Text = "Sistemske notifikacije – upozorenja o trotinetima:";
            // dgvNotifications
            dgvNotifications.AllowUserToAddRows = false;
            dgvNotifications.AllowUserToDeleteRows = false;
            dgvNotifications.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvNotifications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvNotifications.BackgroundColor = Color.White;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvNotifications.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvNotifications.ColumnHeadersHeight = 35;
            dgvNotifications.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dgvNotifications.Location = new Point(11, 35);
            dgvNotifications.Name = "dgvNotifications";
            dgvNotifications.ReadOnly = true;
            dgvNotifications.RowHeadersVisible = false;
            dgvNotifications.RowTemplate.Height = 32;
            dgvNotifications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNotifications.Size = new Size(897, 432);
            dgvNotifications.TabIndex = 1;
            dgvNotifications.CellClick += DgvNotifications_CellClick;
            // btnOznačiProcitano
            btnOznačiProcitano.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnOznačiProcitano.BackColor = Color.FromArgb(39, 174, 96);
            btnOznačiProcitano.Cursor = Cursors.Hand;
            btnOznačiProcitano.FlatAppearance.BorderSize = 0;
            btnOznačiProcitano.FlatStyle = FlatStyle.Flat;
            btnOznačiProcitano.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnOznačiProcitano.ForeColor = Color.White;
            btnOznačiProcitano.Location = new Point(11, 479);
            btnOznačiProcitano.Name = "btnOznačiProcitano";
            btnOznačiProcitano.Size = new Size(278, 38);
            btnOznačiProcitano.TabIndex = 2;
            btnOznačiProcitano.Text = "✅  Označi sve kao pročitano";
            btnOznačiProcitano.UseVisualStyleBackColor = false;
            btnOznačiProcitano.Click += BtnOznačiProcitano_Click;
            // btnObrišiNotifikacije
            btnObrišiNotifikacije.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnObrišiNotifikacije.BackColor = Color.FromArgb(192, 57, 43);
            btnObrišiNotifikacije.Cursor = Cursors.Hand;
            btnObrišiNotifikacije.FlatAppearance.BorderSize = 0;
            btnObrišiNotifikacije.FlatStyle = FlatStyle.Flat;
            btnObrišiNotifikacije.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnObrišiNotifikacije.ForeColor = Color.White;
            btnObrišiNotifikacije.Location = new Point(299, 479);
            btnObrišiNotifikacije.Name = "btnObrišiNotifikacije";
            btnObrišiNotifikacije.Size = new Size(220, 38);
            btnObrišiNotifikacije.TabIndex = 3;
            btnObrišiNotifikacije.Text = "🗑️  Obriši sve";
            btnObrišiNotifikacije.UseVisualStyleBackColor = false;
            btnObrišiNotifikacije.Click += BtnObrišiNotifikacije_Click;
            // AdminForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(924, 621);
            Controls.Add(tabControl);
            Controls.Add(panelTop);
            MinimumSize = new Size(900, 660);
            Name = "AdminForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "\U0001f6f4 GoTrot Admin Panel";
            panelTop.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabScooters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvManageScooters).EndInit();
            panelAddScooter.ResumeLayout(false);
            panelAddScooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numBattery).EndInit();
            tabHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            tabNotifications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvNotifications).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnAdminOdjava;
        private System.Windows.Forms.Button btnAdminZatvori;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabScooters;
        private System.Windows.Forms.DataGridView dgvManageScooters;
        private System.Windows.Forms.Panel panelAddScooter;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label lblBattery;
        private System.Windows.Forms.NumericUpDown numBattery;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Button btnAddScooter;
        private System.Windows.Forms.Button btnDeleteScooter;
        private System.Windows.Forms.Button btnPostavljenaNaPunjenje;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.TabPage tabNotifications;
        private System.Windows.Forms.Label lblNotifTitle;
        private System.Windows.Forms.DataGridView dgvNotifications;
        private System.Windows.Forms.Button btnOznačiProcitano;
        private System.Windows.Forms.Button btnObrišiNotifikacije;
        private Label label1;
    }
}