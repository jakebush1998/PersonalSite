using Microsoft.AspNetCore.Mvc;
using PersonalSite.UI.MVC.Models;
using System.Diagnostics;

//Email - step 2
using Microsoft.Extensions.Configuration;

using MimeKit;
using MailKit.Net.Smtp;

namespace PersonalSite.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //email - step 3
        private readonly IConfiguration _config;

        //email - step 4
        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Contact()
        {
            return View();

        }



        public IActionResult Index()
        {
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