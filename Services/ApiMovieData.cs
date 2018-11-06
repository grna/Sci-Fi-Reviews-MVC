using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using SciFiReviews.Models.DataModels;

namespace SciFiReviews.Services
{
    public class ApiMovieData : IMovieData
    {
        private IRestClient _restClient;

        public ApiMovieData(IRestClient restclient)
        {
            _restClient = restclient;
            _restClient.BaseUrl = new Uri("https://localhost:44330/api/movies");
        }

        public async Task<IRestResponse<List<Movie>>> GetMovies(int pageNumber, int pageSize, string searchWord, string resourceUrl)
        {
            RestRequest restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Accept", "application/json");

            if (!string.IsNullOrEmpty(resourceUrl))
                restRequest.Resource = resourceUrl;
            else
            {
                restRequest.AddParameter("pageNumber", pageNumber);
                restRequest.AddParameter("pageSize", pageSize);
                restRequest.AddParameter("searchWord", searchWord);
            }

            return await _restClient.ExecuteTaskAsync<List<Movie>>(restRequest);
        }

        public async Task<IRestResponse<Movie>> GetMovie(int id)
        {
            RestRequest restRequest = new RestRequest($"{id}", Method.GET);
            restRequest.AddHeader("Accept", "application/json");

            return await _restClient.ExecuteTaskAsync<Movie>(restRequest);
        }
    }
}
