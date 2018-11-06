using RestSharp;
using SciFiReviews.Helpers;
using SciFiReviews.Models.DataModels;
using SciFiReviews.Models.EntityModels;
using SciFiReviews.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SciFiReviews.Services
{
    public interface IReviewData
    {
        Task<IRestResponse<List<Review>>> GetReviews(ReviewsResourceParameters parameters);

        Task<IRestResponse<List<Review>>> GetMyReviews(int reviewerId, int pageNumber, int pageSize, string resourceUrl);

        Task<IRestResponse<Review>> GetReview(string resourceUrl, int? id);

        Task<IRestResponse> CreateReview(ReviewCreateViewModel createModel);

        Task<IRestResponse> EditReview(ReviewEditViewModel editModel);

        Task<IRestResponse> DeleteReview(int id);

        Task<IRestResponse> DeleteComment(int reviewId, int commentId);

        Task<IRestResponse> CreateComment(CommentCreate createModel);

        Task<IRestResponse<Review>> LikeReview(int reviewId, int reviewerId, bool like);
    }
}
