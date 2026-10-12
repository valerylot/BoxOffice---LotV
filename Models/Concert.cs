using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxOffice___LotV.Models
{
    public class Concert
    {
        public int Id {get; set;}

        public string Artist {get; set;} = string.Empty;

        public string? OpeningAct {get; set;} = string.Empty;

        public string Venue {get; set;} = string.Empty;

        public string City {get; set;} = string.Empty;

        public DateTime Date {get; set;}

        public string Genre {get; set;} = string.Empty;

        public double TicketPrice {get; set;}

        public int Capacity {get; set;}

        public int TicketsSold {get; set;} = 0;
    }
}