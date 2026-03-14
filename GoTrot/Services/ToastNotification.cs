using System.Drawing;
using System.Windows.Forms;
using GoTrot.Services;

namespace GoTrot.Services
{
    public enum ToastTip { Info, Success, Warning, Error }

    /// <summary>
    /// Mali popup u donjem desnom uglu forme — nestaje sam nakon 3 sekunde.
    /// Koristi se umjesto MessageBox.Show za nekriticne poruke.
    /// </summary>
    public class ToastNotification : Form
    {
        private static readonly Queue<(string msg, ToastTip tip)> _queue = new();
        private static bool _isShowing = false;

        private ToastNotification(string poruka, ToastTip tip)
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            ShowInTaskbar = false;
            Size = new Size(340, 64);
            Opacity = 0;

            Color bgColor = tip switch
            {
                ToastTip.Success => Color.FromArgb(39, 174, 96),
                ToastTip.Warning => Color.FromArgb(243, 156, 18),
                ToastTip.Error   => Color.FromArgb(192, 57, 43),
                _                => Color.FromArgb(44, 62, 80)
            };

            string icon = tip switch
            {
                ToastTip.Success => "✅",
                ToastTip.Warning => "⚠️",
                ToastTip.Error   => "❌",
                _                => "ℹ️"
            };

            BackColor = bgColor;

            var lbl = new Label
            {
                Text = $"  {icon}  {poruka}",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 8, 0)
            };
            Controls.Add(lbl);

            // Pozicioniranje — donji desni ugao ekrana
            var screen = Screen.PrimaryScreen!.WorkingArea;
            Location = new Point(screen.Right - Width - 16, screen.Bottom - Height - 16);

            // Fade in
            var fadeIn = new System.Windows.Forms.Timer { Interval = 30 };
            fadeIn.Tick += (s, e) =>
            {
                Opacity = Math.Min(1.0, Opacity + 0.12);
                if (Opacity >= 1.0) fadeIn.Stop();
            };

            // Auto-close timer
            var closeTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            closeTimer.Tick += (s, e) =>
            {
                closeTimer.Stop();
                var fadeOut = new System.Windows.Forms.Timer { Interval = 30 };
                fadeOut.Tick += (s2, e2) =>
                {
                    Opacity = Math.Max(0, Opacity - 0.10);
                    if (Opacity <= 0) { fadeOut.Stop(); Close(); }
                };
                fadeOut.Start();
            };

            Shown += (s, e) => { fadeIn.Start(); closeTimer.Start(); };
            FormClosed += (s, e) =>
            {
                _isShowing = false;
                PrikaziSljedeci();
            };
        }

        private static void PrikaziSljedeci()
        {
            if (_isShowing || _queue.Count == 0) return;
            _isShowing = true;
            var (msg, tip) = _queue.Dequeue();
            new ToastNotification(msg, tip).Show();
        }

        /// <summary>
        /// Prikaži toast poruku. Ako je već neka vidljiva, doda se u red čekanja.
        /// </summary>
        public static void Prikazi(string poruka, ToastTip tip = ToastTip.Info)
        {
            _queue.Enqueue((poruka, tip));
            PrikaziSljedeci();
        }

        // Kratice
        public static void Info(string msg)    => Prikazi(msg, ToastTip.Info);
        public static void Uspjeh(string msg)  => Prikazi(msg, ToastTip.Success);
        public static void Upozorenje(string msg) => Prikazi(msg, ToastTip.Warning);
        public static void Greska(string msg)  => Prikazi(msg, ToastTip.Error);
    }
}
