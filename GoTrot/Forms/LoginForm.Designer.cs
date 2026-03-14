namespace GoTrot.Forms
{
    partial class LoginForm
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
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            chkShowPassword = new CheckBox();
            lblCapsLock = new Label();
            btnLogin = new Button();
            lblForgot = new Label();
            lblSep = new Label();
            btnRegister = new Button();
            lblBrand = new Label();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(26, 82, 118);
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(420, 70);
            panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(-10, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(420, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "\U0001f6f4  GoTrot";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.White;
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            txtEmail.Location = new Point(40, 95);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = " Email adresa";
            txtEmail.Size = new Size(340, 27);
            txtEmail.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            txtPassword.Location = new Point(40, 138);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.PlaceholderText = " Lozinka";
            txtPassword.Size = new Size(340, 27);
            txtPassword.TabIndex = 2;
            txtPassword.KeyDown += TxtPassword_KeyDown;
            txtPassword.KeyUp += TxtPassword_KeyUp;
            // 
            // chkShowPassword
            // 
            chkShowPassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            chkShowPassword.ForeColor = Color.FromArgb(101, 103, 107);
            chkShowPassword.Location = new Point(40, 173);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(130, 20);
            chkShowPassword.TabIndex = 3;
            chkShowPassword.Text = "Prikaži lozinku";
            chkShowPassword.CheckedChanged += ChkShowPassword_CheckedChanged;
            // 
            // lblCapsLock
            // 
            lblCapsLock.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblCapsLock.ForeColor = Color.FromArgb(200, 100, 0);
            lblCapsLock.Location = new Point(232, 173);
            lblCapsLock.Name = "lblCapsLock";
            lblCapsLock.Size = new Size(148, 20);
            lblCapsLock.TabIndex = 9;
            lblCapsLock.Text = "⚠️ Caps Lock je uključen";
            lblCapsLock.Visible = false;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(24, 119, 242);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(40, 207);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(340, 46);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Prijavi se";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // lblForgot
            // 
            lblForgot.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblForgot.ForeColor = Color.FromArgb(101, 103, 107);
            lblForgot.Location = new Point(40, 265);
            lblForgot.Name = "lblForgot";
            lblForgot.Size = new Size(340, 20);
            lblForgot.TabIndex = 5;
            lblForgot.Text = "Zaboravili ste lozinku?";
            lblForgot.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSep
            // 
            lblSep.BackColor = Color.FromArgb(218, 220, 224);
            lblSep.Location = new Point(40, 300);
            lblSep.Name = "lblSep";
            lblSep.Size = new Size(340, 1);
            lblSep.TabIndex = 6;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.FromArgb(242, 242, 242);
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 180);
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnRegister.ForeColor = Color.FromArgb(26, 82, 118);
            btnRegister.Location = new Point(80, 318);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(260, 46);
            btnRegister.TabIndex = 7;
            btnRegister.Text = "Kreiraj novi nalog";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += BtnRegister_Click;
            // 
            // lblBrand
            // 
            lblBrand.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            lblBrand.ForeColor = Color.FromArgb(26, 82, 118);
            lblBrand.Location = new Point(35, 380);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(340, 26);
            lblBrand.TabIndex = 8;
            lblBrand.Text = "\U0001f6f4 GoTrot";
            lblBrand.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LoginForm
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(420, 425);
            Controls.Add(panelTop);
            Controls.Add(txtEmail);
            Controls.Add(txtPassword);
            Controls.Add(chkShowPassword);
            Controls.Add(lblCapsLock);
            Controls.Add(btnLogin);
            Controls.Add(lblForgot);
            Controls.Add(lblSep);
            Controls.Add(btnRegister);
            Controls.Add(lblBrand);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "\U0001f6f4 GoTrot Prijava";
            panelTop.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Label lblCapsLock;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblForgot;
        private System.Windows.Forms.Label lblSep;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lblBrand;
    }
}
