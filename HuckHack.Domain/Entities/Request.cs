namespace HuckHack.Domain.Entities
{
    public class Request : Entity
    {
        public string FromEmail { get; set; }

        public string ToEmail { get; set; }
    }
}
