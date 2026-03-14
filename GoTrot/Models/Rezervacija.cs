namespace GoTrot.Models
{
    public class Rezervacija
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int ScooterId { get; set; }
        public Scooter Scooter { get; set; } = null!;
        public DateTime VrijemeRezervacije { get; set; } = DateTime.Now;
        public DateTime Istice => VrijemeRezervacije.AddMinutes(10);
        public bool IsAktivna => DateTime.Now < Istice;
    }
}
