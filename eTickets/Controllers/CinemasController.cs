using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }





        //GET: Cinemas/Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")]Cinema cinema)
        {
            if (string.IsNullOrEmpty(cinema.Name))
            {
                TempData["ErrorMessage"] = "Name is required.";
                return View(cinema);
            }
            if (string.IsNullOrEmpty(cinema.Logo))
            {
                TempData["ErrorMessage"] = "Logo is required.";
                return View(cinema);
            }
            if (string.IsNullOrEmpty(cinema.Description))
            {
                TempData["ErrorMessage"] = "Description is required.";
                return View(cinema);
            }

            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));

        }




        //GET: Cinemas/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }




        //GET: Cinemas/Edit/1

        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (string.IsNullOrEmpty(cinema.Name))
            {
                TempData["ErrorMessage"] = "Name is required.";
                return View(cinema);
            }
            if (string.IsNullOrEmpty(cinema.Logo))
            {
                TempData["ErrorMessage"] = "Logo is required.";
                return View(cinema);
            }
            if (string.IsNullOrEmpty(cinema.Description))
            {
                TempData["ErrorMessage"] = "Description is required.";
                return View(cinema);
            }

            await _service.UpdateAsync(id,cinema);
            return RedirectToAction(nameof(Index));

        }


    }
}
