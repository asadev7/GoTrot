using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GoTrot.Data;
using GoTrot.Models;
using GoTrot.Services;

namespace GoTrot.Forms
{
    public partial class AdminForm : Form
    {
        public bool Odjavljen { get; private set; } = false;

        private AppDbContext _db = new AppDbContext();
        private readonly ScooterService _scooterService = new ScooterService(new AppDbContext());
        private System.Windows.Forms.Timer _chargingTimer = new System.Windows.Forms.Timer();
        private readonly RezervacijaService _rezervacijaService = new RezervacijaService(new AppDbContext());

        public AdminForm()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;

            dgvManageScooters.RowPrePaint += (s, e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvManageScooters.Rows.Count) return;
                var row = dgvManageScooters.Rows[e.RowIndex];
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

            PrimijeniOfflinePunjenje();
            PopraviNedostupneTrotinete();
            UcitajPodatke();
            UcitajNotifikacije();
            AzurirajBrojNotifikacija();
            this.Load += (s, e) =>
            {
                DodajStatistikeTab();
                DodajZoneTab();
                DodajServisiTab();
                DodajAdminDugmad();
                ThemeManager.Apply(this);
            };
            _rezervacijaService.OcistiIstekle();

            _chargingTimer.Interval = 10000;
            _chargingTimer.Tick += ChargingTimer_Tick;
            _chargingTimer.Start();

