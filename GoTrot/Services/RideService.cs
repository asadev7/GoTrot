using GoTrot.Data;
using GoTrot.Models;

namespace GoTrot.Services
{
    /// <summary>
    /// Servisna klasa koja enkapsulira poslovnu logiku vezanu za vožnje.
    /// Izvučeno iz MainForm radi bolje arhitekture i bogatijeg Dijagrama Klasa.
    /// </summary>
    public class RideService
    {
        private readonly AppDbContext _db;

        public RideService(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Provjera može li korisnik iznajmiti trotinet.
        /// Vraća poruku greške ili null ako je sve OK.
        /// </summary>
        public string? ValidirajIznajmljivanje(User korisnik, Scooter scooter)
        {
            if (korisnik.Balance < 2.00m)
                return "Nemate dovoljno kredita!\nMinimalan iznos za vožnju je 2.00 KM.";

            if (!scooter.IsAvailable)
                return "Ovaj trotinet je trenutno zauzet.\nOdaberite drugi trotinet.";

            if (scooter.Status == ScooterStatus.NedostupanZaOdrzavanje)
                return "Ovaj trotinet je van upotrebe (održavanje).\nOdaberite drugi trotinet.";

            if (scooter.BatteryLevel < 10)
                return $"Baterija trotineta je preslaba ({scooter.BatteryLevel}%).\nOdaberite trotinet sa više baterije.";

            return null;
        }

        /// <summary>
        /// Pokreće novu vožnju — kreira Ride, mijenja Status trotineta, dodaje notifikaciju.
        /// </summary>
        public Ride ZapocniVoznju(User korisnik, Scooter scooter, AppDbContext? externalDb = null)
        {
            var db = externalDb ?? _db;

            // Reload iz baze da izbjegnemo tracking conflict
            var dbScooter = db.Scooters.Find(scooter.Id) ?? scooter;
            dbScooter.IsAvailable = false;
            dbScooter.Status = ScooterStatus.Iznajmljen;

            var novnaVoznja = new Ride
            {
                UserId = korisnik.Id,
                ScooterId = dbScooter.Id,
                StartTime = DateTime.Now
            };

            db.Rides.Add(novnaVoznja);

            db.Notifications.Add(new Notification
            {
                Poruka = $"🛴 Korisnik '{korisnik.ImePrezime}' ({korisnik.Email}) iznajmio trotinet '{dbScooter.Model}' u {DateTime.Now:HH:mm}. Lokacija: {dbScooter.Location}",
                VrijemeKreiranja = DateTime.Now,
                Procitana = false
            });

            db.SaveChanges();
            return novnaVoznja;
        }

        /// <summary>
        /// Završava vožnju — računa cijenu, oduzima kredit, ažurira bateriju i Status.
        /// </summary>
        public void ZavrsiVoznju(Ride voznja, Scooter scooter, User korisnik, AppDbContext? externalDb = null)
        {
            var db = externalDb ?? _db;

            voznja.EndTime = DateTime.Now;
            double trajanje = (voznja.EndTime.Value - voznja.StartTime).TotalMinutes;
            if (trajanje < 0.1) trajanje = 0.1;

            voznja.TotalCost = (decimal)Math.Round(trajanje, 2) * scooter.PricePerMinute;

            // Potrošnja baterije: 1% na svakih 3 minute
            int potrosnjaBaterije = (int)Math.Floor(trajanje / 3);
            scooter.BatteryLevel = Math.Max(0, scooter.BatteryLevel - potrosnjaBaterije);

            // Oduzmi kredit
            korisnik.Balance -= voznja.TotalCost;
            if (korisnik.Balance < 0) korisnik.Balance = 0;

            // Postavi status trotineta
            if (scooter.BatteryLevel == 0)
            {
                scooter.IsAvailable = false;
                scooter.Status = ScooterStatus.NedostupanPraznaBaterija;

                db.Notifications.Add(new Notification
                {
                    Poruka = $"🔴 Trotinet '{scooter.Model}' (ID: {scooter.Id}) ima praznu bateriju (0%) i potrebno je punjenje! Lokacija: {scooter.Location}",
                    VrijemeKreiranja = DateTime.Now,
                    Procitana = false
                });
            }
            else
            {
                scooter.IsAvailable = true;
                scooter.Status = ScooterStatus.Dostupan;
            }

            db.Notifications.Add(new Notification
            {
                Poruka = $"✅ Korisnik '{korisnik.ImePrezime}' ({korisnik.Email}) završio vožnju trotineta '{scooter.Model}'. Trajanje: {trajanje:F1} min | Cijena: {voznja.TotalCost:F2} KM | Preostali kredit: {korisnik.Balance:F2} KM",
                VrijemeKreiranja = DateTime.Now,
                Procitana = false
            });

            db.SaveChanges();
        }

        /// <summary>
        /// Traži nedovršenu vožnju korisnika (crash recovery).
        /// </summary>
        public Ride? PronadiAktivnuVoznju(int userId)
        {
            return _db.Rides
                .Where(r => r.UserId == userId && r.EndTime == null)
                .OrderByDescending(r => r.StartTime)
                .FirstOrDefault();
        }

        /// <summary>
        /// Izračunava trenutnu cijenu aktivne vožnje.
        /// </summary>
        public decimal IzracunajTrenutniTrosak(Ride voznja, decimal pricePerMinute)
        {
            double elapsed = (DateTime.Now - voznja.StartTime).TotalMinutes;
            return (decimal)elapsed * pricePerMinute;
        }
    }
}
