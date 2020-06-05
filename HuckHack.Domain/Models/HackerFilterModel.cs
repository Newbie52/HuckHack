namespace HuckHack.Domain.Models
{
    public class HackerFilterModel
    {
        public string SearchValue { get; set; }

        public bool Solo { get; set; }

        public bool SearchTeam { get; set; }

        public bool HasTeam { get; set; }

        public bool Frontend { get; set; }
        public bool Backend { get; set; }
        public bool Analyst { get; set; }
        public bool Designer { get; set; }
        public bool Mobile { get; set; }
        public bool TeamLead { get; set; }
        public bool Speaker { get; set; }
        public bool Other { get; set; }

        public bool CSharp { get; set; }
        public bool Java { get; set; }
        public bool Python { get; set; }
        public bool Javascript { get; set; }
        public bool Css { get; set; }
        public bool Angular { get; set; }
        public bool React { get; set; }
        public bool Cpp { get; set; }
        public bool Design { get; set; }
        public bool MachineLearning { get; set; }

        //public string[] Skills { get; set; }
    }
}
