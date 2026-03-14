using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GoTrot.Data;
using GoTrot.Models;
using GoTrot.Services;

namespace GoTrot.Forms
{
    public partial class MojeVoznjeForm : Form
    {
        private AppDbContext _db = new AppDbContext();
        private User _currentUser;

        public MojeVoznjeForm(User user)
        {
            _currentUser = user;
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            UcitajVoznje();
            DodajDugmeExport();
            this.Load += (s, e) => ThemeManager.Apply(this);
        }

        private void DodajDugmeExport()
        {
            var btnExport = new Button
            {
                Text = "📥  Exportuj CSV",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(118, 40),
                Cursor = Cursors.Hand
            };
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += BtnExportCsv_Click;

            // Dodaj dugme pored btnZatvori
            this.Load += (s, e) =>
            {
                btnExport.Location = new Point(btnZatvori.Left - btnExport.Width - 10, btnZatvori.Top);
                btnZatvori.Parent?.Controls.Add(btnExport);
            };
        }

        private void UcitajVoznje()
        {
            var voznje = _db.Rides
                .Where(r => r.UserId == _currentUser.Id && r.EndTime != null)
                .AsEnumerable()
                .Select(r => new
                {
                    Trotinet = _db.Scooters.FirstOrDefault(s => s.Id == r.ScooterId)?.Model ?? "Nepoznat",
                    Datum = r.StartTime.ToString("dd.MM.yyyy"),
                    Pocetak = r.StartTime.ToString("HH:mm"),
                    Kraj = r.EndTime.HasValue ? r.EndTime.Value.ToString("HH:mm") : "-",
                    Trajanje = r.EndTime.HasValue
                        ? $"{(r.EndTime.Value - r.StartTime).TotalMinutes:F1} min"
                        : "-",
                    Cijena = r.TotalCost.ToString("F2") + " KM"
                })
                .OrderByDescending(r => r.Datum)
                .ToList();

            dgvVoznje.DataSource = voznje;

            if (dgvVoznje.Columns.Count > 0)
            {
                dgvVoznje.Columns["Trotinet"].HeaderText = "🛴 Trotinet";
                dgvVoznje.Columns["Datum"].HeaderText = "📅 Datum";
                dgvVoznje.Columns["Pocetak"].HeaderText = "🕐 Početak";
                dgvVoznje.Columns["Kraj"].HeaderText = "🕑 Kraj";
                dgvVoznje.Columns["Trajanje"].HeaderText = "⏱ Trajanje";
                dgvVoznje.Columns["Cijena"].HeaderText = "🪙 Cijena";
            }

            dgvVoznje.EnableHeadersVisualStyles = false;
            dgvVoznje.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.IsDarkMode ? ThemeManager.DarkCard : Color.FromArgb(44, 62, 80);
            dgvVoznje.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvVoznje.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvVoznje.DefaultCellStyle.BackColor = ThemeManager.Panel;
            dgvVoznje.DefaultCellStyle.ForeColor = ThemeManager.Text;
            dgvVoznje.DefaultCellStyle.SelectionBackColor = ThemeManager.Accent;
            dgvVoznje.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvVoznje.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.IsDarkMode ? ThemeManager.DarkCard : Color.FromArgb(235, 245, 255);
            dgvVoznje.AlternatingRowsDefaultCellStyle.ForeColor = ThemeManager.Text;
            dgvVoznje.BackgroundColor = ThemeManager.Panel;

            if (voznje.Any())
            {
                int ukupnoVoznji = voznje.Count;
                decimal ukupnoPlaceno = _db.Rides
                    .Where(r => r.UserId == _currentUser.Id && r.EndTime != null)
                    .AsEnumerable()
                    .Sum(r => r.TotalCost);

                lblStats.Text = $"Ukupno vožnji: {ukupnoVoznji}   |   Ukupno potrošeno: {ukupnoPlaceno:F2} KM";
            }
            else
            {
                lblStats.Text = "Nemate još nijednu završenu vožnju.";
            }
        }

        /// <summary>
        /// Exportuje historiju vožnji kao CSV fajl (UTF-8 sa BOM za Excel).
        /// </summary>
        private void BtnExportCsv_Click(object? sender, EventArgs e)
        {
            var voznje = _db.Rides
                .Where(r => r.UserId == _currentUser.Id && r.EndTime != null)
                .OrderByDescending(r => r.StartTime)
                .AsEnumerable()
                .Select(r => new
                {
                    Trotinet = _db.Scooters.FirstOrDefault(s => s.Id == r.ScooterId)?.Model ?? "Nepoznat",
                    r.StartTime,
                    EndTime = r.EndTime!.Value,
                    r.TotalCost
                })
                .ToList();

            if (!voznje.Any())
            {
                MessageBox.Show("Nema vožnji za export.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Title = "Sačuvaj historiju vožnji",
                Filter = "CSV fajl (*.csv)|*.csv",
                FileName = $"GoTrot_Voznje_{_currentUser.ImePrezime.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd}.csv",
                DefaultExt = "csv"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                // UTF-8 sa BOM — Excel ispravno prikazuje bosanska slova
                var sb = new StringBuilder();
                sb.AppendLine("Trotinet,Datum,Početak,Kraj,Trajanje (min),Cijena (KM)");

                foreach (var v in voznje)
                {
                    double trajanje = (v.EndTime - v.StartTime).TotalMinutes;
                    sb.AppendLine(
                        $"\"{v.Trotinet}\"," +
                        $"{v.StartTime:dd.MM.yyyy}," +
                        $"{v.StartTime:HH:mm}," +
                        $"{v.EndTime:HH:mm}," +
                        $"{trajanje:F1}," +
                        $"{v.TotalCost:F2}");
                }

                // Summary red
                decimal ukupno = voznje.Sum(v => v.TotalCost);
                sb.AppendLine();
                sb.AppendLine($"\"UKUPNO ({voznje.Count} vožnji)\",,,,,{ukupno:F2}");

                File.WriteAllText(dialog.FileName, sb.ToString(), new UTF8Encoding(true));

                MessageBox.Show(
                    $"✅ Export uspješan!\n\nFajl sačuvan:\n{dialog.FileName}\n\nUkupno vožnji: {voznje.Count}\nUkupno potrošeno: {ukupno:F2} KM",
                    "Export završen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri exportu:\n{ex.Message}", "Greška",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnZatvori_Click(object sender, EventArgs e) => this.Close();
    }
}