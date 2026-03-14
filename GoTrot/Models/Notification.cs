using System;

namespace GoTrot.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Poruka { get; set; } = "";
        public DateTime VrijemeKreiranja { get; set; } = DateTime.Now;
        public bool Procitana { get; set; } = false;
    }
}
