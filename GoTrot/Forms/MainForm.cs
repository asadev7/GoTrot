using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GoTrot.Data;
using GoTrot.Models;
using GoTrot.Services;

namespace GoTrot.Forms
{
    public partial class MainForm : Form
    {
        private AppDbContext _db = new AppDbContext();
        private readonly RideService _rideService;
        private readonly ScooterService _scooterService;
        private readonly RezervacijaService _rezervacijaService;
        private User _currentUser;
        private Ride? _activeRide = null;
        private Rezervacija? _aktivnaRezervacija = null;
        private bool _upozorenje2km = false;
        private bool _upozorenje1km = false;
        private System.Windows.Forms.Timer _rezervacijaTimer = new System.Windows.Forms.Timer { Interval = 30000 };
        public bool Odjavljen { get; private set; } = false;

        public MainForm(User user)
        {
            _currentUser = user;
            _rideService = new RideService(_db);
            _scooterService = new ScooterService(_db);
            _rezervacijaService = new RezervacijaService(_db);

            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            dgvScooters.RowPrePaint += (s, e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvScooters.Rows.Count) return;
                var row = dgvScooters.Rows[e.RowIndex];
                bool dostupan = row.DataBoundItem is Scooter sc && sc.IsAvailable && sc.Status == ScooterStatus.Dostupan;
                Color boja = ThemeManager.IsDarkMode
                    ? (dostupan ? Color.FromArgb(20, 60, 80) : Color.FromArgb(70, 30, 30))
                    : (dostupan ? Color.FromArgb(213, 232, 252) : Color.FromArgb(255, 204, 204));
                Color textBoja = ThemeManager.IsDarkMode ? ThemeManager.DarkText : Color.FromArgb(33, 37, 41);
                row.DefaultCellStyle.BackColor = boja;
                row.DefaultCellStyle.SelectionBackColor = boja;
                row.DefaultCellStyle.ForeColor = textBoja;
                row.DefaultCellStyle.SelectionForeColor = textBoja;
            };

            this.Load += (s, e) =>
            {
                ThemeManager.Apply(this);
                OsveziTabelu();

                PositionButtons();

                // Klik na rezervisan trotinet — nudi otkazivanje rezervacije
                dgvScooters.CellClick += (sender, ev) =>
                {
                    if (ev.RowIndex < 0) return;
                    var scooter = dgvScooters.Rows[ev.RowIndex].DataBoundItem as Scooter;
                    if (scooter == null || scooter.Status != ScooterStatus.Rezervisan) return;

                    // Provjeri iz baze da li postoji aktivna rezervacija ovog korisnika za ovaj trotinet
                    var now = DateTime.Now;
                    var rez = _db.Rezervacije.FirstOrDefault(r =>
                        r.UserId == _currentUser.Id &&
                        r.ScooterId == scooter.Id &&
                        r.VrijemeRezervacije.AddMinutes(10) > now);

                    if (rez == null)
                    {
                        ToastNotification.Info("Ovaj trotinet je rezervisan od strane drugog korisnika.");
                        return;
                    }

                    if (PrikaziPotvrdu($"Želite li otkazati rezervaciju za '{scooter.Model}'?", "Otkaži rezervaciju"))
                    {
                        _rezervacijaService.OtkaziRezervaciju(rez);
                        _aktivnaRezervacija = null;
                        AzurirajDugmadZaRezervaciju();
                        OsveziTabelu();
                        ToastNotification.Uspjeh($"Rezervacija za '{scooter.Model}' otkazana.");
                    }
                };
            };
            this.Resize += (s, e) => PositionButtons();

            btnEndRide.ForeColor = Color.White;
            btnEndRide.UseVisualStyleBackColor = false;
            AzurirajHeader();

            _scooterService.SeedData(_db);
            _scooterService.SinkronizirajStatuse(_db);
            _rezervacijaService.OcistiIstekle();
            OsveziTabelu();
            ProvjeriAktivnuVoznju();
            ProvjeriAktivnuRezervaciju();

            btnEndRide.Paint += (s, pe) =>
            {
                pe.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(192, 57, 43)), pe.ClipRectangle);
                TextRenderer.DrawText(pe.Graphics, btnEndRide.Text, btnEndRide.Font,
                    pe.ClipRectangle, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            _rezervacijaTimer.Tick += (s, e) =>
            {
                _rezervacijaService.OcistiIstekle();
                if (_aktivnaRezervacija != null && !_aktivnaRezervacija.IsAktivna)
                {
                    _aktivnaRezervacija = null;
                    AzurirajDugmadZaRezervaciju();
                    ToastNotification.Upozorenje("Vaša rezervacija je istekla.");
                }
                OsveziTabelu();
            };
            _rezervacijaTimer.Start();
        }

