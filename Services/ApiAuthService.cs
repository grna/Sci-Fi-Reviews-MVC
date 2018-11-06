using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using RestSharp;
using SciFiReviews.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SciFiReviews.Services
{
    public class ApiAuthService : IAuthService
    {
        private IRestClient _restClient;

        public ApiAuthService(IRestClient restClient)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri("https://localhost:44330/api/auth");
        }

        public async Task<IRestResponse> CreateNewUser(SignUpViewModel signUpModel)
        {
            IRestRequest request = new RestRequest("/create", Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(signUpModel);

            return await _restClient.ExecuteTaskAsync(request);
        }

        public async Task<IRestResponse> ValidateCredentials(SignInViewModel signInModel)
        {
            IRestRequest request = new RestRequest("/validate", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(signInModel);

            return await _restClient.ExecuteTaskAsync(request);
        }
    }
}
