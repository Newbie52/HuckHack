using MongoDB.Bson.Serialization.Attributes;

namespace HuckHack.Domain.Entities
{
    public class TeamCard : Entity
    {
        public string EventId { get; set; }

        public string TeamId { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public int MembersCount { get; set; }

        public string Description { get; set; }

        public string Requirements { get; set; }

        public string HackathonCoverLetter { get; set; }

        [BsonIgnore]
        public Event Event { get; set; }
    }
}
