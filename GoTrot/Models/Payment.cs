namespace GoTrot.Models
{
    /// <summary>
    /// Evidencija svake uplate kredita od strane korisnika.
    /// Navigacijska veza prema User-u.
    /// </summary>
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Iznos { get; set; }
        public DateTime VrijemeUplate { get; set; } = DateTime.Now;
        public string Napomena { get; set; } = "";

        // Navigacijska veza → User
        public User? User { get; set; }
    }
}
