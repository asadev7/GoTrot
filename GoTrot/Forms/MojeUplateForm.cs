using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GoTrot.Data;
using GoTrot.Models;
using GoTrot.Services;

namespace GoTrot.Forms
{
    public partial class MojeUplateForm : Form
    {
        private AppDbContext _db = new AppDbContext();
        private User _currentUser;

        public MojeUplateForm(User user)
        {
            _currentUser = user;
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            UcitajUplate();
            this.Load += (s, e) => ThemeManager.Apply(this);
        }

        private void UcitajUplate()
        {
            var uplate = _db.Payments
                .Where(p => p.UserId == _currentUser.Id)
                .AsEnumerable()
                .Select(p => new
                {
                    Datum = p.VrijemeUplate.ToString("dd.MM.yyyy"),
                    Vrijeme = p.VrijemeUplate.ToString("HH:mm"),
                    Iznos_KM = p.Iznos.ToString("F2") + " KM",
                    Napomena = p.Napomena
                })
                .OrderByDescending(p => p.Datum)
                .ToList();

            dgvUplate.DataSource = uplate;
            if (dgvUplate.Columns.Count > 0)
            {
                dgvUplate.Columns["Datum"].HeaderText = "📅 Datum";
                dgvUplate.Columns["Vrijeme"].HeaderText = "🕐 Vrijeme";
                dgvUplate.Columns["Iznos_KM"].HeaderText = "💳 Iznos";
                dgvUplate.Columns["Napomena"].HeaderText = "📝 Napomena";

                // Resetuj AutoSize prije postavljanja fiksnih širina
                dgvUplate.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                dgvUplate.Columns["Datum"].Width = 120;
                dgvUplate.Columns["Vrijeme"].Width = 120;
                dgvUplate.Columns["Iznos_KM"].Width = 120;
                dgvUplate.Columns["Napomena"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvUplate.EnableHeadersVisualStyles = false;
            dgvUplate.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.IsDarkMode ? ThemeManager.DarkCard : Color.FromArgb(44, 62, 80);
            dgvUplate.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUplate.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvUplate.DefaultCellStyle.BackColor = ThemeManager.Panel;
            dgvUplate.DefaultCellStyle.ForeColor = ThemeManager.Text;
            dgvUplate.DefaultCellStyle.SelectionBackColor = ThemeManager.Accent;
            dgvUplate.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvUplate.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.IsDarkMode ? ThemeManager.DarkCard : Color.FromArgb(235, 245, 255);
            dgvUplate.AlternatingRowsDefaultCellStyle.ForeColor = ThemeManager.Text;
            dgvUplate.BackgroundColor = ThemeManager.Panel;

            // Statistika
            if (uplate.Any())
            {
                decimal ukupno = _db.Payments
                    .Where(p => p.UserId == _currentUser.Id)
                    .AsEnumerable()
                    .Sum(p => p.Iznos);

                lblStats.Text = $"Ukupno uplata: {uplate.Count}   |   Ukupno uplaćeno: {ukupno:F2} KM   |   Trenutni kredit: {_currentUser.Balance:F2} KM";
            }
            else
            {
                lblStats.Text = $"Nemate još nijednu uplatu.   |   Trenutni kredit: {_currentUser.Balance:F2} KM";
            }
        }

        private void BtnZatvori_Click(object sender, EventArgs e) => this.Close();
    }
}