using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tdcsp2020.Models;
using tdcsp2020.Services;

namespace tdcsp2020.Controllers
{
    public class HomeController : Controller
    {
        private NewsService _newsService;

        public HomeController(NewsService newsService)
        {
            _newsService = newsService;
        }


        public IActionResult Index()
        {
            var news = _newsService.Load(20, "turismo");

            return View(news);
        }

    }
}
