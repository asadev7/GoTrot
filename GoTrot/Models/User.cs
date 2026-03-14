namespace GoTrot.Models
{
    public class User
    {
        public int Id { get; set; }
        public string ImePrezime { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public decimal Balance { get; set; } = 20.00m;

        /// <summary>
        /// Označava da li je korisnik administrator.
        /// Admin pristup provjerava se po ovom flagu, a ne samo po email adresi.
        /// </summary>
        public bool IsAdmin { get; set; } = false;

        // Navigacijske veze
        public ICollection<Ride> Rides { get; set; } = new List<Ride>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
