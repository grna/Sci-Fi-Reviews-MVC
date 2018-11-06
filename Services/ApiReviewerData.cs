using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using SciFiReviews.Models.DataModels;

namespace SciFiReviews.Services
{
    public class ApiReviewerData : IReviewerData
    {
        private IRestClient _restClient;

        public ApiReviewerData(IRestClient restClient)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri("https://localhost:44330/api/reviewers");
        }

        public async Task<IRestResponse<Reviewer>> GetReviewer(string username)
        {
            RestRequest restRequest = new RestRequest($"/{username}", Method.GET);
            restRequest.AddHeader("Accept", "application/json");

            return await _restClient.ExecuteTaskAsync<Reviewer>(restRequest);
        }
    }
}
