namespace GoTrot.Models
{
    public class RatingVoznje
    {
        public int Id { get; set; }
        public int RideId { get; set; }
        public Ride Ride { get; set; } = null!;
        public int Ocjena { get; set; } // 1-5
        public string Komentar { get; set; } = "";
        public DateTime VrijemeOcjene { get; set; } = DateTime.Now;
    }
}
