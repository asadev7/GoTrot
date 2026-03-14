namespace GoTrot.Models
{
    public class Zona
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = "";
        public string Opis { get; set; } = "";
        public ICollection<Scooter> Scooters { get; set; } = new List<Scooter>();
    }
}
