using MongoDB.Bson.Serialization.Attributes;

namespace HuckHack.Domain.Entities
{
    public class HackerCard : Entity
    {
        public HackerCard()
        {
            Skills = string.Empty;
        }

        public string UserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Picture { get; set; }

        public string City { get; set; }

        public Specialty Specialty { get; set; }

        public string Description { get; set; }

        public string EventId { get; set; }

        public string EventName { get; set; }

        public string Skills { get; set; }

        [BsonIgnore]
        public string DisplayName => $"{FirstName} {LastName[0]}.";

        [BsonIgnore]
        public Event Event { get; set; }
    }
}
