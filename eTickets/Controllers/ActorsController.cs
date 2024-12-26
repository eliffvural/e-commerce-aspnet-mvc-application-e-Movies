﻿using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {

        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAll();
            return View(data);
        }


        //Get: Actors/Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")] Actor actor)
        {
            if (string.IsNullOrEmpty(actor.FullName))
            {
                TempData["ErrorMessage"] = "Actor name is required.";
                return View(actor);
            }
            if (string.IsNullOrEmpty(actor.Bio))
            {
                TempData["ErrorMessage"] = "Bio is required.";
                return View(actor);
            }
            if (string.IsNullOrEmpty(actor.ProfilePictureURL))
            {
                TempData["ErrorMessage"] = "Profile picture is required.";
                return View(actor);
            }
            _service.Add(actor);
            return RedirectToAction(nameof(Index));
        }


     

    }
}