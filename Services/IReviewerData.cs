using RestSharp;
using SciFiReviews.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SciFiReviews.Services
{
    public interface IReviewerData
    {
        Task<IRestResponse<Reviewer>> GetReviewer(string username);
    }
}
