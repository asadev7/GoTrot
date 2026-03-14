namespace GoTrot.Models
{
    public class Servis
    {
        public int Id { get; set; }
        public int ScooterId { get; set; }
        public Scooter Scooter { get; set; } = null!;
        public DateTime DatumPocetka { get; set; } = DateTime.Now;
        public DateTime? DatumZavrsetka { get; set; }
        public string Opis { get; set; } = "";
        public bool Zavrsen => DatumZavrsetka.HasValue;
    }
}
