using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shout.DAL;
using Shout.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shout.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShoutContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _context = new ShoutContext();
            _logger = logger;
        }

        public IActionResult Index()
        {
            var posts = from post in _context.Shoutts
                        where post.DatePublication < DateTime.Now
                        select post;

            ViewData["Posts"] = posts;

            return View();
        }

        [HttpPost]
        public IActionResult Index(Shoutt shoutt)
        {
            if (ModelState.IsValid)
            {
                shoutt.DatePublication = DateTime.Now;
                _context.Shoutts.Add(shoutt);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoutt);
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
