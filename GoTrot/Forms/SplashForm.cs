using System;
using System.Drawing;
using System.Windows.Forms;

namespace GoTrot.Forms
{
    /// <summary>
    /// Loading screen koji se prikazuje dok se SQL Server konekcija inicijalizuje.
    /// Sprječava zamrzavanje UI-a pri pokretanju.
    /// </summary>
    public class SplashForm : Form
    {
        private Label lblNaslov;
        private Label lblPoruka;
        private ProgressBar progressBar;
        private Label lblVersion;

        public SplashForm()
        {
            InitializeSplash();
        }

        private void InitializeSplash()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(440, 240);
            this.BackColor = Color.FromArgb(22, 22, 34);
            this.TopMost = true;

            // Naziv aplikacije
            lblNaslov = new Label
            {
                Text = "🛴  GoTrot",
                Font = new Font("Segoe UI", 26F, FontStyle.Bold),
                ForeColor = Color.FromArgb(13, 158, 138),
                AutoSize = true,
                Location = new Point(30, 32)
            };

            // Podnaslov
            var lblSub = new Label
            {
                Text = "E-Scooter Sharing System",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(160, 160, 180),
                AutoSize = true,
                Location = new Point(36, 80)
            };

            // Status poruka
            lblPoruka = new Label
            {
                Text = "Spajanje na bazu podataka...",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(200, 200, 220),
                AutoSize = true,
                Location = new Point(30, 140)
            };

            // Progress bar
            progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 25,
                Location = new Point(30, 168),
                Size = new Size(380, 6),
                ForeColor = Color.FromArgb(13, 158, 138)
            };

            // Verzija
            lblVersion = new Label
            {
                Text = "v1.0  |  IB230014",
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(100, 100, 120),
                AutoSize = true,
                Location = new Point(30, 205)
            };

            this.Controls.AddRange(new Control[]
            {
                lblNaslov, lblSub, lblPoruka, progressBar, lblVersion
            });
        }

        public void SetPoruka(string poruka)
        {
            if (lblPoruka.InvokeRequired)
                lblPoruka.Invoke(new Action(() => lblPoruka.Text = poruka));
            else
                lblPoruka.Text = poruka;
        }
    }
}
