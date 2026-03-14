using System;
using System.Threading;
using System.Windows.Forms;
using GoTrot.Data;
using GoTrot.Forms;

namespace GoTrot
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // MORA biti prije svih ostalih poziva da ThreadException handler radi
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Ignoriši WebView2 E_ABORT greške pri izlasku
            Application.ThreadException += (s, e) =>
            {
                if (e.Exception?.HResult == unchecked((int)0x80004004) ||
                    e.Exception?.Message?.Contains("E_ABORT") == true ||
                    e.Exception?.Message?.Contains("0x80004004") == true ||
                    e.Exception?.Message?.Contains("aborted") == true)
                    return; // tiho ignoriši
                MessageBox.Show(e.Exception?.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            System.AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                if (ex?.HResult == unchecked((int)0x80004004) ||
                    ex?.Message?.Contains("E_ABORT") == true ||
                    ex?.Message?.Contains("aborted") == true)
                    return;
            };

            // Prikaži splash screen dok se baza inicijalizuje
            SplashForm splash = new SplashForm();
            splash.Show();
            Application.DoEvents();

            try
            {
                splash.SetPoruka("Primjenjujem migracije...");
                Application.DoEvents();
                MigrationHelper.RunAll();

                splash.SetPoruka("Inicijalizacija baze podataka...");
                Application.DoEvents();
                using (var db = new AppDbContext())
                {
                    db.Database.EnsureCreated();
                }

                splash.SetPoruka("Učitavanje aplikacije...");
                Application.DoEvents();
                Thread.Sleep(400); // kratka pauza da splash bude vidljiv
            }
            catch (Exception ex)
            {
                splash.Hide();
                MessageBox.Show(
                    $"Greška pri spajanju na bazu podataka!\n\n{ex.Message}\n\n" +
                    "Provjerite appsettings.json i da li je SQL Server pokrenut.",
                    "Greška konekcije", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                splash.Close();
                splash.Dispose();
            }

            // Glavna petlja aplikacije
            while (true)
            {
                bool otvoriRegistraciju = false;
                using (var landing = new LandingForm())
                {
                    if (landing.ShowDialog() != DialogResult.OK)
                        return;

                    otvoriRegistraciju = landing.OtvoriRegistraciju;
                }

                if (otvoriRegistraciju)
                {
                    var reg = new RegisterForm();
                    reg.StartPosition = FormStartPosition.CenterScreen;
                    reg.TopMost = true;
                    reg.Shown += (s, e) =>
                    {
                        reg.Activate();
                        reg.BringToFront();
                        reg.Focus();
                        System.Threading.Tasks.Task.Delay(200).ContinueWith(_ =>
                            reg.Invoke((Action)(() => { reg.TopMost = false; reg.Activate(); })));
                    };
                    Application.DoEvents(); // osiguraj da LandingForm završi zatvaranje
                    reg.ShowDialog();
                    continue;
                }

                bool odjavljen = false;
                using (var login = new LoginForm())
                {
                    login.StartPosition = FormStartPosition.CenterScreen;
                    login.TopMost = true;
                    login.Shown += (s, e) =>
                    {
                        login.Activate();
                        login.BringToFront();
                        login.Focus();
                        System.Threading.Tasks.Task.Delay(300).ContinueWith(_ =>
                            login.Invoke((Action)(() => login.TopMost = false)));
                    };
                    if (login.ShowDialog() != DialogResult.OK)
                        continue;

                    using (var main = new MainForm(login.LoggedInUser))
                    {
                        main.ShowDialog();
                        odjavljen = main.Odjavljen;
                    }
                }

                if (odjavljen)
                    continue;

                break;
            }
        }
    }
}