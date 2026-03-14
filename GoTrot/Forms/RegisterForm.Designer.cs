namespace GoTrot.Forms
{
    partial class RegisterForm
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
            lblSubtitle = new Label();
            lblIme = new Label();
            txtIme = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new TextBox();
            chkShowPassword = new CheckBox();
            lblBonus = new Label();
            btnPotvrdi = new Button();
            btnOdustani = new Button();
            panelTop.SuspendLayout();
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
            panelTop.Size = new Size(440, 75);
            panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Kreiranje novog naloga";
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblSubtitle.ForeColor = Color.FromArgb(210, 235, 255);
            lblSubtitle.Location = new Point(20, 44);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(400, 18);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Registracijom dobijate 20 KM bonusa!";
            // 
            // lblIme
            // 
            lblIme.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblIme.Location = new Point(40, 92);
            lblIme.Name = "lblIme";
            lblIme.Size = new Size(130, 22);
            lblIme.TabIndex = 1;
            lblIme.Text = "Ime i prezime:";
            // 
            // txtIme
            // 
            txtIme.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            txtIme.Location = new Point(40, 116);
            txtIme.Name = "txtIme";
            txtIme.PlaceholderText = "Unesite ime i prezime";
            txtIme.Size = new Size(360, 27);
            txtIme.TabIndex = 2;
            // 
            // lblEmail
            // 
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblEmail.Location = new Point(40, 158);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(130, 22);
            lblEmail.TabIndex = 3;
            lblEmail.Text = "Email adresa:";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            txtEmail.Location = new Point(40, 182);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "korisnik@email.com";
            txtEmail.Size = new Size(360, 27);
            txtEmail.TabIndex = 4;
            // 
            // lblPassword
            // 
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblPassword.Location = new Point(40, 224);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(130, 22);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Lozinka:";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            txtPassword.Location = new Point(40, 248);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.PlaceholderText = "Minimalno 8 karaktera";
            txtPassword.Size = new Size(360, 27);
            txtPassword.TabIndex = 6;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblConfirmPassword.Location = new Point(40, 290);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(140, 22);
            lblConfirmPassword.TabIndex = 7;
            lblConfirmPassword.Text = "Potvrdi lozinku:";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            txtConfirmPassword.Location = new Point(40, 314);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '●';
            txtConfirmPassword.PlaceholderText = "Ponovite lozinku";
            txtConfirmPassword.Size = new Size(360, 27);
            txtConfirmPassword.TabIndex = 8;
            // 
            // chkShowPassword
            // 
            chkShowPassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            chkShowPassword.ForeColor = Color.Gray;
            chkShowPassword.Location = new Point(40, 350);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(130, 20);
            chkShowPassword.TabIndex = 9;
            chkShowPassword.Text = "Prikaži lozinke";
            chkShowPassword.CheckedChanged += ChkShowPassword_CheckedChanged;
            // 
            // lblBonus
            // 
            lblBonus.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            lblBonus.ForeColor = Color.FromArgb(39, 174, 96);
            lblBonus.Location = new Point(40, 378);
            lblBonus.Name = "lblBonus";
            lblBonus.Size = new Size(360, 20);
            lblBonus.TabIndex = 10;
            lblBonus.Text = "💰  Novi korisnici dobijaju 20 KM startnog kredita!";
            // 
            // btnPotvrdi
            // 
            btnPotvrdi.BackColor = Color.FromArgb(39, 174, 96);
            btnPotvrdi.Cursor = Cursors.Hand;
            btnPotvrdi.FlatAppearance.BorderSize = 0;
            btnPotvrdi.FlatStyle = FlatStyle.Flat;
            btnPotvrdi.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnPotvrdi.ForeColor = Color.White;
            btnPotvrdi.Location = new Point(40, 412);
            btnPotvrdi.Name = "btnPotvrdi";
            btnPotvrdi.Size = new Size(165, 42);
            btnPotvrdi.TabIndex = 11;
            btnPotvrdi.Text = "✔  Registruj se";
            btnPotvrdi.UseVisualStyleBackColor = false;
            btnPotvrdi.Click += BtnPotvrdi_Click;
            // 
            // btnOdustani
            // 
            btnOdustani.BackColor = Color.FromArgb(220, 220, 220);
            btnOdustani.Cursor = Cursors.Hand;
            btnOdustani.FlatAppearance.BorderSize = 0;
            btnOdustani.FlatStyle = FlatStyle.Flat;
            btnOdustani.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnOdustani.Location = new Point(235, 412);
            btnOdustani.Name = "btnOdustani";
            btnOdustani.Size = new Size(165, 42);
            btnOdustani.TabIndex = 12;
            btnOdustani.Text = "Odustani";
            btnOdustani.UseVisualStyleBackColor = false;
            btnOdustani.Click += BtnOdustani_Click;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(440, 472);
            Controls.Add(panelTop);
            Controls.Add(lblIme);
            Controls.Add(txtIme);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(chkShowPassword);
            Controls.Add(lblBonus);
            Controls.Add(btnPotvrdi);
            Controls.Add(btnOdustani);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "\U0001f6f4 GoTrot Registracija";
            panelTop.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblIme;
        private System.Windows.Forms.TextBox txtIme;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Label lblBonus;
        private System.Windows.Forms.Button btnPotvrdi;
        private System.Windows.Forms.Button btnOdustani;
    }
}
