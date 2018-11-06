using RestSharp;
using SciFiReviews.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SciFiReviews.Services
{
    public interface IMovieData
    {
        Task<IRestResponse<List<Movie>>> GetMovies(int pageNumber, int pageSize, string searchWord, string resourceUrl);

        Task<IRestResponse<Movie>> GetMovie(int id);
    }
}
