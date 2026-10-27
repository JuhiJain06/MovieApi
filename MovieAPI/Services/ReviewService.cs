using MovieAPI.Exceptions;
using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.RequestModels;
using MovieAPI.Models.ResponseModels;
using MovieAPI.Repositories.Interfaces;
using MovieAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<int> Add(int movieId,ReviewRequestModel request)
        {
            var review = new Review()
            {
                MovieId=movieId,
                Message = request.Message,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            Validate(review);

            review.Id=await _reviewRepository.Add(review);

            return review.Id;
        }

        public async Task Delete(int movieId,int id)
        {
            if (id < 1)
            {
                throw new BadRequestException("Invalid review");
            }

            var review = await _reviewRepository.Get(id,movieId);

            if (review == null)
            {
                throw new NotFoundException("review not found");
            }

            await _reviewRepository.Delete(review);
        }

        public async Task<List<ReviewResponseModel>> Get(int movieId)
        {
            var reviews = await _reviewRepository.Get(movieId);

            return reviews.Select(x=>new ReviewResponseModel()
            {
                Id=x.Id,
                Message=x.Message
            }).ToList();
        }

        public async  Task<ReviewResponseModel> Get(int movieId, int id)
        {
            if (id < 1)
            {
                throw new BadRequestException("Invalid review");
            }

            var review = await _reviewRepository.Get(id,movieId);

            if (review == null)
            {
                throw new NotFoundException("review not found");
            }

            var response= new ReviewResponseModel()
            {
                Id=review.Id,
                Message=review.Message,
            };

            return response;
        }

        public async Task Update(int movieId,int id, ReviewUpdateRequest request)
        {
            
            var existingReview = await _reviewRepository.Get(id,movieId);

            if (existingReview == null)
            {
                throw new NotFoundException("Review you are trying to update does not exists");
            }
            var review = new Review()
            {
                Id = id,
                MovieId = movieId,
                Message = request.Message,
                UpdatedAt = DateTime.UtcNow,
                CreatedAt =existingReview.CreatedAt
            };


            Validate(review);

            await _reviewRepository.Update(review);
        }

        public void Validate(Review request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                throw new BadRequestException("Enter proper message");
            }



        }
    }
}
