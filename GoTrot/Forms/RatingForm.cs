using System.Drawing;
using System.Windows.Forms;
using GoTrot.Data;
using GoTrot.Models;
using GoTrot.Services;

namespace GoTrot.Forms
{
    /// <summary>
    /// Dialog za ocjenjivanje vožnje (1-5 zvjezdica) nakon završetka.
    /// </summary>
    public class RatingForm : Form
    {
        private int _ocjena = 5;
        private readonly int _rideId;
        private TextBox txtKomentar = null!;
        private Label[] zvjezdice = null!;

        public RatingForm(int rideId, string scooterModel)
        {
            _rideId = rideId;

            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(440, 360);
            Text = "Ocijenite vožnju";
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = ThemeManager.Bg;

            var lblNaslov = new Label
            {
                Text = $"Kako ocjenjujete vožnju\ntrotineta {scooterModel}?",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = ThemeManager.Text,
                AutoSize = false,
                Size = new Size(400, 55),
                Location = new Point(20, 16),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Zvjezdice
            zvjezdice = new Label[5];
            for (int i = 0; i < 5; i++)
            {
                int idx = i;
                zvjezdice[i] = new Label
                {
                    Text = "★",
                    Font = new Font("Segoe UI", 28F),
                    ForeColor = Color.FromArgb(243, 156, 18),
                    AutoSize = true,
                    Location = new Point(20 + i * 72, 88),
                    Cursor = Cursors.Hand,
                    Tag = i + 1
                };
                zvjezdice[i].Click += (s, e) => { _ocjena = idx + 1; OsveziZvjezdice(); };
                zvjezdice[i].MouseEnter += (s, e) => OsveziZvjezdicePrijed(idx + 1);
                zvjezdice[i].MouseLeave += (s, e) => OsveziZvjezdice();
            }

            var lblKomentar = new Label
            {
                Text = "Komentar (opcionalno):",
                Font = new Font("Segoe UI", 9F),
                ForeColor = ThemeManager.Sub,
                AutoSize = true,
                Location = new Point(20, 162)
            };

            txtKomentar = new TextBox
            {
                Location = new Point(20, 185),
                Size = new Size(390, 70),
                Multiline = true,
                BackColor = ThemeManager.Card,
                ForeColor = ThemeManager.Text,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9F)
            };

            var btnSpremi = new Button
            {
                Text = "✅ Spremi ocjenu",
                Location = new Point(20, 272),
                Size = new Size(185, 38),
                BackColor = ThemeManager.Accent,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSpremi.FlatAppearance.BorderSize = 0;
            btnSpremi.Click += BtnSpremi_Click;

            var btnPreskoci = new Button
            {
                Text = "Preskoči",
                Location = new Point(220, 272),
                Size = new Size(120, 38),
                BackColor = Color.FromArgb(200, 200, 200),
                ForeColor = Color.FromArgb(80, 80, 80),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand
            };
            btnPreskoci.FlatAppearance.BorderSize = 0;
            btnPreskoci.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[] { lblNaslov, lblKomentar, txtKomentar, btnSpremi, btnPreskoci });
            foreach (var z in zvjezdice) Controls.Add(z);

            OsveziZvjezdice();
        }

        private void OsveziZvjezdice()
        {
            for (int i = 0; i < 5; i++)
                zvjezdice[i].ForeColor = i < _ocjena
                    ? Color.FromArgb(243, 156, 18)
                    : Color.FromArgb(200, 200, 200);
        }

        private void OsveziZvjezdicePrijed(int do_idx)
        {
            for (int i = 0; i < 5; i++)
                zvjezdice[i].ForeColor = i < do_idx
                    ? Color.FromArgb(255, 180, 0)
                    : Color.FromArgb(200, 200, 200);
        }

        private void BtnSpremi_Click(object? sender, EventArgs e)
        {
            using var db = new AppDbContext();
            db.Rati.Add(new RatingVoznje
            {
                RideId = _rideId,
                Ocjena = _ocjena,
                Komentar = txtKomentar.Text.Trim(),
                VrijemeOcjene = DateTime.Now
            });
            db.SaveChanges();
            ToastNotification.Uspjeh($"Hvala na ocjeni! Dali ste {_ocjena}★");
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}