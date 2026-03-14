using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using GoTrot.Data;
using GoTrot.Models;
using GoTrot.Services;

namespace GoTrot.Forms
{
    public class LandingForm : Form
    {
        private WebView2 _webView;
        private bool _disposing = false;

        public bool OtvoriRegistraciju { get; private set; } = false;

        public LandingForm()
        {
            this.Text = "🛴 GoTrot";
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(900, 600);

            _webView = new WebView2();
            _webView.Dock = DockStyle.Fill;
            this.Controls.Add(_webView);

            this.FormClosing += (s, e) =>
            {
                _disposing = true;
                try { _webView.CoreWebView2?.Stop(); } catch { }
            };
            this.FormClosed += (s, e) =>
            {
                try { _webView.Dispose(); } catch { }
            };
            this.Load += async (s, e) => await InitWebView();
        }

        private async System.Threading.Tasks.Task InitWebView()
        {
            try
            {
                await _webView.EnsureCoreWebView2Async(null);
                if (_disposing) return;

                // Prima poruke iz JS via window.chrome.webview.postMessage()
                _webView.CoreWebView2.WebMessageReceived += (s, e) =>
                {
                    string msg = e.TryGetWebMessageAsString();
                    this.BeginInvoke((Action)(() =>
                    {
                        if (msg == "action=register")
                        {
                            OtvoriRegistraciju = true;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else if (msg == "action=login")
                        {
                            OtvoriRegistraciju = false;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else if (msg == "darkmode:on")
                        {
                            if (!ThemeManager.IsDarkMode) ThemeManager.Toggle();
                        }
                        else if (msg == "darkmode:off")
                        {
                            if (ThemeManager.IsDarkMode) ThemeManager.Toggle();
                        }
                    }));
                };

                _webView.CoreWebView2.DOMContentLoaded += async (s, e) =>
                {
                    try
                    {
                        if (_disposing) return;
                        bool dark = ThemeManager.IsDarkMode;
                        await _webView.CoreWebView2.ExecuteScriptAsync(
                            $"if(typeof setDarkMode==='function') setDarkMode({(dark ? "true" : "false")});");
                    }
                    catch { }
                };

                string html = GenerisiHtml();
                _webView.CoreWebView2.NavigateToString(html);
            }
            catch { }  // Ignoriši sve WebView2 greške (E_ABORT pri zatvaranju itd.)
        }

        public void StopWebView()
        {
            _disposing = true;
            try { _webView.CoreWebView2?.Stop(); } catch { }
            try { _webView.Dispose(); } catch { }
        }

        private string GenerisiHtml()
        {
            var scooters = new System.Collections.Generic.List<Scooter>();
            try
            {
                using var db = new AppDbContext();
                scooters = db.Scooters.ToList();
            }
            catch { }

            if (scooters.Count == 0)
            {
                scooters.Add(new Scooter { Model = "Xiaomi Pro 2", BatteryLevel = 90, Location = "Musala", IsAvailable = true });
                scooters.Add(new Scooter { Model = "Segway Max G2", BatteryLevel = 75, Location = "Centar", IsAvailable = true });
                scooters.Add(new Scooter { Model = "Ninebot E45", BatteryLevel = 55, Location = "Carina", IsAvailable = true });
                scooters.Add(new Scooter { Model = "Xiaomi Essential", BatteryLevel = 12, Location = "Šehovina", IsAvailable = true });
                scooters.Add(new Scooter { Model = "Segway Ninebot F40", BatteryLevel = 88, Location = "Bulevar", IsAvailable = true });
                scooters.Add(new Scooter { Model = "Razor E300", BatteryLevel = 63, Location = "Lučki most", IsAvailable = true });
            }

            var koordinate = new System.Collections.Generic.Dictionary<string, (double lat, double lng)>(
                System.StringComparer.OrdinalIgnoreCase)
            {
                ["Centar"] = (43.3438, 17.8078),
                ["Musala"] = (43.3422, 17.8115),
                ["Carina"] = (43.3475, 17.8095),
                ["Šehovina"] = (43.3403, 17.8050),
                ["Bulevar"] = (43.3460, 17.8030),
                ["Lučki most"] = (43.3395, 17.8090),
                ["Stari grad"] = (43.3370, 17.8135),
                ["Rondo"] = (43.3445, 17.7995),
                ["Bijelo Polje"] = (43.3620, 17.7950),
                ["Sjever"] = (43.3590, 17.7980),
                ["Jug"] = (43.3280, 17.8100),
            };

            var locJson = new StringBuilder("[");
            foreach (var s in scooters)
            {
                koordinate.TryGetValue(s.Location, out var c);
                double lat = c != default ? c.lat : 43.3438 + (s.Id % 5) * 0.003;
                double lng = c != default ? c.lng : 17.8078 + (s.Id % 3) * 0.004;

                bool available = s.IsAvailable
                    && s.Status != ScooterStatus.NedostupanPraznaBaterija
                    && s.Status != ScooterStatus.NedostupanZaOdrzavanje;

                locJson.Append($@"{{""lat"":{lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}," +
                               $@"""lng"":{lng.ToString(System.Globalization.CultureInfo.InvariantCulture)}," +
                               $@"""model"":""{EscapeJs(s.Model)}""," +
                               $@"""battery"":{s.BatteryLevel}," +
                               $@"""available"":{(available ? "true" : "false")}," +
                               $@"""location"":""{EscapeJs(s.Location)}""}},");
            }
            if (locJson.Length > 1) locJson.Length--;
            locJson.Append("]");

            var slides = new StringBuilder();
            var dots = new StringBuilder();
            int total = scooters.Count;

            for (int i = 0; i < total; i++)
            {
                var s = scooters[i];
                string activeClass = (i == 0) ? " active" : "";
                string dotActive = (i == 0) ? " active" : "";

                string badgeHtml = !s.IsAvailable
                    ? "<div class=\"unavailable-badge\">&#9679; Nije dostupan</div>"
                    : s.BatteryLevel < 10
                        ? "<div class=\"low-badge\">&#9679; Niska baterija</div>"
                        : "<div class=\"available-badge\">&#9679; Dostupan</div>";

                string batColor = s.BatteryLevel >= 50 ? "#0d9e8a"
                                : s.BatteryLevel >= 20 ? "#e67e22" : "#c0392b";

                slides.AppendFormat(@"
          <div class=""slide{0}"">
            <div class=""scooter-card"">
              <span class=""scooter-emoji"">&#128756;</span>
              {1}
              <div class=""scooter-name"">{2}</div>
              <div class=""scooter-meta""><div class=""meta-row"">
                <div class=""meta-col""><div class=""meta-col-inner""><span class=""meta-key"">Baterija</span><div class=""meta-val"" style=""color:{4}"">{5}%</div></div></div>
                <div class=""meta-col""><div class=""meta-col-inner""><span class=""meta-key"">Cijena</span><div class=""meta-val"">0.25 KM/min</div></div></div>
                <div class=""meta-col""><div class=""meta-col-inner""><span class=""meta-key"">Lokacija</span><div class=""meta-val"">{3}</div></div></div>
              </div></div>
            </div>
          </div>", activeClass, badgeHtml, s.Model, s.Location, batColor, s.BatteryLevel);

                dots.AppendFormat("<span class=\"dot{0}\" onclick=\"goTo({1})\"></span>", dotActive, i);
            }

            string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GoTrot_Landing.html");
            string template = File.Exists(htmlPath) ? File.ReadAllText(htmlPath) : "";

            template = ReplaceBetween(template, "<!--SLIDES_START-->", "<!--SLIDES_END-->", slides.ToString());
            template = ReplaceBetween(template, "<!--DOTS_START-->", "<!--DOTS_END-->", dots.ToString());
            template = template.Replace("var total = 6;", "var total = " + total + ";");
            template = ReplaceBetween(template, "<!--SCOOTER_LOCATIONS-->", "<!--/SCOOTER_LOCATIONS-->", locJson.ToString());

            return template;
        }

        private static string EscapeJs(string s) =>
            s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("'", "\\'");

        private string ReplaceBetween(string source, string startMarker, string endMarker, string replacement)
        {
            int startIdx = source.IndexOf(startMarker);
            int endIdx = source.IndexOf(endMarker);
            if (startIdx < 0 || endIdx < 0) return source;
            return source.Substring(0, startIdx + startMarker.Length)
                + replacement
                + source.Substring(endIdx);
        }
    }
}