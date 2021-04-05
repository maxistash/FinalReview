using FinalReview.Models;
using FinalReview.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FinalReview.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        // adding the BLContext creates an instance of the database in the program
        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext blc)
        {
            _logger = logger;
            // this is so you can pass context to the rest of the pages
            context = blc;
        }

        public IActionResult Index(long? team, string teamid, int pagenum)
        {
            int pageSize = 5;
            int pageNum = pagenum;

            return View(new IndexViewModel
            {
                // Queries the database for the bowlers in a particular team
                // Selects a predetermined amount of bowlers
                Team = teamid,
                Bowlers = (context.Bowlers
                .Where(m => m.TeamId == team || team == null)
                .OrderBy(m => m.BowlerLastName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                //   .FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {team} OR {team} IS NULL ORDER BY BowlerFirstName")
                //   .Where(x => x.BowlerState.Contains("TX"))
                //   .OrderBy(x => x.BowlerFirstName)
                .ToList()),

                PageNumberingInfo = new PageNumberingInfo
                {
                    // Finds out the page numers
                    itemsPerPage = pageSize,
                    currentPage = pagenum,
                    // If no team has been selected, then get the full count; otherwise, only count the number from the team that has been selected
                    totalItems = (team == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(x => x.TeamId == team).Count())
                },

                // List of teams to be dynamically displayed later
                Teams = (context.Teams
                .Where(m => m.TeamId == team)
                .ToList())

            }) ;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
