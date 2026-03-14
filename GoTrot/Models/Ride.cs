namespace GoTrot.Models
{
    public class Ride
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int ScooterId { get; set; }
        public Scooter Scooter { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal TotalCost { get; set; }
        public RatingVoznje? Rating { get; set; }
    }
}
