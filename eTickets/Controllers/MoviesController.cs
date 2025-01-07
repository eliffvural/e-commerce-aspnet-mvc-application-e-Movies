using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        private const string ApiUrl = "https://api.themoviedb.org/3/trending/movie/day?language=en-US";
        private const string BearerToken = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwZDY1ZDU0OGU4Nzg1Njk2MjNlMDc2MmZhNDY5YzExNyIsIm5iZiI6MTczNjE5Mjc5Ny4zODIwMDAyLCJzdWIiOiI2NzdjMzMxZDhmZDZmNTEwOWQ3MmVhOGIiLCJzY29wZXMiOlsiYXBpX3JlYWQiXSwidmVyc2lvbiI6MX0.CiVXjuU_WDjeKwtd-TJiqTXZi4_S8w-KRjxUhCFxgiE"; // Buraya gerçek tokenınızı koyun

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync(n=>n.Cinema);
            return View(allMovies);
        }


        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);

            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filteredResult );
            }
            return View("Index",allMovies);
        }


        // GET: Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync(id);
            return View(movieDetail);
        }




        //GET:  Movies/Create
        public async Task< IActionResult> Create()
        {
           var movieDropdownsData= await _service.GetNewMovieDropdownsVM();


            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
           

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsVM();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }





        //GET:  Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {

            var movieDetails = await _service.GetMovieByIdAsync(id);

            if (movieDetails == null) return View("NotFound");

            var response= new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                ImageURL = movieDetails.ImageURL,
                CinemaId = movieDetails.CinemaId,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                MovieCategory = movieDetails.MovieCategory,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actor_Movies.Select(n => n.ActorId).ToList()
            };


            var movieDropdownsData = await _service.GetNewMovieDropdownsVM();


            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewMovieVM movie)
        {
            if( id != movie.Id) return View("NotFound");    


            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsVM();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> TrendingMovies()
        {
            try
            {
                // HttpClient ile API'ye bağlan
                using var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(ApiUrl),
                    Headers =
           {
               { "accept", "application/json" },
               { "Authorization", $"Bearer {BearerToken}" },
           },
                };

                using var response = await client.SendAsync(request);

                // API çağrısı başarısızsa hata döner
                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, "Failed to fetch trending movies.");

                // API'den JSON veriyi al
                var responseBody = await response.Content.ReadAsStringAsync();

                // JSON'u dinamik objeye dönüştür
                var moviesData = JsonConvert.DeserializeObject<dynamic>(responseBody);

                // Veriyi Dictionary içine filtrele ve aktar
                var movieList = new List<Dictionary<string, string>>();
                foreach (var movie in moviesData.results)
                {
                    var movieDict = new Dictionary<string, string>
           {
               { "original_title", (string)movie.original_title },
               { "overview", (string)movie.overview },
               { "poster_path", $"https://image.tmdb.org/t/p/w500{(string)movie.poster_path}" } // Poster URL'sini tam yap
           };
                    movieList.Add(movieDict);
                }

                // Filtrelenmiş veriyi View'a gönder
                return View(movieList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