        // Pozicionira sva dugmad desno u panelTop proporcionalno širini forme
        private void PositionButtons()
        {
            int h = 38;
            int gap = 10;
            int y = (panelTop.ClientSize.Height - h) / 2;
            if (y < 0) y = 6;
            int w = 135; // jednaka širina svih dugmadi

            // Postavi širinu svima
            btnMojeVoznje.Size = new Size(w, h);
            btnMojeUplate.Size = new Size(w, h);
            btnRezervisi.Size = new Size(w, h);
            btnOtkaziRez.Size = new Size(w, h);
            btnUplata.Size = new Size(w, h);

            // Pozicioniraj slijeva nadesno: Moje vožnje → Moje uplate → Rezerviši/Otkaži → Uplati kredit
            int right = panelTop.ClientSize.Width - 12;

            btnUplata.SetBounds(right - w, y, w, h);
            right -= w + gap;

            btnRezervisi.SetBounds(right - w, y, w, h);
            btnOtkaziRez.SetBounds(right - w, y, w, h);
            right -= w + gap;

            btnMojeUplate.SetBounds(right - w, y, w, h);
            right -= w + gap;

            btnMojeVoznje.SetBounds(right - w, y, w, h);

            // Odjava i Izlaz u panelBottom
            int rightB = panelBottom.ClientSize.Width - 10;
            int hB = 40;
            int yB = (panelBottom.ClientSize.Height - hB) / 2;
            if (yB < 0) yB = 6;
            btnIzlaz.SetBounds(rightB - btnIzlaz.Width, yB, btnIzlaz.Width, hB);
            rightB -= btnIzlaz.Width + gap;
            btnOdjava.SetBounds(rightB - btnOdjava.Width, yB, btnOdjava.Width, hB);
        }

        private void ProvjeriAktivnuVoznju()
        {
            var nedovrsena = _rideService.PronadiAktivnuVoznju(_currentUser.Id);
            if (nedovrsena == null) return;

            var scooter = _db.Scooters.Find(nedovrsena.ScooterId);
            if (scooter == null) return;

            _activeRide = nedovrsena;
            scooter.IsAvailable = false;
            scooter.Status = ScooterStatus.Iznajmljen;
            _db.SaveChanges();

            _upozorenje2km = false;
            _upozorenje1km = false;

            var elapsed = DateTime.Now - _activeRide.StartTime;
            lblStatus.Text = $"🔴  Vožnja u toku: {scooter.Model}  |  📍 {scooter.Location}  |  ⏱ {(int)elapsed.TotalMinutes} min";
            btnStartRide.Enabled = false;
            btnEndRide.Enabled = true;
            btnEndRide.ForeColor = Color.White;
            rideTimer.Start();

            ToastNotification.Info($"Pronađena aktivna vožnja: {scooter.Model} ({(int)elapsed.TotalMinutes} min)");
        }

        private void ProvjeriAktivnuRezervaciju()
        {
            _aktivnaRezervacija = _rezervacijaService.PronadiAktivnu(_currentUser.Id);
            AzurirajDugmadZaRezervaciju();
        }

        private void AzurirajDugmadZaRezervaciju()
        {
            btnRezervisi.Visible = _aktivnaRezervacija == null;
            btnOtkaziRez.Visible = _aktivnaRezervacija != null;
        }

        private void BtnRezervisi_Click(object? sender, EventArgs e)
        {
            if (dgvScooters.SelectedRows.Count == 0)
            {
                ToastNotification.Upozorenje("Odaberite trotinet iz liste.");
                return;
            }

            var scooter = (Scooter)dgvScooters.SelectedRows[0].DataBoundItem;
            var greska = _rezervacijaService.ValidirajRezervaciju(_currentUser, scooter);

            if (greska != null) { ToastNotification.Greska(greska); return; }

            _aktivnaRezervacija = _rezervacijaService.KreirajRezervaciju(_currentUser, scooter);
            AzurirajDugmadZaRezervaciju();
            OsveziTabelu();

            var istic = _aktivnaRezervacija.Istice.ToString("HH:mm:ss");
            ToastNotification.Uspjeh($"Rezervisan {scooter.Model} — ističe u {istic}");
        }

        private void BtnOtkaziRez_Click(object? sender, EventArgs e)
        {
            if (_aktivnaRezervacija == null) return;
            var rez = _db.Rezervacije.Find(_aktivnaRezervacija.Id);
            if (rez != null) _rezervacijaService.OtkaziRezervaciju(rez);
            _aktivnaRezervacija = null;
            AzurirajDugmadZaRezervaciju();
            OsveziTabelu();
            ToastNotification.Info("Rezervacija otkazana.");
        }

