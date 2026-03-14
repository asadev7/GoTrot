using GoTrot.Data;
using GoTrot.Models;

namespace GoTrot.Services
{
    /// <summary>
    /// Servisna klasa za upravljanje trotinetima.
    /// Izvučeno iz AdminForm i MainForm radi bolje arhitekture.
    /// </summary>
    public class ScooterService
    {
        private readonly AppDbContext _db;

        public ScooterService(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Pokreće punjenje trotineta — mijenja Status i bilježi startTime.
        /// </summary>
        public void ZapocniPunjenje(Scooter scooter)
        {
            scooter.ChargingStartTime = DateTime.Now;
            scooter.IsAvailable = false;
            scooter.Status = ScooterStatus.NaPunjenju;
            _db.SaveChanges();
        }

        /// <summary>
        /// Admin ručno stavlja trotinet van upotrebe (održavanje).
        /// </summary>
        public void StaviNaOdrzavanje(Scooter scooter)
        {
            scooter.IsAvailable = false;
            scooter.Status = ScooterStatus.NedostupanZaOdrzavanje;

            _db.Notifications.Add(new Notification
            {
                Poruka = $"🔧 Trotinet '{scooter.Model}' (ID: {scooter.Id}) stavljen van upotrebe radi održavanja. Lokacija: {scooter.Location}",
                VrijemeKreiranja = DateTime.Now,
                Procitana = false
            });

            _db.SaveChanges();
        }

        /// <summary>
        /// Admin vraća trotinet u upotrebu nakon održavanja.
        /// </summary>
        public void VratiIzOdrzavanja(Scooter scooter)
        {
            scooter.IsAvailable = true;
            scooter.Status = ScooterStatus.Dostupan;

            _db.Notifications.Add(new Notification
            {
                Poruka = $"✅ Trotinet '{scooter.Model}' (ID: {scooter.Id}) vraćen u upotrebu nakon održavanja. Lokacija: {scooter.Location}",
                VrijemeKreiranja = DateTime.Now,
                Procitana = false
            });

            _db.SaveChanges();
        }

        /// <summary>
        /// Primjenjuje offline punjenje na sve trotinete koji su na punjenju.
        /// </summary>
        public bool PrimijeniOfflinePunjenje(AppDbContext db)
        {
            var scooters = db.Scooters.ToList();
            bool changed = false;

            foreach (var s in scooters)
            {
                bool bioPunjen = s.IsCharging;
                s.ApplyOfflineCharging();

                if (bioPunjen && !s.IsCharging)
                {
                    s.Status = ScooterStatus.Dostupan;
                    changed = true;

                    db.Notifications.Add(new Notification
                    {
                        Poruka = $"✅ Trotinet '{s.Model}' potpuno napunjen (100%) i vraćen u upotrebu. Lokacija: {s.Location}",
                        VrijemeKreiranja = DateTime.Now,
                        Procitana = false
                    });
                }
            }

            if (changed) db.SaveChanges();
            return changed;
        }

        /// <summary>
        /// Popravlja Status za trotinete čiji Status nije sinhronizovan s IsAvailable.
        /// </summary>
        public void SinkronizirajStatuse(AppDbContext db)
        {
            var svi = db.Scooters.ToList();
            bool changed = false;

            foreach (var s in svi)
            {
                // Ako je IsAvailable true, ali Status nije Dostupan → popravi
                if (s.IsAvailable && s.Status != ScooterStatus.Dostupan)
                {
                    s.Status = ScooterStatus.Dostupan;
                    changed = true;
                }
                // Ako IsCharging, ali Status nije NaPunjenju → popravi
                if (s.IsCharging && s.Status != ScooterStatus.NaPunjenju)
                {
                    s.Status = ScooterStatus.NaPunjenju;
                    changed = true;
                }
            }

            if (changed) db.SaveChanges();
        }

        /// <summary>
        /// Seed trotineti pri prvom pokretanju.
        /// </summary>
        public void SeedData(AppDbContext db)
        {
            if (!db.Scooters.Any())
            {
                db.Scooters.AddRange(
                    new Scooter { Model = "Xiaomi Pro 2",     BatteryLevel = 90, Location = "Sarajevo - Centar",     IsAvailable = true, PricePerMinute = 0.25m, Status = ScooterStatus.Dostupan },
                    new Scooter { Model = "Segway Max G2",    BatteryLevel = 75, Location = "Sarajevo - Ilidža",     IsAvailable = true, PricePerMinute = 0.25m, Status = ScooterStatus.Dostupan },
                    new Scooter { Model = "Ninebot E45",      BatteryLevel = 55, Location = "Sarajevo - Baščaršija", IsAvailable = true, PricePerMinute = 0.25m, Status = ScooterStatus.Dostupan },
                    new Scooter { Model = "Xiaomi Essential", BatteryLevel = 12, Location = "Sarajevo - Vogošća",    IsAvailable = true, PricePerMinute = 0.25m, Status = ScooterStatus.Dostupan }
                );
                db.SaveChanges();
            }

            // Ispravi cijenu ako je pogrešna
            var pogresni = db.Scooters.Where(s => s.PricePerMinute != 0.25m).ToList();
            if (pogresni.Any())
            {
                foreach (var s in pogresni) s.PricePerMinute = 0.25m;
                db.SaveChanges();
            }
        }
    }
}
