using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace HuckHack.Domain.Entities
{
    public class Team : Entity
    {
        public Team()
        {
            Users = new List<User>();
            UserIds = new List<string>();
        }

        // LeaderId
        public string UserId { get; set; }

        public string Name { get; set; }

        public int MembersCount { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public string Requirements { get; set; }

        public string EventId { get; set; }

        public string EventName { get; set; }

        public List<string> UserIds { get; set; }

        [BsonIgnore]
        public List<User> Users { get; set; }
    }
}