            btnPostavljenaNaPunjenje.Paint += (s, pe) =>
            {
                var btn = (Button)s;
                pe.Graphics.FillRectangle(new SolidBrush(btn.BackColor), pe.ClipRectangle);
                TextRenderer.DrawText(pe.Graphics, btn.Text, btn.Font, pe.ClipRectangle, btn.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
        }

        private void PrimijeniOfflinePunjenje()
        {
            var scooters = _db.Scooters.ToList();
            bool changed = false;

            foreach (var s in scooters)
            {
                bool bioPunjen = s.IsCharging;
                s.ApplyOfflineCharging();

                if (bioPunjen && !s.IsCharging)
                {
                    s.Status = ScooterStatus.Dostupan;
                    changed = true;
                    _db.Notifications.Add(new Notification
                    {
                        Poruka = $"✅ Trotinet '{s.Model}' potpuno napunjen (100%) i vraćen u upotrebu. Lokacija: {s.Location}",
                        VrijemeKreiranja = DateTime.Now,
                        Procitana = false
                    });
                }
            }

            if (changed) _db.SaveChanges();
        }

        private void ChargingTimer_Tick(object? sender, EventArgs e)
        {
            var scooters = _db.Scooters.Where(s => s.ChargingStartTime != null).ToList();
            if (!scooters.Any()) return;

            bool changed = false;

            foreach (var s in scooters)
            {
                int stari = s.BatteryLevel;
                s.ApplyOfflineCharging();

                if (s.BatteryLevel != stari)
                {
                    changed = true;
                    if (!s.IsCharging)
                    {
                        s.Status = ScooterStatus.Dostupan;
                        _db.Notifications.Add(new Notification
                        {
                            Poruka = $"✅ Trotinet '{s.Model}' potpuno napunjen (100%) i vraćen u upotrebu. Lokacija: {s.Location}",
                            VrijemeKreiranja = DateTime.Now,
                            Procitana = false
                        });
                    }
                }
            }

            if (changed)
            {
                _db.SaveChanges();
                UcitajPodatke();
                UcitajNotifikacije();
                AzurirajBrojNotifikacija();
            }
        }

        private void AzurirajDugmeNapuni()
        {
            if (dgvManageScooters.SelectedRows.Count == 0)
            {
                btnPostavljenaNaPunjenje.Text = "🔌 Napuni trotinet";
                btnPostavljenaNaPunjenje.BackColor = Color.FromArgb(41, 128, 185);
                return;
            }

            var scooter = (Scooter)dgvManageScooters.SelectedRows[0].DataBoundItem;

            if (scooter.IsCharging)
            {
                btnPostavljenaNaPunjenje.Text = "⏹ Zaustavi punjenje";
                btnPostavljenaNaPunjenje.BackColor = Color.FromArgb(211, 84, 0);
                btnPostavljenaNaPunjenje.TextAlign = ContentAlignment.MiddleCenter;
            }
            else
            {
                btnPostavljenaNaPunjenje.Text = "🔌 Napuni trotinet";
                btnPostavljenaNaPunjenje.BackColor = Color.FromArgb(41, 128, 185);
                btnPostavljenaNaPunjenje.TextAlign = ContentAlignment.MiddleCenter;
            }
        }

        private void UcitajPodatke()
        {
            _db = new AppDbContext();

            dgvManageScooters.DataSource = null;
            dgvManageScooters.DataSource = _db.Scooters.ToList();
            dgvManageScooters.EnableHeadersVisualStyles = false;

            dgvManageScooters.Columns["Id"].Visible = false;
            dgvManageScooters.Columns["PricePerMinute"].Visible = false;
            dgvManageScooters.Columns["ChargingStartTime"].Visible = false;
            dgvManageScooters.Columns["IsCharging"].Visible = false;
            if (dgvManageScooters.Columns.Contains("Rides"))
                dgvManageScooters.Columns["Rides"].Visible = false;
            if (dgvManageScooters.Columns.Contains("ZonaId"))
                dgvManageScooters.Columns["ZonaId"].Visible = false;
            if (dgvManageScooters.Columns.Contains("Zona"))
                dgvManageScooters.Columns["Zona"].Visible = false;
            if (dgvManageScooters.Columns.Contains("Servisi"))
                dgvManageScooters.Columns["Servisi"].Visible = false;

            dgvManageScooters.Columns["Model"].HeaderText = "Model trotineta";
            dgvManageScooters.Columns["BatteryLevel"].HeaderText = "🔋 Baterija (%)";
            dgvManageScooters.Columns["IsAvailable"].HeaderText = "✅ Dostupan";
            dgvManageScooters.Columns["Location"].HeaderText = "📍 Lokacija";
            dgvManageScooters.Columns["Status"].HeaderText = "⚙️ Status";

            dgvManageScooters.Columns["Model"].DisplayIndex = 0;
            dgvManageScooters.Columns["Location"].DisplayIndex = 1;
            dgvManageScooters.Columns["BatteryLevel"].DisplayIndex = 2;
            dgvManageScooters.Columns["IsAvailable"].DisplayIndex = 3;
            dgvManageScooters.Columns["Status"].DisplayIndex = 4;



            AzurirajDugmeNapuni();

            var historija = _db.Rides
                .Where(r => r.EndTime != null)
                .AsEnumerable()
                .Select(r => new
                {
                    Korisnik = _db.Users.FirstOrDefault(u => u.Id == r.UserId)?.ImePrezime ?? "Nepoznat",
                    Trotinet = _db.Scooters.FirstOrDefault(s => s.Id == r.ScooterId)?.Model ?? "Nepoznat",
                    Pocetak = r.StartTime.ToString("dd.MM.yyyy HH:mm"),
                    Kraj = r.EndTime.HasValue ? r.EndTime.Value.ToString("dd.MM.yyyy HH:mm") : "U toku",
                    Cijena_KM = r.TotalCost.ToString("F2") + " KM"
                })
                .OrderByDescending(r => r.Pocetak)
                .ToList();

            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.IsDarkMode ? ThemeManager.DarkCard : Color.FromArgb(44, 62, 80);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvHistory.BackgroundColor = ThemeManager.Panel;
            dgvHistory.DefaultCellStyle.BackColor = ThemeManager.Panel;
            dgvHistory.DefaultCellStyle.ForeColor = ThemeManager.Text;
            dgvHistory.DefaultCellStyle.SelectionBackColor = ThemeManager.Accent;
            dgvHistory.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.IsDarkMode ? ThemeManager.DarkCard : Color.FromArgb(235, 245, 255);
            dgvHistory.AlternatingRowsDefaultCellStyle.ForeColor = ThemeManager.Text;
            dgvHistory.DataSource = historija;

            if (dgvHistory.Columns.Count > 0)
            {
                dgvHistory.Columns["Korisnik"].HeaderText = "👤 Korisnik";
                dgvHistory.Columns["Trotinet"].HeaderText = "🛴 Trotinet";
                dgvHistory.Columns["Pocetak"].HeaderText = "🕐 Početak";
                dgvHistory.Columns["Kraj"].HeaderText = "🕑 Kraj";
                dgvHistory.Columns["Cijena_KM"].HeaderText = "🪙 Cijena (KM)";
            }

            Color bojaHistorija = ThemeManager.IsDarkMode ? Color.FromArgb(20, 55, 85) : Color.FromArgb(213, 232, 252);
            foreach (DataGridViewRow row in dgvHistory.Rows)
            {
                row.DefaultCellStyle.BackColor = bojaHistorija;
                row.DefaultCellStyle.SelectionBackColor = bojaHistorija;
                row.DefaultCellStyle.ForeColor = ThemeManager.Text;
                row.DefaultCellStyle.SelectionForeColor = ThemeManager.Text;
            }
        }

        private void PopraviNedostupneTrotinete()
        {
            var prazni = _db.Scooters.Where(s => s.BatteryLevel == 0 && s.ChargingStartTime == null).ToList();
            foreach (var s in prazni)
            {
                s.IsAvailable = false;
                s.Status = ScooterStatus.NedostupanPraznaBaterija;
            }

            if (prazni.Any()) _db.SaveChanges();
        }

        private void UcitajNotifikacije()
        {
            var notifikacije = _db.Notifications
                .AsEnumerable()
                .OrderByDescending(n => n.VrijemeKreiranja)
                .Select(n => new
                {
                    n.Id,
                    Status = n.Procitana ? "✅ Pročitana" : "🔴 Nova",
                    Poruka = n.Poruka,
                    Vrijeme = n.VrijemeKreiranja.ToString("dd.MM.yyyy HH:mm")
                })
                .ToList();

            dgvNotifications.DataSource = null;
            dgvNotifications.DataSource = notifikacije;
            dgvNotifications.EnableHeadersVisualStyles = false;
            dgvNotifications.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvNotifications.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNotifications.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            if (dgvNotifications.Columns.Contains("Id"))
                dgvNotifications.Columns["Id"].Visible = false;
            if (dgvNotifications.Columns.Contains("Status"))
            {
                dgvNotifications.Columns["Status"].HeaderText = "Status";
                dgvNotifications.Columns["Status"].Width = 130;
            }
            if (dgvNotifications.Columns.Contains("Poruka"))
            {
                dgvNotifications.Columns["Poruka"].HeaderText = "📋 Poruka";
                dgvNotifications.Columns["Poruka"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvNotifications.Columns.Contains("Vrijeme"))
            {
                dgvNotifications.Columns["Vrijeme"].HeaderText = "🕐 Vrijeme";
                dgvNotifications.Columns["Vrijeme"].Width = 145;
            }

            dgvNotifications.DefaultCellStyle.BackColor = ThemeManager.Panel;
            dgvNotifications.DefaultCellStyle.ForeColor = ThemeManager.Text;
            dgvNotifications.DefaultCellStyle.SelectionBackColor = ThemeManager.Accent;
            dgvNotifications.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvNotifications.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.IsDarkMode ? ThemeManager.DarkCard : Color.FromArgb(235, 245, 255);
            dgvNotifications.AlternatingRowsDefaultCellStyle.ForeColor = ThemeManager.Text;
            dgvNotifications.BackgroundColor = ThemeManager.Panel;

            foreach (DataGridViewRow row in dgvNotifications.Rows)
            {
                bool nova = row.Cells["Status"]?.Value?.ToString()?.Contains("Nova") == true;
                Color boja = ThemeManager.IsDarkMode
                    ? (nova ? Color.FromArgb(80, 20, 20) : Color.FromArgb(25, 40, 65))
                    : (nova ? Color.FromArgb(255, 220, 220) : Color.FromArgb(235, 245, 255));
                row.DefaultCellStyle.BackColor = boja;
                row.DefaultCellStyle.SelectionBackColor = ThemeManager.Accent;
                row.DefaultCellStyle.ForeColor = ThemeManager.Text;
                row.DefaultCellStyle.SelectionForeColor = Color.White;
            }
        }

        private void DgvNotifications_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvNotifications.Rows[e.RowIndex];
            var status = row.Cells["Status"]?.Value?.ToString();
            if (status == null || !status.Contains("Nova")) return;
            if (!int.TryParse(row.Cells["Id"]?.Value?.ToString(), out int notifId)) return;

            var notif = _db.Notifications.FirstOrDefault(n => n.Id == notifId);
            if (notif == null) return;

            notif.Procitana = true;
            _db.SaveChanges();
            UcitajNotifikacije();
            AzurirajBrojNotifikacija();
        }

        private void AzurirajBrojNotifikacija()
        {
            int broj = _db.Notifications.Count(n => !n.Procitana);
            tabNotifications.Text = broj > 0 ? $"🔔 Notifikacije ({broj})" : "🔔 Notifikacije";
        }

        private void BtnOznačiProcitano_Click(object sender, EventArgs e)
        {
            var neprocitane = _db.Notifications.Where(n => !n.Procitana).ToList();
            foreach (var n in neprocitane) n.Procitana = true;
            _db.SaveChanges();
            UcitajNotifikacije();
            AzurirajBrojNotifikacija();
            ToastNotification.Uspjeh("Sve notifikacije označene kao pročitane.");
        }

        private void BtnObrišiNotifikacije_Click(object sender, EventArgs e)
        {
            if (!PrikaziPotvrdu("Jeste li sigurni da želite obrisati SVE notifikacije?\nOva akcija se ne može poništiti.", "Brisanje notifikacija"))
                return;

            _db.Notifications.RemoveRange(_db.Notifications.ToList());
            _db.SaveChanges();
            UcitajNotifikacije();
            AzurirajBrojNotifikacija();
            ToastNotification.Uspjeh("Sve notifikacije obrisane.");
        }

        private void BtnPostavljenaNaPunjenje_Click(object sender, EventArgs e)
        {
            if (dgvManageScooters.SelectedRows.Count == 0)
            {
                ToastNotification.Upozorenje("Odaberite trotinet.");
                return;
            }

            var scooter = (Scooter)dgvManageScooters.SelectedRows[0].DataBoundItem;
            var dbScooter = _db.Scooters.First(s => s.Id == scooter.Id);

            if (dbScooter.IsCharging)
            {
                dbScooter.ChargingStartTime = null;
                dbScooter.IsAvailable = true;
                dbScooter.Status = ScooterStatus.Dostupan;
                _db.SaveChanges();

                _db.Notifications.Add(new Notification
                {
                    Poruka = $"⏹ Punjenje zaustavljeno za '{dbScooter.Model}'. Trenutna baterija: {dbScooter.BatteryLevel}%. Lokacija: {dbScooter.Location}",
                    VrijemeKreiranja = DateTime.Now,
                    Procitana = false
                });
                _db.SaveChanges();

                ToastNotification.Uspjeh($"⏹ Punjenje zaustavljeno. Baterija: {dbScooter.BatteryLevel}%");
            }
            else
            {
                if (dbScooter.BatteryLevel >= 100)
                {
                    ToastNotification.Info("Baterija je već na 100%!");
                    return;
                }

                dbScooter.ChargingStartTime = DateTime.Now;
                dbScooter.IsAvailable = false;
                dbScooter.Status = ScooterStatus.NaPunjenju;
                _db.SaveChanges();

                _db.Notifications.Add(new Notification
                {
                    Poruka = $"🔌 Punjenje pokrenuto za '{dbScooter.Model}'. Početna baterija: {dbScooter.BatteryLevel}%. Lokacija: {dbScooter.Location}",
                    VrijemeKreiranja = DateTime.Now,
                    Procitana = false
                });
                _db.SaveChanges();

                ToastNotification.Info($"🔌 Punjenje pokrenuto za '{dbScooter.Model}'.");
            }

            UcitajPodatke();
            UcitajNotifikacije();
            AzurirajBrojNotifikacija();
        }

        private void BtnAddScooter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                ToastNotification.Upozorenje("Unesite naziv modela trotineta.");
                return;
            }

