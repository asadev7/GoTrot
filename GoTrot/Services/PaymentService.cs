using GoTrot.Data;
using GoTrot.Models;

namespace GoTrot.Services
{
    /// <summary>
    /// Servisna klasa za uplatu kredita.
    /// Čuva Payment zapis u bazi uz ažuriranje User.Balance.
    /// </summary>
    public class PaymentService
    {
        private readonly AppDbContext _db;

        public PaymentService(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Validacija iznosa uplate. Vraća poruku greške ili null.
        /// </summary>
        public string? ValidirajUplatu(decimal iznos)
        {
            if (iznos <= 0)
                return "Unesite ispravan iznos veći od 0.";
            if (iznos < 1.00m)
                return "Minimalni iznos uplate je 1.00 KM.";
            if (iznos > 500.00m)
                return "Maksimalni iznos uplate je 500.00 KM.";
            return null;
        }

        /// <summary>
        /// Izvršava uplatu — ažurira Balance, kreira Payment zapis i notifikaciju.
        /// </summary>
        public void IzvrsiUplatu(User korisnik, decimal iznos)
        {
            korisnik.Balance += iznos;

            // Kreiraj Payment zapis za evidenciju
            _db.Payments.Add(new Payment
            {
                UserId = korisnik.Id,
                Iznos = iznos,
                VrijemeUplate = DateTime.Now,
                Napomena = $"Uplata kredita — korisnik {korisnik.Email}"
            });

            _db.Notifications.Add(new Notification
            {
                Poruka = $"💳 Korisnik '{korisnik.ImePrezime}' ({korisnik.Email}) uplatio {iznos:F2} KM na svoj račun. Novi saldo: {korisnik.Balance:F2} KM.",
                VrijemeKreiranja = DateTime.Now,
                Procitana = false
            });

            _db.SaveChanges();
        }
    }
}
