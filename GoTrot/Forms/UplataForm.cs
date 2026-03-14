using System;
using System.Windows.Forms;
using GoTrot.Data;
using GoTrot.Models;
using GoTrot.Services;

namespace GoTrot.Forms
{
    public partial class UplataForm : Form
    {
        private AppDbContext _db = new AppDbContext();
        private readonly PaymentService _paymentService;
        private User _currentUser;

        public UplataForm(User user)
        {
            _currentUser = user;
            _paymentService = new PaymentService(_db);
            InitializeComponent();
            lblTrenutniKredit.Text = $"Trenutni kredit: {_currentUser.Balance:F2} KM";
            this.Load += (s, e) => ThemeManager.Apply(this);
        }

        private void Btn5km_Click(object sender, EventArgs e) => txtIznos.Text = "5";
        private void Btn10km_Click(object sender, EventArgs e) => txtIznos.Text = "10";
        private void Btn20km_Click(object sender, EventArgs e) => txtIznos.Text = "20";
        private void Btn50km_Click(object sender, EventArgs e) => txtIznos.Text = "50";

        private void BtnUplati_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtIznos.Text.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out decimal iznos))
            {
                ToastNotification.Greska("Unesite ispravan iznos.");
                return;
            }

            var greska = _paymentService.ValidirajUplatu(iznos);
            if (greska != null)
            {
                ToastNotification.Greska(greska);
                return;
            }

            var korisnik = _db.Users.Find(_currentUser.Id)!;
            _paymentService.IzvrsiUplatu(korisnik, iznos);

            ToastNotification.Uspjeh($"Uplata uspješna! Uplaćeno: {iznos:F2} KM — Novi kredit: {korisnik.Balance:F2} KM");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnOdustani_Click(object sender, EventArgs e) => this.Close();

        private void TxtIznos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.' && e.KeyChar != '\b')
                e.Handled = true;
        }
    }
}
