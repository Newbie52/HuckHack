using System.Collections.Generic;
using HuckHack.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace HuckHack.Domain.Entities
{
    public class User : Entity
    {
        public class SocialLinks
        {
            public string VK { get; set; }

            public string TG { get; set; }

            public string Github { get; set; }

            public string Facebook { get; set; }
        }

        public User()
        {
            SendedInviteRequests = new List<string>();
            Links = new SocialLinks();
            TeamIds = new List<string>();
            HardSkills = string.Empty;
            SoftSkills = string.Empty;
        }

        public string Email { get; set; }

        public UserRole Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ShortDisplayName => $"{FirstName} {LastName[0]}.";

        public string City { get; set; }

        public string Picture { get; set; }

        public int Age { get; set; }

        public string PortfolioLinks { get; set; }

        public string HardSkills { get; set; }

        public string SoftSkills { get; set; }

        public string HackathonsExperience { get; set; }

        public Specialty Specialty { get; set; }

        public List<string> SendedInviteRequests { get; set; }

        public List<string> TeamIds { get; set; }

        public SocialLinks Links { get; set; }

        [BsonIgnore]
        public List<Team> Teams { get; set; }
    }
}
