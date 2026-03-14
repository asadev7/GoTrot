using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GoTrot.Data;
using GoTrot.Models;
using GoTrot.Services;

namespace GoTrot.Forms
{
    public partial class RegisterForm : Form
    {
        private AppDbContext _db = new AppDbContext();

        // Ispravan email regex: name@domain.tld
        private static readonly Regex EmailRegex = new Regex(
            @"^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled);

        public RegisterForm()
        {
            InitializeComponent();
            this.Load += (s, e) => ThemeManager.Apply(this);
        }

        private void BtnPotvrdi_Click(object sender, EventArgs e)
        {
            string ime = txtIme.Text.Trim();
            string email = txtEmail.Text.Trim();
            string lozinka = txtPassword.Text;
            string potvrda = txtConfirmPassword.Text;

            // Provjera praznih polja
            if (string.IsNullOrWhiteSpace(ime) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(lozinka) || string.IsNullOrWhiteSpace(potvrda))
            {
                MessageBox.Show("Molimo unesite sve podatke.", "Upozorenje",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Regex validacija email formata
            if (!EmailRegex.IsMatch(email))
            {
                MessageBox.Show("Unesite ispravnu email adresu.\nPrimjer: ime@domena.com", "Greška",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Minimalna dužina lozinke
            if (lozinka.Length < 8)
            {
                MessageBox.Show("Lozinka mora imati najmanje 8 karaktera.", "Greška",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Podudaranje lozinki
            if (lozinka != potvrda)
            {
                MessageBox.Show("Lozinke se ne podudaraju!\nProvjerite unesenu lozinku.", "Greška",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Clear();
                txtConfirmPassword.Focus();
                return;
            }

            // Provjera duplikata emaila
            if (_db.Users.Any(u => u.Email == email))
            {
                MessageBox.Show("Korisnik sa ovim emailom već postoji!", "Greška",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            var noviKorisnik = new User
            {
                ImePrezime = ime,
                Email = email,
                // Lozinka se čuva kao BCrypt hash — nikad plain text
                Password = BCrypt.Net.BCrypt.HashPassword(lozinka),
                Balance = 20.00m,
                IsAdmin = false
            };

            _db.Users.Add(noviKorisnik);
            _db.SaveChanges();

            MessageBox.Show(
                $"Registracija uspješna!\n\nDobrodošli, {noviKorisnik.ImePrezime}!\n" +
                $"Na račun vam je dodano 20 KM bonusa.\n\nSada se možete prijaviti.",
                "Uspjeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void BtnOdustani_Click(object sender, EventArgs e) => this.Close();

        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            char pc = chkShowPassword.Checked ? '\0' : '●';
            txtPassword.PasswordChar = pc;
            txtConfirmPassword.PasswordChar = pc;
        }
    }
}
