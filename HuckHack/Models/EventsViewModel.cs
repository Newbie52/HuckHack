using HuckHack.Domain.Entities;

using System.Collections.Generic;

namespace HuckHack.Models
{
    public class EventsViewModel
    {
        public List<Event> Items { get; set; }

        public List<string> Cities { get; set; }
    }
}
