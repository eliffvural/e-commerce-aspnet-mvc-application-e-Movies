using Microsoft.AspNetCore.Mvc;
using System.Data;
using eTickets.Data.Services;

namespace eTickets.Controllers
{
    public class OlapController : Controller
    {
        private readonly IOlapService _olapService;

        public OlapController(IOlapService olapService)
        {
            _olapService = olapService;
        }

        public IActionResult Index()
        {
            string query = @"SELECT 
                                {[Measures].[TotalSalesAmount], [Measures].[HighestPriceSold]} ON COLUMNS,
                                NON EMPTY [Movies].[Name].MEMBERS ON ROWS
                             FROM [Ecommerce-app-db]";
            DataTable data = _olapService.ExecuteOlapQuery(query);
            return View(data);
        }
    }
}
