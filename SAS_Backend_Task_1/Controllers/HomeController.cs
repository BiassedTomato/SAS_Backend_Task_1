using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SAS_Backend_Task_1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SAS_Backend_Task_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IServiceA _aService;
        private IServiceB1 _b1Service;
        private IServiceB2 _b2Service;

        public HomeController(IServiceA sA, IServiceB1 sB1, IServiceB2 sB2, ILogger<HomeController> logger)
        {
            _logger = logger;
            _aService = sA;
            _b1Service = sB1;
            _b2Service = sB2;

            ViewBag.A = 0;
            ViewBag.B1 = 0;
        }

        public IActionResult Index()
        {
            var val = _aService.GetConstructionCount();
            ViewBag.A = val;

            ViewData["A"] = val;

            ViewBag.B1 = _b1Service.GetConstructionCount();

            ViewBag.SetA = (_aService as ServiceA).Settings.ServiceInfo;
            ViewBag.SetB = (_b1Service as ServiceB).Settings.ServiceInfo;

            return View(new InterfaceViewmodel() { B2 = _b2Service.GetConstructionCount() });
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

    public class ServiceAViewModel
    {
        public int Count;

        public ServiceAViewModel(int value)
        {
            Count = value;
        }
    }
}
