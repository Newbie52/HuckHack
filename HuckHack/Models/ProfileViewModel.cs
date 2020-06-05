using HuckHack.Domain.Entities;

namespace HuckHack.Models
{
    public class ProfileViewModel
    {
        public bool Owner { get; set; }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public int Age { get; set; }

        public string PortfolioLinks { get; set; }

        public string HardSkills { get; set; }

        public string SoftSkills { get; set; }

        public string HackathonsExperience { get; set; }

        public string Picture { get; set; }

        public Specialty Specialty { get; set; }

        public string VK { get; set; }

        public string TG { get; set; }

        public string Github { get; set; }

        public string Facebook { get; set; }
    }
}
