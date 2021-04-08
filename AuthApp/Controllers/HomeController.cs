using AuthApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JikanDotNet;

namespace AuthApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                Jikan jikan = new Jikan();

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userAnimeLabels = _dbContext.UserAnimeLabels
                    .Where(x => x.UserId == userId)
                    .Include(x => x.Label)
                    .Select(x => new LabeledAnime 
                    {
                        LabelName = x.Label.Name,
                        AnimeName = jikan.GetAnime(x.AnimeId).Result.Title
                    })
                    .ToList();

                var temp = userAnimeLabels.GroupBy(x => x.LabelName);
                ViewData["LikedAnime"] = userAnimeLabels.GroupBy(x => x.LabelName);
            }

            return View();
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
