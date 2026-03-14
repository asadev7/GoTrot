using GoTrot.Data;
using GoTrot.Models;

namespace GoTrot.Services
{
    public class RezervacijaService
    {
        private AppDbContext _db;

        public RezervacijaService(AppDbContext db) => _db = db;

        public void AzurirajContext(AppDbContext db) => _db = db;

        public string? ValidirajRezervaciju(User user, Scooter scooter)
        {
            if (scooter.Status != ScooterStatus.Dostupan)
                return "Trotinet nije dostupan za rezervaciju.";

            var aktivna = _db.Rezervacije
                .FirstOrDefault(r => r.UserId == user.Id && r.VrijemeRezervacije > DateTime.Now.AddMinutes(-10));

            if (aktivna != null)
                return "Već imate aktivnu rezervaciju. Otkazite je prije nove.";

            return null;
        }

        public Rezervacija KreirajRezervaciju(User user, Scooter scooter)
        {
            scooter.Status = ScooterStatus.Rezervisan;
            scooter.IsAvailable = false;

            var rezervacija = new Rezervacija
            {
                UserId = user.Id,
                ScooterId = scooter.Id,
                VrijemeRezervacije = DateTime.Now
            };

            _db.Rezervacije.Add(rezervacija);
            _db.Notifications.Add(new Notification
            {
                Poruka = $"🔖 Korisnik '{user.ImePrezime}' rezervisao trotinet '{scooter.Model}'. Rezervacija ističe za 10 min.",
                VrijemeKreiranja = DateTime.Now,
                Procitana = false
            });

            _db.SaveChanges();
            return rezervacija;
        }

        public void OtkaziRezervaciju(Rezervacija rezervacija)
        {
            // Reload iz lokalnog _db da izbjegnemo entity tracking conflict
            // (entitet može biti dohvaćen iz drugog DbContext instance-a)
            var lokalna = _db.Rezervacije.Find(rezervacija.Id);
            if (lokalna == null) return;

            var scooter = _db.Scooters.Find(rezervacija.ScooterId);
            if (scooter != null && scooter.Status == ScooterStatus.Rezervisan)
            {
                scooter.Status = ScooterStatus.Dostupan;
                scooter.IsAvailable = true;
            }

            _db.Rezervacije.Remove(lokalna);
            _db.SaveChanges();
        }

        public void OcistiIstekle()
        {
            var istekle = _db.Rezervacije
                .Where(r => r.VrijemeRezervacije < DateTime.Now.AddMinutes(-10))
                .ToList();

            foreach (var r in istekle)
            {
                var scooter = _db.Scooters.Find(r.ScooterId);
                if (scooter != null && scooter.Status == ScooterStatus.Rezervisan)
                {
                    scooter.Status = ScooterStatus.Dostupan;
                    scooter.IsAvailable = true;
                    _db.Notifications.Add(new Notification
                    {
                        Poruka = $"⏰ Rezervacija za '{scooter.Model}' je istekla — trotinet vraćen u upotrebu.",
                        VrijemeKreiranja = DateTime.Now,
                        Procitana = false
                    });
                }
                _db.Rezervacije.Remove(r);
            }

            if (istekle.Any()) _db.SaveChanges();
        }

        public Rezervacija? PronadiAktivnu(int userId) =>
            _db.Rezervacije.FirstOrDefault(r =>
                r.UserId == userId &&
                r.VrijemeRezervacije > DateTime.Now.AddMinutes(-10));
    }
}