        private void AzurirajHeader()
        {
            lblWelcome.Text = $"Dobrodošli, {_currentUser.ImePrezime} 👋";
            lblBalance.Text = $"💳  Kredit: {_currentUser.Balance:F2} KM";
        }

        private void OsveziTabelu()
        {
            _db = new AppDbContext();
            _rezervacijaService.AzurirajContext(_db);
            dgvScooters.DataSource = null;
            dgvScooters.DataSource = _db.Scooters.ToList();

            var koloneZaSakriti = new[] { "Id", "PricePerMinute", "ChargingStartTime", "IsCharging", "Rides", "Servisi", "ZonaId", "Zona" };
            foreach (var k in koloneZaSakriti)
                if (dgvScooters.Columns.Contains(k))
                    dgvScooters.Columns[k].Visible = false;

            dgvScooters.Columns["Model"].HeaderText = "🛴 Model";
            dgvScooters.Columns["BatteryLevel"].HeaderText = "🔋 Baterija (%)";
            dgvScooters.Columns["IsAvailable"].HeaderText = "✅ Dostupan";
            dgvScooters.Columns["Location"].HeaderText = "📍 Lokacija";
            dgvScooters.Columns["Status"].HeaderText = "⚙️ Status";

            dgvScooters.Columns["Model"].DisplayIndex = 0;
            dgvScooters.Columns["Location"].DisplayIndex = 1;
            dgvScooters.Columns["BatteryLevel"].DisplayIndex = 2;
            dgvScooters.Columns["IsAvailable"].DisplayIndex = 3;
            dgvScooters.Columns["Status"].DisplayIndex = 4;

            var svjeziKorisnik = _db.Users.Find(_currentUser.Id);
            if (svjeziKorisnik != null)
            {
                _currentUser = svjeziKorisnik;
                lblBalance.Text = $"💳  Kredit: {_currentUser.Balance:F2} KM";
            }

            ThemeManager.Apply(this);
        }

        private void BtnStartRide_Click(object sender, EventArgs e)
        {
            if (dgvScooters.SelectedRows.Count == 0)
            {
                ToastNotification.Upozorenje("Odaberite trotinet klikom na red u tabeli.");
                return;
            }

            var scooter = (Scooter)dgvScooters.SelectedRows[0].DataBoundItem;

            if (_aktivnaRezervacija?.ScooterId == scooter.Id)
            {
                var rez = _db.Rezervacije.Find(_aktivnaRezervacija.Id);
                if (rez != null) _db.Rezervacije.Remove(rez);
                _db.SaveChanges();
                _aktivnaRezervacija = null;
                AzurirajDugmadZaRezervaciju();
            }

            var greska = _rideService.ValidirajIznajmljivanje(_currentUser, scooter);
            if (greska != null) { ToastNotification.Greska(greska); return; }

            // Svježi context da izbjegnemo EF tracking konflikt
            using var freshDb = new AppDbContext();
            _activeRide = _rideService.ZapocniVoznju(_currentUser, scooter, freshDb);

            lblStatus.Text = $"🔴  Vožnja u toku: {scooter.Model}  |  📍 {scooter.Location}";
            btnStartRide.Enabled = false;
            btnEndRide.Enabled = true;
            btnEndRide.ForeColor = Color.White;
            _upozorenje2km = false;
            _upozorenje1km = false;
            rideTimer.Start();
            OsveziTabelu();
            ToastNotification.Uspjeh($"Vožnja započeta — {scooter.Model}!");
        }

        private void BtnEndRide_Click(object sender, EventArgs e)
        {
            if (_activeRide == null) return;

            rideTimer.Stop();

            // Koristimo svježi context kako bi izbjegli EF tracking konflikt
            using var freshDb = new AppDbContext();
            var scooter = freshDb.Scooters.Find(_activeRide.ScooterId)!;
            var korisnik = freshDb.Users.Find(_currentUser.Id)!;
            var voznja = freshDb.Rides.Find(_activeRide.Id)!;

            _rideService.ZavrsiVoznju(voznja, scooter, korisnik, freshDb);

            double trajanje = (voznja.EndTime!.Value - voznja.StartTime).TotalMinutes;

            if (scooter.Status == ScooterStatus.NedostupanPraznaBaterija)
                ToastNotification.Upozorenje($"Trotinet '{scooter.Model}' ima praznu bateriju — van upotrebe.");

            ToastNotification.Uspjeh($"Vožnja završena — {trajanje:F1} min — {voznja.TotalCost:F2} KM");

            int finishedRideId = voznja.Id;
            _currentUser = korisnik;
            _activeRide = null;
            lblStatus.Text = "🟢  Spreman za vožnju. Odaberite trotinet iz liste.";
            lblTimer.Text = "";
            btnStartRide.Enabled = true;
            btnEndRide.Enabled = false;
            btnEndRide.ForeColor = Color.White;
            OsveziTabelu();

            var ratingForm = new RatingForm(finishedRideId, scooter.Model);
            ratingForm.ShowDialog(this);
        }

