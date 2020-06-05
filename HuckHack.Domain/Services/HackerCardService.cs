using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Contracts.Services;
using HuckHack.Domain.Entities;
using HuckHack.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace HuckHack.Domain.Services
{
    public class HackerCardService : IHackerCardService
    {
        private readonly IHackerCardRepository _hackerCardRepository;
        private readonly IEventRepository _eventRepository;

        public HackerCardService(
            IHackerCardRepository hackerCardRepository,
            IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            _hackerCardRepository = hackerCardRepository;
        }

        public void Create(string email, HackerCard card)
        {
            card.EventName = _eventRepository.GetNameByEventId(card.EventId);
            card.Email = email;
            var existCards = _hackerCardRepository.Get(i => i.EventId, card.EventId);
            if (!existCards.Any(i => i.UserId == card.UserId))
                _hackerCardRepository.Add(card);
        }

        public List<HackerCard> Search(HackerFilterModel filter, string eventId)
        {
            var filteredHackers = new List<HackerCard>();
            var hackers = _hackerCardRepository.Get(i => i.EventId, eventId).ToList();

            filteredHackers.AddRange(FilterBySearchValue(filter, hackers));

            if (!string.IsNullOrEmpty(filter.SearchValue))
                hackers = filteredHackers;

            filteredHackers.AddRange(FilterBySpecialty(filter, hackers));
            filteredHackers.AddRange(FilterBySkills(filter, hackers));

            var distinct = filteredHackers.GroupBy(i => i.Id).Select(group => group.First()).ToList();

            return distinct;
        }

        private IEnumerable<HackerCard> FilterBySearchValue(HackerFilterModel filter, List<HackerCard> hackers)
        {
            if (!string.IsNullOrEmpty(filter.SearchValue))
                return hackers.Where(i => $"{i.FirstName} {i.LastName} {i.Specialty}".Contains(filter.SearchValue));

            return new List<HackerCard>();
        }

        private IEnumerable<HackerCard> FilterBySpecialty(HackerFilterModel filter, List<HackerCard> hackers)
        {
            var filteredHackers = new List<HackerCard>();

            if (filter.Frontend)
                filteredHackers.AddRange(hackers.Where(i => i.Specialty == Specialty.Frontend));

            if (filter.Backend)
                filteredHackers.AddRange(hackers.Where(i => i.Specialty == Specialty.Backend));

            if (filter.Analyst)
                filteredHackers.AddRange(hackers.Where(i => i.Specialty == Specialty.Analyst));

            if (filter.Designer)
                filteredHackers.AddRange(hackers.Where(i => i.Specialty == Specialty.Designer));

            if (filter.Mobile)
                filteredHackers.AddRange(hackers.Where(i => i.Specialty == Specialty.Mobile));

            if (filter.TeamLead)
                filteredHackers.AddRange(hackers.Where(i => i.Specialty == Specialty.TeamLead));

            if (filter.Speaker)
                filteredHackers.AddRange(hackers.Where(i => i.Specialty == Specialty.Speaker));

            if (filter.Other)
                filteredHackers.AddRange(hackers.Where(i => i.Specialty == Specialty.Other));

            if (filter.Backend)
                filteredHackers.AddRange(hackers.Where(i => i.Specialty == Specialty.Backend));

            return filteredHackers;
        }

        private IEnumerable<HackerCard> FilterBySkills(HackerFilterModel filter, List<HackerCard> hackers)
        {
            var filteredHackers = new List<HackerCard>();

            if (filter.CSharp)
                filteredHackers.AddRange(hackers.Where(i => i.Skills.ToLower().Contains("c#")));

            if (filter.Java)
                filteredHackers.AddRange(hackers.Where(i => i.Skills.ToLower().Contains("java")));

            if (filter.Python)
                filteredHackers.AddRange(hackers.Where(i => i.Skills.ToLower().Contains("python")));

            if (filter.Javascript)
                filteredHackers.AddRange(hackers.Where(i => i.Skills.ToLower().Contains("javascript")));

            if (filter.Css)
                filteredHackers.AddRange(hackers.Where(i => i.Skills.ToLower().Contains("css")));

            if (filter.Angular)
                filteredHackers.AddRange(hackers.Where(i => i.Skills.ToLower().Contains("angular")));

            if (filter.React)
                filteredHackers.AddRange(hackers.Where(i => i.Skills.ToLower().Contains("react")));

            if (filter.Cpp)
                filteredHackers.AddRange(hackers.Where(i => i.Skills.ToLower().Contains("c++")));

            if (filter.Design)
                filteredHackers.AddRange(hackers.Where(i => i.Skills.ToLower().Contains("design")));

            if (filter.MachineLearning)
                filteredHackers.AddRange(hackers.Where(i => i.Skills.ToLower().Contains("machinelearning")));

            return filteredHackers;
        }
    }
}
