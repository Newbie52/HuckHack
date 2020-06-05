using System;

namespace HuckHack.Domain.Entities
{
    public class Event : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActual => EndDate > DateTime.UtcNow;

        public string City { get; set; }

        public string Link { get; set; }
    }
}
