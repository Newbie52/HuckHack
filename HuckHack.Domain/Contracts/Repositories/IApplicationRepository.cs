using HuckHack.Domain.Entities;

using System.Collections.Generic;

namespace HuckHack.Domain.Contracts.Repositories
{
    public interface IApplicationRepository : IRepository<ApplicationToFindHackers>
    {
        IEnumerable<ApplicationToFindHackers> GetByEventId(string eventId);
    }
}