            int baterija = (int)numBattery.Value;

            var novi = new Scooter
            {
                Model = txtModel.Text.Trim(),
                BatteryLevel = baterija,
                Location = string.IsNullOrWhiteSpace(txtLocation.Text) ? "Nepoznato" : txtLocation.Text.Trim(),
                IsAvailable = baterija > 0,
                PricePerMinute = 0.25m,
                Status = baterija > 0 ? ScooterStatus.Dostupan : ScooterStatus.NedostupanPraznaBaterija
            };

            _db.Scooters.Add(novi);
            _db.SaveChanges();
            UcitajPodatke();

            txtModel.Clear();
            txtLocation.Clear();
            numBattery.Value = 100;

            if (baterija == 0)
                ToastNotification.Upozorenje($"Trotinet '{novi.Model}' dodan, ali je nedostupan jer ima 0% baterije!");
            else
                ToastNotification.Uspjeh($"Trotinet '{novi.Model}' uspješno dodan u sistem!");
        }

        private void BtnDeleteScooter_Click(object sender, EventArgs e)
        {
            if (dgvManageScooters.SelectedRows.Count == 0)
            {
                ToastNotification.Upozorenje("Odaberite trotinet koji želite obrisati.");
                return;
            }

            var scooter = (Scooter)dgvManageScooters.SelectedRows[0].DataBoundItem;

            if (scooter.Status == ScooterStatus.Iznajmljen)
            {
                ToastNotification.Greska("Ne možete obrisati trotinet koji je trenutno u vožnji!");
                return;
            }

            if (PrikaziPotvrdu($"Jeste li sigurni da želite obrisati trotinet:\n'{scooter.Model}'?", "Potvrda brisanja"))
            {
                var dbScooter = _db.Scooters.First(s => s.Id == scooter.Id);
                _db.Scooters.Remove(dbScooter);
                _db.SaveChanges();
                UcitajPodatke();
            }
        }

