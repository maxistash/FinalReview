using FinalReview.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalReview.Component
{
    //Queries the database for the teamnames
    public class BowlerTeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;
        public BowlerTeamViewComponent(BowlingLeagueContext blc)
        {
            context = blc;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeam = RouteData?.Values["teamname"];

            return View(context.Teams
                //   .Select(x=>x.TeamName)
                .Distinct()
                .OrderBy(x => x));
             //   .ToList());
        }
    }
}
