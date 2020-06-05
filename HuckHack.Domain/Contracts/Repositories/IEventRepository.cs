using HuckHack.Domain.Entities;

using System.Collections.Generic;

namespace HuckHack.Domain.Contracts.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        string GetNameByEventId(string id);

        IEnumerable<Event> GetOnlyUpcomingEvents();
    }
}
