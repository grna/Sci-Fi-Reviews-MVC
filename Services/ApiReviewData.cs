using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;
using SciFiReviews.Helpers;
using SciFiReviews.Models.DataModels;
using SciFiReviews.Models.EntityModels;
using SciFiReviews.Models.ViewModels;

namespace SciFiReviews.Services
{
    public class ApiReviewData : IReviewData
    {
        private IRestClient _restClient;

        public ApiReviewData(IRestClient restClient)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri("https://localhost:44330/api/reviews");
            
        }

        public async Task<IRestResponse<List<Review>>> GetReviews(ReviewsResourceParameters parameters)
        {
            RestRequest restRequest = new RestRequest(Method.GET);

            restRequest.AddHeader("Accept", "application/json");

            if (!string.IsNullOrEmpty(parameters.ResourceUrl))
                restRequest.Resource = parameters.ResourceUrl;
            else
            {
                restRequest.AddParameter("pageNumber", parameters.PageNumber);
                restRequest.AddParameter("pageSize", parameters.PageSize);
            }

            if (!string.IsNullOrEmpty(parameters.MovieGenre))
                restRequest.AddParameter("movieGenre", parameters.MovieGenre);

            if (!string.IsNullOrEmpty(parameters.SortBy))
                restRequest.AddParameter("sortBy", parameters.SortBy);

            return await _restClient.ExecuteTaskAsync<List<Review>>(restRequest);
        }

        public async Task<IRestResponse<List<Review>>> GetMyReviews(int reviewerId, int pageNumber, int pageSize, string resourceUrl)
        {
            RestRequest restRequest = new RestRequest(Method.GET);

            restRequest.AddHeader("Accept", "application/json");

            if (!string.IsNullOrEmpty(resourceUrl))
                restRequest.Resource = resourceUrl;
            else
            {
                restRequest.AddParameter("reviewerId", reviewerId);
                restRequest.AddParameter("pageNumber", pageNumber);
                restRequest.AddParameter("pageSize", pageSize);
            }

            return await _restClient.ExecuteTaskAsync<List<Review>>(restRequest);
        }

        public async Task<IRestResponse<Review>> GetReview(string resourceUrl, int? id)
        {
            RestRequest restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Accept", "application/json");

            if (!string.IsNullOrEmpty(resourceUrl))
                restRequest.Resource = resourceUrl;
            else
                restRequest.Resource = restRequest.Resource + $"/{id}";

            return await _restClient.ExecuteTaskAsync<Review>(restRequest);
        }

        public async Task<IRestResponse> CreateReview(ReviewCreateViewModel createModel)
        {
            RestRequest restRequest = new RestRequest(Method.POST);

            restRequest.AddHeader("Content", "application/json");
            restRequest.AddJsonBody(createModel);

            return await _restClient.ExecuteTaskAsync(restRequest);
        }

        public async Task<IRestResponse> EditReview(ReviewEditViewModel editModel)
        {
            RestRequest restRequest = new RestRequest(Method.PUT);
            restRequest.AddHeader("Content", "application/json");
            restRequest.AddJsonBody(editModel);

            return await _restClient.ExecuteTaskAsync(restRequest);
        }

        public async Task<IRestResponse> DeleteReview(int id)
        {
            RestRequest restRequest = new RestRequest($"/{id}", Method.DELETE);

            return await _restClient.ExecuteTaskAsync(restRequest);
        }

        public async Task<IRestResponse> DeleteComment(int reviewId, int commentId)
        {
            RestRequest restRequest = new RestRequest($"/{reviewId}/comments/{commentId}", Method.DELETE);

            return await _restClient.ExecuteTaskAsync(restRequest);
        }

        public async Task<IRestResponse> CreateComment(CommentCreate createModel)
        {
            RestRequest restRequest = new RestRequest($"/{createModel.ReviewId}/comments", Method.POST);
            restRequest.AddHeader("Content", "application/json");
            restRequest.AddJsonBody(createModel);

            return await _restClient.ExecuteTaskAsync(restRequest);
        }

        public async Task<IRestResponse<Review>> LikeReview(int reviewId, int reviewerId, bool like)
        {
            RestRequest restRequest = 
                new RestRequest($"/{reviewId}/like?reviewerId={reviewerId}&like={like}", 
                    Method.PUT);
            restRequest.AddHeader("Accept", "application/json");

            return await _restClient.ExecuteTaskAsync<Review>(restRequest);
        }
    }
}
