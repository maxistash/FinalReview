using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalReview.Models.ViewModels
{
    // The data that we will pull from in the index
    public class IndexViewModel
    {
        public List<Bowler> Bowlers { get; set; }
        public PageNumberingInfo PageNumberingInfo { get; set; }
        public string Team { get; set; }
        public List<Team> Teams { get; set; }
    }
}
