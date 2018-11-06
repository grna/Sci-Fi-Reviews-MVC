using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SciFiReviews.Models;
using SciFiReviews.Services;

namespace SciFiReviews.Controllers
{
    public class MoviesController : Controller
    {
        const int pageSize = 4;
        private IMovieData _movieData;

        public MoviesController(IMovieData movieData)
        {
            _movieData = movieData;
        }

        public async Task<IActionResult> Movies(string searchWord, string resourceUrl, int pageNumber = 1)
        {
            var response = await _movieData.GetMovies(pageNumber, pageSize, searchWord, resourceUrl);

            if (!response.IsSuccessful)
                return View("Error", new ErrorViewModel(response.ErrorMessage,
                    response.ErrorException.ToString()));

            ViewBag.PaginationMetadata = JsonConvert.DeserializeObject(response.Headers
                .FirstOrDefault(h => h.Name == "X-Pagination").Value.ToString());

            return View(response.Data);
        }

        public async Task<IActionResult> MoviesAjax(string searchWord, string resourceUrl, int pageNumber = 1)
        {
            var response = await _movieData.GetMovies(pageNumber, pageSize, searchWord, resourceUrl);

            if (!response.IsSuccessful)
                return View("Error", new ErrorViewModel(response.ErrorMessage,
                    response.ErrorException.ToString()));

            Response.Headers.Add("X-Pagination", response.Headers
                .FirstOrDefault(h => h.Name == "X-Pagination").Value.ToString());

            return PartialView("_Movies", response.Data);
        }

        public async Task<IActionResult> Movie(int id)
        {
            var response = await _movieData.GetMovie(id);

            if (!response.IsSuccessful)
                return View("Error", new ErrorViewModel(response.ErrorMessage,
                    response.ErrorException.ToString()));

            return View(response.Data);
        }
    }
}