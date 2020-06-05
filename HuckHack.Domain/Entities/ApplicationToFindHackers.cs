namespace HuckHack.Domain.Entities
{
    public class ApplicationToFindHackers : Entity
    {
        public string UserId { get; set; }

        public string EventId { get; set; }

        public string EventName { get; set; }

        public string TeamName { get; set; }

        public string ShortDescription { get; set; } // no more than 200 letters

        public string FullDescription { get; set; }
    }
}