        private void BtnAdminOdjava_Click(object sender, EventArgs e)
        {
            if (PrikaziPotvrdu("Jeste li sigurni da se želite odjaviti?", "Odjava"))
            {
                Odjavljen = true;
                this.Close();
            }
        }

        private void BtnAdminZatvori_Click(object sender, EventArgs e)
        {
            if (PrikaziPotvrdu("Jeste li sigurni da želite izaći iz aplikacije?", "Izlaz"))
            {
                // Stopira sve WebView2 instance prije izlaska
                foreach (Form f in Application.OpenForms)
                {
                    if (f is LandingForm lf)
                        try { lf.StopWebView(); } catch { }
                }
                this.Close();
                Application.Exit();
            }
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void DgvManageScooters_SelectionChanged(object sender, EventArgs e)
        {
            AzurirajDugmeNapuni();
        }

        private void BtnStaviNaOdrzavanje_Click(object sender, EventArgs e)
        {
            if (dgvManageScooters.SelectedRows.Count == 0)
            {
                ToastNotification.Upozorenje("Odaberite trotinet.");
                return;
            }

            var scooter = _db.Scooters.Find((int)dgvManageScooters.SelectedRows[0].Cells["Id"].Value);
            if (scooter == null) return;

            if (scooter.Status == ScooterStatus.Iznajmljen)
            {
                ToastNotification.Greska("Trotinet je trenutno iznajmljen i ne može se staviti na održavanje.");
                return;
            }

            if (PrikaziPotvrdu($"Staviti trotinet '{scooter.Model}' van upotrebe radi održavanja?", "Potvrda"))
            {
                _scooterService.StaviNaOdrzavanje(scooter);
                using var dbServis = new AppDbContext();
                dbServis.Servisi.Add(new Servis
                {
                    ScooterId = scooter.Id,
                    DatumPocetka = DateTime.Now,
                    Opis = "Redovno održavanje"
                });
                dbServis.SaveChanges();
                _db = new AppDbContext();
                UcitajPodatke();
                var dgvS = this.Controls.Find("dgvServisi", true).FirstOrDefault() as DataGridView;
                if (dgvS != null) UcitajServise(dgvS);
                var dgvST = this.Controls.Find("dgvServisTrotineti", true).FirstOrDefault() as DataGridView;
                if (dgvST != null) UcitajServisTrotinete(dgvST);
                ToastNotification.Uspjeh($"Trotinet '{scooter.Model}' stavljen na održavanje.");
            }
        }

        private void BtnVratiIzOdrzavanja_Click(object sender, EventArgs e)
        {
            if (dgvManageScooters.SelectedRows.Count == 0)
            {
                ToastNotification.Upozorenje("Odaberite trotinet.");
                return;
            }

            var scooter = _db.Scooters.Find((int)dgvManageScooters.SelectedRows[0].Cells["Id"].Value);
            if (scooter == null) return;

            if (scooter.Status != ScooterStatus.NedostupanZaOdrzavanje)
            {
                ToastNotification.Info("Trotinet nije na održavanju.");
                return;
            }

            _scooterService.VratiIzOdrzavanja(scooter);
            _db = new AppDbContext();
            UcitajPodatke();
            ToastNotification.Uspjeh($"Trotinet '{scooter.Model}' vraćen u upotrebu.");
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (panelAddScooter == null) return;

            int panelWidth = panelAddScooter.Width;
            int dostupno = panelWidth - 460;
            int modelWidth = dostupno / 2 - 90;
            int lokacijaWidth = dostupno / 2;

            if (modelWidth < 150) modelWidth = 150;
            if (lokacijaWidth < 140) lokacijaWidth = 140;

            txtModel.Width = modelWidth;
            lblBattery.Left = txtModel.Left + txtModel.Width + 15;
            numBattery.Left = lblBattery.Left;
            lblLocation.Left = numBattery.Left + numBattery.Width + 15;
            txtLocation.Left = lblLocation.Left;
            txtLocation.Width = lokacijaWidth;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _chargingTimer.Stop();
            base.OnFormClosing(e);
        }

        private void DodajAdminDugmad()
        {
            // Dark mode dugme uklonjeno — koristi se iz Landing page
        }

        private void DodajStatistikeTab()
        {
            foreach (TabPage tp in tabControl.TabPages)
                if (tp.Name == "tabStatistike") return;

            var tabStats = new TabPage
            {
                Text = "📊  Statistike",
                BackColor = Color.FromArgb(245, 248, 250),
                Name = "tabStatistike"
            };

            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 110,
                Padding = new Padding(20, 16, 20, 0),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            var lblTop = new Label
            {
                Text = "Top trotineti po broju vožnji",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(20, 130)
            };

            var dgvTop = new DataGridView
            {
                Name = "dgvStatsTop",
                Location = new Point(20, 158),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9F),
                Size = new Size(200, 200)
            };
            dgvTop.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvTop.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvTop.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTop.EnableHeadersVisualStyles = false;

            tabStats.Controls.Add(flowPanel);
            tabStats.Controls.Add(lblTop);
            tabStats.Controls.Add(dgvTop);
            tabControl.TabPages.Add(tabStats);

            void AzurirajVelicinu()
            {
                int margin = 20;
                dgvTop.Width = tabStats.ClientSize.Width - (margin * 2);
                dgvTop.Height = tabStats.ClientSize.Height - dgvTop.Top - margin;
            }

            tabControl.Selected += (s, e) =>
            {
                if (e.TabPage?.Name == "tabStatistike") AzurirajVelicinu();
            };

            tabStats.Resize += (s, e) => AzurirajVelicinu();

            UcitajStatistike(flowPanel, dgvTop);
        }

