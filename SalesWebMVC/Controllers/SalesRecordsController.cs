using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SalesRecordsController : Controller
    {

        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? mindate, DateTime? maxdate)
        {
            if (!mindate.HasValue)
            {
                mindate = new DateTime(DateTime.Now.Year,1,1);
            }
            if (!maxdate.HasValue)
            {
                maxdate = DateTime.Now;
            }
            ViewData["mindate"] = mindate.Value.ToString("yyyy-MM-dd");
            ViewData["maxdate"] = maxdate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordService.FindByDateAsync(mindate, maxdate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? mindate, DateTime? maxdate)
        {
            if (!mindate.HasValue)
            {
                mindate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxdate.HasValue)
            {
                maxdate = DateTime.Now;
            }

            ViewData["mindate"] = mindate.Value.ToString("yyyy-MM-dd");
            ViewData["maxdate"] = maxdate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordService.FindByDateGroupingAsync(mindate, maxdate);
            return View(result);
        }
    }
}
