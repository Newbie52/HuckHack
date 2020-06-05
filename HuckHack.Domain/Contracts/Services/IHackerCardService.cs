using System.Collections.Generic;
using HuckHack.Domain.Entities;
using HuckHack.Domain.Models;

namespace HuckHack.Domain.Contracts.Services
{
    public interface IHackerCardService
    {
        void Create(string email, HackerCard card);

        List<HackerCard> Search(HackerFilterModel filter, string eventId);
    }
}
