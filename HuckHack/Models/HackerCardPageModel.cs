using HuckHack.Domain.Entities;
using HuckHack.Domain.Models;

using System.Collections.Generic;

namespace HuckHack.Models
{
    public class HackerCardPageModel
    {
        public HackerFilterModel Filter { get; set; }

        public List<HackerCard> Hackers { get; set; }
    }
}
