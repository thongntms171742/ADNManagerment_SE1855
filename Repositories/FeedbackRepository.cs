using BusinessObjects;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly FeedbackDAO _feedbackDAO;

        public FeedbackRepository()
        {
            _feedbackDAO = new FeedbackDAO();
        }

        public List<Feedback> GetAllFeedbacks() => _feedbackDAO.GetAllFeedbacks();
        public Feedback GetFeedbackById(int id) => _feedbackDAO.GetFeedbackById(id);
        public List<Feedback> GetFeedbacksByCustomerId(int customerId) => _feedbackDAO.GetFeedbacksByCustomerId(customerId);
        public List<Feedback> GetFeedbacksByBookingId(int bookingId) => _feedbackDAO.GetFeedbacksByBookingId(bookingId);
        public void AddFeedback(Feedback feedback) => _feedbackDAO.AddFeedback(feedback);
        public void UpdateFeedback(Feedback feedback) => _feedbackDAO.UpdateFeedback(feedback);
        public void DeleteFeedback(int id) => _feedbackDAO.DeleteFeedback(id);
    }
} 