        private void RideTimer_Tick(object sender, EventArgs e)
        {
            if (_activeRide == null) return;

            var elapsed = DateTime.Now - _activeRide.StartTime;
            var scooter = _db.Scooters.Find(_activeRide.ScooterId);
            decimal trenutnaCijena = _rideService.IzracunajTrenutniTrosak(_activeRide, scooter?.PricePerMinute ?? 0.25m);
            lblTimer.Text = $"⏱  {elapsed.Minutes:D2}:{elapsed.Seconds:D2}  |  Cijena do sada: {trenutnaCijena:F2} KM";

            var svjeziKorisnik = _db.Users.Find(_currentUser.Id);
            decimal preostalo = svjeziKorisnik!.Balance - trenutnaCijena;

            if (preostalo <= 2.00m && preostalo > 1.00m && !_upozorenje2km)
            {
                _upozorenje2km = true;
                ToastNotification.Upozorenje("Ostalo vam je samo 2.00 KM kredita!");
            }
            else if (preostalo <= 1.00m && preostalo > 0 && !_upozorenje1km)
            {
                _upozorenje1km = true;
                ToastNotification.Greska("Kritično! Samo 1.00 KM kredita — vožnja će se automatski završiti!");
            }
            else if (preostalo <= 0)
            {
                rideTimer.Stop();
                ToastNotification.Greska("Nema više kredita — vožnja automatski završena!");
                BtnEndRide_Click(sender, e);
            }
        }

        private void BtnUplata_Click(object sender, EventArgs e)
        {
            var uplata = new UplataForm(_currentUser);
            if (uplata.ShowDialog() == DialogResult.OK)
            {
                _db = new AppDbContext();
                OsveziTabelu();
            }
        }

        private void BtnMojeVoznje_Click(object sender, EventArgs e) =>
            new MojeVoznjeForm(_currentUser).ShowDialog();

        private void BtnMojeUplate_Click(object sender, EventArgs e) =>
            new MojeUplateForm(_currentUser).ShowDialog();

        private void BtnOdjava_Click(object sender, EventArgs e)
        {
            if (PrikaziPotvrdu("Jeste li sigurni da se želite odjaviti?", "Odjava"))
            {
                rideTimer.Stop();
                _rezervacijaTimer.Stop();
                Odjavljen = true;
                this.Close();
            }
        }

        private void BtnIzlaz_Click(object sender, EventArgs e)
        {
            if (PrikaziPotvrdu("Jeste li sigurni da želite izaći iz aplikacije?", "Izlaz"))
                Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _rezervacijaTimer.Stop();
            base.OnFormClosing(e);
        }

        private bool PrikaziPotvrdu(string poruka, string naslov)
        {
            using var dlg = new Form
            {
                Text = naslov,
                Size = new Size(380, 180),
                MinimumSize = new Size(380, 180),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = ThemeManager.Panel
            };

            var lbl = new Label
            {
                Text = poruka,
                Font = new Font("Segoe UI", 10F),
                ForeColor = ThemeManager.Text,
                AutoSize = false,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Padding = new Padding(12, 12, 12, 0)
            };

            var panelDugmad = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 52,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(10, 8, 10, 8),
                BackColor = ThemeManager.Panel
            };

            var btnDa = new Button
            {
                Text = "Da",
                DialogResult = DialogResult.Yes,
                Size = new Size(100, 34),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Location = new Point(60, 11)
            };
            btnDa.FlatAppearance.BorderSize = 0;
            var btnNe = new Button
            {
                Text = "Ne",
                DialogResult = DialogResult.No,
                Size = new Size(100, 34),
                BackColor = ThemeManager.IsDarkMode ? ThemeManager.DarkCard : Color.FromArgb(200, 205, 210),
                ForeColor = ThemeManager.Text,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Location = new Point(210, 11)
            };
            btnNe.FlatAppearance.BorderSize = 0;

            panelDugmad.Controls.Add(btnNe);
            panelDugmad.Controls.Add(btnDa);
            dlg.Controls.Add(lbl);
            dlg.Controls.Add(panelDugmad);
            dlg.AcceptButton = btnDa;
            dlg.CancelButton = btnNe;
            return dlg.ShowDialog(this) == DialogResult.Yes;
        }
    }
}