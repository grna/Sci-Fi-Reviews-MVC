using Microsoft.AspNetCore.Http;
using RestSharp;
using SciFiReviews.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SciFiReviews.Services
{
    public interface IAuthService
    {
        Task<IRestResponse> ValidateCredentials(SignInViewModel signInModel);

        Task<IRestResponse> CreateNewUser(SignUpViewModel signUpModel);
    }
}
