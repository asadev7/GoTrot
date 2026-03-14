namespace GoTrot.Models
{
    public class Scooter
    {
        public int Id { get; set; }
        public string Model { get; set; } = "";
        public int BatteryLevel { get; set; }
        public bool IsAvailable { get; set; }
        public decimal PricePerMinute { get; set; } = 0.25m;
        public string Location { get; set; } = "";
        public int? ZonaId { get; set; }
        public Zona? Zona { get; set; }

        /// <summary>
        /// Dostupan → Iznajmljen → Dostupan
        /// Dostupan → NaPunjenju → Dostupan
        /// Dostupan → Rezervisan → Dostupan | Iznajmljen
        /// Iznajmljen → NedostupanPraznaBaterija
        /// Dostupan → NedostupanZaOdrzavanje (admin)
        /// </summary>
        public ScooterStatus Status { get; set; } = ScooterStatus.Dostupan;

        public ICollection<Ride> Rides { get; set; } = new List<Ride>();
        public ICollection<Servis> Servisi { get; set; } = new List<Servis>();

        public DateTime? ChargingStartTime { get; set; }
        public bool IsCharging => ChargingStartTime.HasValue;

        public void ApplyOfflineCharging()
        {
            if (!ChargingStartTime.HasValue) return;

            double elapsedMinutes = (DateTime.Now - ChargingStartTime.Value).TotalMinutes;
            int gained = (int)(elapsedMinutes / 2.0);

            if (gained <= 0) return;

            ChargingStartTime = ChargingStartTime.Value.AddMinutes(gained * 2.0);
            BatteryLevel = Math.Min(100, BatteryLevel + gained);

            if (BatteryLevel >= 100)
            {
                BatteryLevel = 100;
                ChargingStartTime = null;
                IsAvailable = true;
                Status = ScooterStatus.Dostupan;
            }
        }

        public void SinkronizirajStatus()
        {
            IsAvailable = Status == ScooterStatus.Dostupan;
        }
    }
}
