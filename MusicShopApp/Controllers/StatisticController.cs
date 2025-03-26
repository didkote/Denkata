using Microsoft.AspNetCore.Mvc;

using MusicShopApp.Core.Contracts;
using MusicShopApp.Core.Services;
using MusicShopApp.Models.Statistic;

namespace MusicShopApp.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IStatisticService statisticsService;


        public StatisticController(IStatisticService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        public IActionResult Index()
        {
            StatisticVM statistics = new StatisticVM();

            statistics.CountClients = statisticsService.CountClients();
            statistics.CountProducts = statisticsService.CountProducts();
            statistics.CountOrders = statisticsService.CountOrders();
            statistics.SumOrders = statisticsService.SumOrders();

            return View(statistics);
        }
    }
}