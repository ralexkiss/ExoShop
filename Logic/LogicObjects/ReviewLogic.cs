using Data;
using Data.Repositories;
using Interfaces.Contexts;
using Interfaces.Logic;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Logic.LogicObjects
{
    public class ReviewLogic : IReviewLogic
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewLogic(IReviewContext context)
        {
            reviewRepository = new ReviewRepository(context);
        }

        public List<Review>GetAllByProduct(Product product)
        {
            return reviewRepository.GetAllByProduct(product);
        }

        public Review GetReviewById(int id)
        {
            return reviewRepository.GetReviewById(id);
        }

        public void AddReview(Review review)
        {
            reviewRepository.AddReview(review);
        }
        public void RemoveReview(Review review)
        {
            reviewRepository.RemoveReview(review);
        }
    }
}