        private void DodajZoneTab()
        {
            foreach (TabPage tp in tabControl.TabPages)
                if (tp.Name == "tabZone") return;

            var tab = new TabPage { Text = "🗺️  Zone", Name = "tabZone", BackColor = Color.FromArgb(245, 248, 250) };

            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 9.5F),
                Name = "dgvZone"
            };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 245, 255);

            var panelDodaj = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.FromArgb(245, 248, 250) };

            var txtNaziv = new TextBox { Location = new Point(20, 16), Size = new Size(160, 28), PlaceholderText = "Naziv zone", Font = new Font("Segoe UI", 9.5F) };
            var txtOpis = new TextBox { Location = new Point(196, 16), Size = new Size(300, 28), PlaceholderText = "Opis zone", Font = new Font("Segoe UI", 9.5F) };
            var btnDodaj = new Button
            {
                Text = "➕ Dodaj zonu",
                Location = new Point(512, 14),
                Size = new Size(130, 32),
                BackColor = Color.FromArgb(13, 158, 138),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDodaj.FlatAppearance.BorderSize = 0;
            btnDodaj.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtNaziv.Text)) { ToastNotification.Upozorenje("Unesite naziv zone."); return; }
                using var db = new AppDbContext();
                db.Zone.Add(new Zona { Naziv = txtNaziv.Text.Trim(), Opis = txtOpis.Text.Trim() });
                db.SaveChanges();
                txtNaziv.Clear(); txtOpis.Clear();
                UcitajZone(dgv);
                ToastNotification.Uspjeh("Zona dodana.");
            };

            panelDodaj.Controls.AddRange(new Control[] { txtNaziv, txtOpis, btnDodaj });
            tab.Controls.Add(dgv);
            tab.Controls.Add(panelDodaj);
            tabControl.TabPages.Add(tab);
            UcitajZone(dgv);
        }

        private void UcitajZone(DataGridView dgv)
        {
            using var db = new AppDbContext();
            var zone = db.Zone.Select(z => new
            {
                z.Id,
                z.Naziv,
                z.Opis,
                BrojTrotineta = db.Scooters.Count(s => s.ZonaId == z.Id)
            }).ToList();
            dgv.DataSource = zone;
            if (dgv.Columns.Contains("Id")) dgv.Columns["Id"].Visible = false;
            if (dgv.Columns.Contains("Naziv")) dgv.Columns["Naziv"].HeaderText = "🗺️ Naziv";
            if (dgv.Columns.Contains("Opis")) dgv.Columns["Opis"].HeaderText = "📝 Opis";
            if (dgv.Columns.Contains("BrojTrotineta")) dgv.Columns["BrojTrotineta"].HeaderText = "🛴 Trotineta";

            Color bojaZone = ThemeManager.IsDarkMode ? Color.FromArgb(20, 55, 85) : Color.FromArgb(213, 232, 252);
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.DefaultCellStyle.BackColor = bojaZone;
                row.DefaultCellStyle.SelectionBackColor = bojaZone;
                row.DefaultCellStyle.ForeColor = ThemeManager.Text;
                row.DefaultCellStyle.SelectionForeColor = ThemeManager.Text;
            }
        }

        private void DodajServisiTab()
        {
            foreach (TabPage tp in tabControl.TabPages)
                if (tp.Name == "tabServisi") return;

            var tab = new TabPage { Text = "🔧  Servisi", Name = "tabServisi", BackColor = Color.FromArgb(245, 248, 250) };

            // TableLayoutPanel umjesto SplitContainer — bez InvalidOperationException
            var tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.FromArgb(245, 248, 250)
            };
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var splitPanel1 = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(245, 248, 250) };
            var splitPanel2 = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(245, 248, 250) };
            tableLayout.Controls.Add(splitPanel1, 0, 0);
            tableLayout.Controls.Add(splitPanel2, 1, 0);
            tab.Controls.Add(tableLayout);

            // ---- LIJEVO: dostupni trotineti ----
            var lblTrot = new Label
            {
                Text = "🛴  Odaberi trotinet za servis:",
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Padding = new Padding(4, 6, 0, 0)
            };

            var dgvTrotineti = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                Font = new Font("Segoe UI", 9.5F),
                Name = "dgvServisTrotineti"
            };
            dgvTrotineti.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvTrotineti.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTrotineti.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvTrotineti.EnableHeadersVisualStyles = false;
            dgvTrotineti.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 245, 255);

            var panelAkcije = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(245, 248, 250)
            };

            var lblOpis = new Label
            {
                Text = "Opis:",
                Location = new Point(8, 14),
                Size = new Size(38, 22),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(33, 37, 41)
            };
            var txtOpis = new TextBox
            {
                Location = new Point(50, 11),
                Size = new Size(160, 26),
                Font = new Font("Segoe UI", 9F),
                PlaceholderText = "npr. Redovni pregled..."
            };
            var btnPosalji = new Button
            {
                Text = "🔧 Pošalji na servis",
                Location = new Point(218, 10),
                Size = new Size(155, 28),
                BackColor = Color.FromArgb(211, 84, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnPosalji.FlatAppearance.BorderSize = 0;
            btnPosalji.Click += (s, e) =>
            {
                if (dgvTrotineti.SelectedRows.Count == 0)
                {
                    ToastNotification.Upozorenje("Odaberite trotinet iz liste.");
                    return;
                }
                var row = dgvTrotineti.SelectedRows[0];
                int scooterId = (int)row.Cells["Id"].Value;
                string statusStr = row.Cells["Status"].Value?.ToString() ?? "";
                if (statusStr == "Iznajmljen")
                {
                    ToastNotification.Greska("Trotinet je trenutno iznajmljen — ne može se staviti na servis.");
                    return;
                }
                using var db = new AppDbContext();
                var sc = db.Scooters.Find(scooterId);
                if (sc == null) return;
                sc.Status = ScooterStatus.NedostupanZaOdrzavanje;
                sc.IsAvailable = false;
                string opisTekst = string.IsNullOrWhiteSpace(txtOpis.Text) ? "Servis" : txtOpis.Text.Trim();
                db.Servisi.Add(new Servis { ScooterId = sc.Id, DatumPocetka = DateTime.Now, Opis = opisTekst });
                db.Notifications.Add(new Notification
                {
                    Poruka = $"🔧 Trotinet '{sc.Model}' poslan na servis. Razlog: {opisTekst}",
                    VrijemeKreiranja = DateTime.Now,
                    Procitana = false
                });
                db.SaveChanges();
                txtOpis.Text = "";
                UcitajServisTrotinete(dgvTrotineti);
                var dgvS = tab.Controls.Find("dgvServisi", true).FirstOrDefault() as DataGridView;
                if (dgvS != null) UcitajServise(dgvS);
                UcitajPodatke();
                ToastNotification.Uspjeh($"Trotinet '{sc.Model}' poslan na servis.");
            };

            panelAkcije.Controls.Add(lblOpis);
            panelAkcije.Controls.Add(txtOpis);
            panelAkcije.Controls.Add(btnPosalji);

            splitPanel1.Controls.Add(dgvTrotineti);
            splitPanel1.Controls.Add(lblTrot);
            splitPanel1.Controls.Add(panelAkcije);

            // ---- DESNO: historija servisa ----
            var lblHist = new Label
            {
                Text = "📋  Historija servisa:",
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Padding = new Padding(4, 6, 0, 0)
            };

            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 9.5F),
                Name = "dgvServisi"
            };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 245, 255);

            var panelZavrsi = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(245, 248, 250)
            };

            var btnZavrsi = new Button
            {
                Text = "✅ Završi servis",
                Location = new Point(8, 10),
                Size = new Size(150, 28),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnZavrsi.FlatAppearance.BorderSize = 0;
            btnZavrsi.Click += (s, e) =>
            {
                if (dgv.SelectedRows.Count == 0) { ToastNotification.Upozorenje("Odaberite servis iz liste."); return; }
                var id = (int)dgv.SelectedRows[0].Cells["Id"].Value;
                using var db = new AppDbContext();
                var servis = db.Servisi.Find(id);
                if (servis == null || servis.DatumZavrsetka.HasValue) { ToastNotification.Info("Servis je već završen."); return; }
                servis.DatumZavrsetka = DateTime.Now;
                var sc = db.Scooters.Find(servis.ScooterId);
                if (sc != null && sc.Status == ScooterStatus.NedostupanZaOdrzavanje)
                {
                    sc.Status = ScooterStatus.Dostupan;
                    sc.IsAvailable = true;
                }
                db.SaveChanges();
                UcitajServise(dgv);
                UcitajServisTrotinete(dgvTrotineti);
                UcitajPodatke();
                ToastNotification.Uspjeh("Servis završen — trotinet vraćen u upotrebu.");
            };

            panelZavrsi.Controls.Add(btnZavrsi);
            splitPanel2.Controls.Add(dgv);
            splitPanel2.Controls.Add(lblHist);
            splitPanel2.Controls.Add(panelZavrsi);

            tabControl.TabPages.Add(tab);

            UcitajServisTrotinete(dgvTrotineti);
            UcitajServise(dgv);
        }

        private void UcitajServisTrotinete(DataGridView dgv)
        {
            using var db = new AppDbContext();
            var lista = db.Scooters.ToList().Select(s => new
            {
                s.Id,
                s.Model,
                Lokacija = s.Location,
                Baterija = s.BatteryLevel + "%",
                Status = s.Status.ToString()
            }).OrderBy(s => s.Status).ToList();

            dgv.DataSource = lista;
            if (dgv.Columns.Contains("Id")) dgv.Columns["Id"].Visible = false;
            if (dgv.Columns.Contains("Model")) dgv.Columns["Model"].HeaderText = "🛴 Model";
            if (dgv.Columns.Contains("Lokacija")) dgv.Columns["Lokacija"].HeaderText = "📍 Lokacija";
            if (dgv.Columns.Contains("Baterija")) dgv.Columns["Baterija"].HeaderText = "🔋 Baterija";
            if (dgv.Columns.Contains("Status")) dgv.Columns["Status"].HeaderText = "⚙️ Status";

            foreach (DataGridViewRow row in dgv.Rows)
            {
                var status = row.Cells["Status"]?.Value?.ToString() ?? "";
                Color boja;
                if (ThemeManager.IsDarkMode)
                {
                    boja = status == "Dostupan"        ? Color.FromArgb(20, 65, 35)
                         : status == "NaPunjenju"      ? Color.FromArgb(70, 55, 10)
                         : status == "Iznajmljen" || status == "Rezervisan"
                                                       ? Color.FromArgb(20, 55, 85)
                                                       : Color.FromArgb(75, 25, 25);
                }
                else
                {
                    boja = status == "Dostupan"        ? Color.FromArgb(213, 245, 213)
                         : status == "NaPunjenju"      ? Color.FromArgb(255, 243, 205)
                         : status == "Iznajmljen" || status == "Rezervisan"
                                                       ? Color.FromArgb(213, 232, 252)
                                                       : Color.FromArgb(255, 204, 204);
                }
                row.DefaultCellStyle.BackColor = boja;
                row.DefaultCellStyle.SelectionBackColor = boja;
                row.DefaultCellStyle.ForeColor = ThemeManager.Text;
                row.DefaultCellStyle.SelectionForeColor = ThemeManager.Text;
            }
        }

        private void UcitajServise(DataGridView dgv)
        {
            using var db = new AppDbContext();
            var servisi = db.Servisi
                .AsEnumerable()
                .Select(s => new
                {
                    s.Id,
                    Trotinet = db.Scooters.FirstOrDefault(sc => sc.Id == s.ScooterId)?.Model ?? "?",
                    Pocetak = s.DatumPocetka.ToString("dd.MM.yyyy HH:mm"),
                    Zavrsetak = s.DatumZavrsetka.HasValue ? s.DatumZavrsetka.Value.ToString("dd.MM.yyyy HH:mm") : "U toku",
                    s.Opis,
                    Status = s.DatumZavrsetka.HasValue ? "✅ Završen" : "🔧 U toku"
                })
                .OrderByDescending(s => s.Pocetak)
                .ToList();

            dgv.DataSource = servisi;
            if (dgv.Columns.Contains("Id")) dgv.Columns["Id"].Visible = false;
            if (dgv.Columns.Contains("Trotinet")) dgv.Columns["Trotinet"].HeaderText = "🛴 Trotinet";
            if (dgv.Columns.Contains("Pocetak")) dgv.Columns["Pocetak"].HeaderText = "📅 Početak";
            if (dgv.Columns.Contains("Zavrsetak")) dgv.Columns["Zavrsetak"].HeaderText = "📅 Završetak";
            if (dgv.Columns.Contains("Opis")) dgv.Columns["Opis"].HeaderText = "📝 Opis";
            if (dgv.Columns.Contains("Status")) dgv.Columns["Status"].HeaderText = "⚙️ Status";

            foreach (DataGridViewRow row in dgv.Rows)
            {
                bool zavrsen = row.Cells["Status"]?.Value?.ToString()?.Contains("Završen") == true;
                Color boja = zavrsen ? Color.FromArgb(213, 232, 252) : Color.FromArgb(255, 204, 204);
                row.DefaultCellStyle.BackColor = boja;
                row.DefaultCellStyle.SelectionBackColor = boja;
                row.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
                row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 37, 41);
            }
        }

        private void UcitajStatistike(FlowLayoutPanel flowPanel, DataGridView dgvTop)
        {
            using var db = new AppDbContext();

            var zavrseneVoznje = db.Rides.Where(r => r.EndTime != null).ToList();
            int ukupnoVoznji = zavrseneVoznje.Count;
            decimal ukupanPrihod = zavrseneVoznje.Sum(r => r.TotalCost);
            double prosjecnoTrajanje = ukupnoVoznji > 0
                ? zavrseneVoznje.Average(r => (r.EndTime!.Value - r.StartTime).TotalMinutes)
                : 0;
            int aktivniKorisnici = db.Users.Count(u => !u.IsAdmin);
            int ukupnoTrotineta = db.Scooters.Count();
            int dostupnih = db.Scooters.Count(s => s.IsAvailable);

            var kartice = new[]
            {
                ("Ukupno vožnji",     ukupnoVoznji.ToString(),              Color.FromArgb(52, 152, 219)),
                ("Ukupan prihod",     $"{ukupanPrihod:F2} KM",              Color.FromArgb(39, 174, 96)),
                ("Prosjek trajanja",  $"{prosjecnoTrajanje:F1} min",         Color.FromArgb(155, 89, 182)),
                ("Aktivni korisnici", aktivniKorisnici.ToString(),           Color.FromArgb(231, 76, 60)),
                ("Dostupno / ukupno", $"{dostupnih} / {ukupnoTrotineta}",    Color.FromArgb(243, 156, 18))
            };

            flowPanel.Controls.Clear();
            foreach (var (naslov, vrijednost, boja) in kartice)
            {
                var kartica = new Panel
                {
                    Size = new Size(160, 72),
                    BackColor = Color.White,
                    Margin = new Padding(0, 0, 12, 0),
                    Padding = new Padding(12)
                };
                var traka = new Panel { BackColor = boja, Size = new Size(4, 72), Location = new Point(0, 0) };
                var lblVal = new Label { Text = vrijednost, Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = boja, AutoSize = true, Location = new Point(14, 8) };
                var lblNas = new Label { Text = naslov, Font = new Font("Segoe UI", 8F), ForeColor = Color.FromArgb(120, 120, 130), AutoSize = true, Location = new Point(14, 46) };
                kartica.Controls.Add(traka);
                kartica.Controls.Add(lblVal);
                kartica.Controls.Add(lblNas);
                flowPanel.Controls.Add(kartica);
            }

            var topScooters = zavrseneVoznje
                .GroupBy(r => r.ScooterId)
                .Select(g =>
                {
                    var scooter = db.Scooters.FirstOrDefault(s => s.Id == g.Key);
                    return new
                    {
                        Model = scooter?.Model ?? "Nepoznat",
                        Lokacija = scooter?.Location ?? "-",
                        Broj_Voznji = g.Count(),
                        Ukupan_Prihod_KM = g.Sum(r => r.TotalCost).ToString("F2") + " KM",
                        Prosjek_min = $"{g.Average(r => (r.EndTime!.Value - r.StartTime).TotalMinutes):F1} min"
                    };
                })
                .OrderByDescending(x => x.Broj_Voznji)
                .ToList();

            dgvTop.DataSource = topScooters;

            if (dgvTop.Columns.Count > 0)
            {
                dgvTop.Columns["Model"].HeaderText = "🛴 Model";
                dgvTop.Columns["Lokacija"].HeaderText = "📍 Lokacija";
                dgvTop.Columns["Broj_Voznji"].HeaderText = "🔢 Vožnji";
                dgvTop.Columns["Ukupan_Prihod_KM"].HeaderText = "💰 Prihod";
                dgvTop.Columns["Prosjek_min"].HeaderText = "⏱ Prosjek";
                dgvTop.Columns["Model"].FillWeight = 25;
                dgvTop.Columns["Lokacija"].FillWeight = 25;
                dgvTop.Columns["Broj_Voznji"].FillWeight = 15;
                dgvTop.Columns["Ukupan_Prihod_KM"].FillWeight = 18;
                dgvTop.Columns["Prosjek_min"].FillWeight = 17;
            }

            Color bojaStats = Color.FromArgb(213, 232, 252);
            foreach (DataGridViewRow row in dgvTop.Rows)
            {
                row.DefaultCellStyle.BackColor = bojaStats;
                row.DefaultCellStyle.SelectionBackColor = bojaStats;
                row.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
                row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 37, 41);
            }
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
                BackColor = Color.White
            };

            var lbl = new Label
            {
                Text = poruka,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(33, 37, 41),
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
                BackColor = Color.White
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
                BackColor = Color.FromArgb(200, 205, 210),
                ForeColor = Color.FromArgb(33, 37, 41),
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