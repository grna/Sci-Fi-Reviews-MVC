using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SciFiReviews.Helpers;
using SciFiReviews.Models;
using SciFiReviews.Models.DataModels;
using SciFiReviews.Models.ViewModels;
using SciFiReviews.Services;

namespace SciFiReviews.Controllers
{
    public class ReviewsController : Controller
    {
        const int pageSize = 2;

        private IReviewData _reviewData;
        private IReviewerData _reviewerData;

        public ReviewsController(IReviewData reviewData, IReviewerData reviewerData)
        {
            _reviewData = reviewData;
            _reviewerData = reviewerData;
        }

        public async Task<IActionResult> Reviews(ReviewsResourceParameters parameters)
        {
            var response = await _reviewData.GetReviews(parameters);

            if (!response.IsSuccessful)
                return View("Error", new ErrorViewModel(response.ErrorMessage, 
                    response.ErrorException.ToString()));

            ViewBag.PaginationMetadata = JsonConvert.DeserializeObject(response.Headers
                .FirstOrDefault(h => h.Name == "X-Pagination").Value.ToString());

            ViewBag.MovieGenres = new MovieGenres(parameters.MovieGenre);

            return View(response.Data);
        }

        public async Task<IActionResult> ReviewsAjax(ReviewsResourceParameters parameters)
        {
            var response = await _reviewData.GetReviews(parameters);

            if (!response.IsSuccessful)
                return View("Error", new ErrorViewModel(response.ErrorMessage,
                    response.ErrorException.ToString()));

            Response.Headers.Add("X-Pagination", response.Headers
                .FirstOrDefault(h => h.Name == "X-Pagination").Value.ToString());

            return PartialView("_Reviews", response.Data);
        }

        [Authorize]
        public async Task<IActionResult> MyReviews(string resourceUrl, int pageNumber = 1)
        {
            IRestResponse<Reviewer> response1 = await _reviewerData.GetReviewer(User.Identity.Name);
            IRestResponse<List<Review>> response2 = await _reviewData.GetMyReviews(response1.Data.Id, pageNumber, pageSize, resourceUrl);

            if (!response2.IsSuccessful)
                return View("Error", new ErrorViewModel(response2.ErrorMessage,
                    response2.ErrorException.ToString()));

            ViewBag.PaginationMetadata = JsonConvert.DeserializeObject(response2.Headers
                .FirstOrDefault(h => h.Name == "X-Pagination").Value.ToString());

            return View(response2.Data);
        }

        [Authorize]
        public async Task<IActionResult> MyReviewsAjax(string resourceUrl, int pageNumber = 1)
        {
            IRestResponse<Reviewer> response1 = await _reviewerData.GetReviewer(User.Identity.Name);
            IRestResponse<List<Review>> response2 = await _reviewData.GetMyReviews(response1.Data.Id, pageNumber, pageSize, resourceUrl);

            if (!response2.IsSuccessful)
                return View("Error", new ErrorViewModel(response2.ErrorMessage,
                    response2.ErrorException.ToString()));

            Response.Headers.Add("X-Pagination", response2.Headers
                .FirstOrDefault(h => h.Name == "X-Pagination").Value.ToString());

            return PartialView("_MyReviews", response2.Data);
        }

        public async Task<IActionResult> Review(string resourceUrl, int? id)
        {
            IRestResponse<Review> response = await _reviewData.GetReview(resourceUrl, id);

            if (!response.IsSuccessful)
                return View("Error", new ErrorViewModel(response.ErrorMessage,
                    response.ErrorException.ToString()));

            return View(response.Data);
        }

        [Authorize]
        public IActionResult CreateReview()
        {
            ViewBag.MovieGenres = new MovieGenres(null).Genres;

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview(ReviewCreateViewModel createModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _reviewData.CreateReview(createModel);

                if (!response.IsSuccessful)
                    return View("Error", new ErrorViewModel(response.ErrorMessage,
                        response.ErrorException.ToString()));

                return RedirectToAction("Review", new { resourceUrl = 
                    response.Headers.FirstOrDefault(h => h.Name == "Location").Value.ToString() });

            }

            ModelState.AddModelError("Error", "Not all fields were filled in correctly.");
            return View();
        }

        [Authorize]
        public async Task<IActionResult> EditReview(int id)
        {
            var response = await _reviewData.GetReview(null, id);

            if (!response.IsSuccessful)
                return View("Error", new ErrorViewModel(response.ErrorMessage,
                    response.ErrorException.ToString()));

            return View(new ReviewEditViewModel
            {
                Id = id,
                Title = response.Data.Title,
                Body = response.Data.Body,
                Rating = response.Data.Rating
            });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReview(ReviewEditViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _reviewData.EditReview(editModel);

                if (!response.IsSuccessful)
                    return View("Error", new ErrorViewModel(response.ErrorMessage,
                        response.ErrorException.ToString()));

                return RedirectToAction("Review", new { id = editModel.Id });
            }

            ModelState.AddModelError("Error", "Not all fields were filled in correctly.");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var response = await _reviewData.DeleteReview(id);

            if (!response.IsSuccessful)
                return View("Error", new ErrorViewModel(response.ErrorMessage,
                    response.ErrorException.ToString()));

            return RedirectToAction("MyReviews");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int commentId, int reviewId)
        {
            var response = await _reviewData.DeleteComment(reviewId, commentId);

            if (!response.IsSuccessful)
                return View("Error", new ErrorViewModel(response.ErrorMessage,
                    response.ErrorException.ToString()));

            return RedirectToAction("Review", new { id = reviewId });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(CommentCreate createModel)
        {
            if (!ModelState.IsValid)
                return null;

            var response = await _reviewData.CreateComment(createModel);

            if (!response.IsSuccessful)
                return View("Error", new ErrorViewModel(response.ErrorMessage,
                    response.ErrorException.ToString()));

            return RedirectToAction("Review", new { id = createModel.ReviewId });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LikeReview(int reviewId, string reviewerName, bool like)
        {
            var reviewerResponse = await _reviewerData.GetReviewer(reviewerName);

            if (!reviewerResponse.IsSuccessful)
                return null;

            var reviewResponse = await _reviewData.LikeReview(
                reviewId, reviewerResponse.Data.Id, like);

            if (!reviewResponse.IsSuccessful)
                return null;

            return Json(reviewResponse.Data);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
