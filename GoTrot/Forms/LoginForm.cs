using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GoTrot.Data;
using GoTrot.Models;
using GoTrot.Services;

namespace GoTrot.Forms
{
    public partial class LoginForm : Form
    {
        private AppDbContext _db = new AppDbContext();
        public User LoggedInUser { get; private set; } = null!;

        public LoginForm()
        {
            InitializeComponent();
            EnsureAdminExists();
            this.Load += (s, e) => ThemeManager.Apply(this);
        }

        /// <summary>
        /// Kreira admin nalog ako ne postoji — lozinka se čuva hashovana (BCrypt).
        /// </summary>
        private void EnsureAdminExists()
        {
            if (!_db.Users.Any(u => u.Email == "admin@gotrot.ba"))
            {
                _db.Users.Add(new User
                {
                    ImePrezime = "Administrator",
                    Email = "admin@gotrot.ba",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Balance = 0,
                    IsAdmin = true
                });
                _db.SaveChanges();
            }
            else
            {
                // Postavi IsAdmin = true ako nije bio postavljen (upgrade starih baza)
                var admin = _db.Users.FirstOrDefault(u => u.Email == "admin@gotrot.ba");
                if (admin != null && !admin.IsAdmin)
                {
                    admin.IsAdmin = true;
                    _db.SaveChanges();
                }
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Molimo unesite email i lozinku.", "Upozorenje",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                MessageBox.Show("Korisnik sa ovim emailom ne postoji.\nRegistrujte se ili provjerite email.",
                    "Korisnik nije pronađen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Provjera lozinke — podržava i stare plain-text lozinke (migracija)
            bool ispravnaLozinka = VerifikujLozinku(password, user.Password);

            if (!ispravnaLozinka)
            {
                MessageBox.Show("Pogrešna lozinka!\nPokušajte ponovo.",
                    "Pogrešna lozinka", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            // Ako je stara plain-text lozinka ispravna, hashuj je sada (tiha migracija)
            if (!user.Password.StartsWith("$2"))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(password);
                _db.SaveChanges();
            }

            if (user.IsAdmin)
            {
                var admin = new AdminForm();
                admin.ShowDialog();
                if (admin.Odjavljen)
                {
                    // Vrati na landing page — zatvori LoginForm bez OK
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
                txtEmail.Clear();
                txtPassword.Clear();
                txtEmail.Focus();
                return;
            }

            LoggedInUser = user;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Podržava i BCrypt hashove i stare plain-text lozinke (radi migracije starih naloga).
        /// </summary>
        private static bool VerifikujLozinku(string unesena, string sacuvana)
        {
            if (sacuvana.StartsWith("$2"))
            {
                try { return BCrypt.Net.BCrypt.Verify(unesena, sacuvana); }
                catch { return false; }
            }
            return unesena == sacuvana;
        }

        private void TxtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            lblCapsLock.Visible = Control.IsKeyLocked(Keys.CapsLock);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            var reg = new RegisterForm();
            reg.StartPosition = FormStartPosition.CenterScreen;
            reg.TopMost = true;
            reg.Shown += (s, e) =>
            {
                reg.Activate();
                reg.BringToFront();
                reg.Focus();
                System.Threading.Tasks.Task.Delay(300).ContinueWith(_ =>
                    reg.Invoke((Action)(() => reg.TopMost = false)));
            };
            reg.ShowDialog();
        }

        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●';
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnLogin_Click(sender, e);
        }
    }
}