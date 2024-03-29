using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System.Diagnostics;

namespace SalesWebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SellerService _sellerService;
        private readonly SalesRecordService _salesRecordService;

        public HomeController(ILogger<HomeController> logger, SellerService sellerService, SalesRecordService salesRecordService)
        {
            _logger = logger;
            _sellerService = sellerService;
            _salesRecordService = salesRecordService;
        }

        public async Task<IActionResult> Index()
        {
            int sellersCount = await _sellerService.CountAllAsync();
            Department mostSaleDepartment = _salesRecordService.TopSaleDepartment(new DateTime(DateTime.Now.Year,DateTime.Now.Month,(DateTime.Now.Day - 7)),DateTime.Now).Key;
            HomeViewModel viewModel = new HomeViewModel { SellersCount = sellersCount, DepartmentMostSale = mostSaleDepartment };

            return View(viewModel);
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