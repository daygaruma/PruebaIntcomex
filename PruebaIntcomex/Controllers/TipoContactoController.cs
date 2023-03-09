using Microsoft.AspNetCore.Mvc;
using PruebaIntcomex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace PruebaIntcomex.Controllers
{
    public class TipoContactoController : Controller
    {
        private readonly ILogger<TipoContactoController> _logger;

        public TipoContactoController(ILogger<TipoContactoController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
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
