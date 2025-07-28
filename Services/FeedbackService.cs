using BusinessObjects;
using DataAccessObjects;
using System.Collections.Generic;

namespace Services
{
    public interface IFeedbackService
    {
        List<Feedback> GetAllFeedbacks();
        List<Feedback> GetFeedbacksByCustomerId(int customerId);
        List<Feedback> GetFeedbacksByBookingId(int bookingId);
        Feedback? GetFeedbackById(int id);
        void AddFeedback(Feedback feedback);
        void UpdateFeedback(Feedback feedback);
        void DeleteFeedback(int id);
    }

    public class FeedbackService : IFeedbackService
    {
        private readonly FeedbackDAO _feedbackDAO;

        public FeedbackService()
        {
            _feedbackDAO = new FeedbackDAO();
        }

        public List<Feedback> GetAllFeedbacks()
        {
            return _feedbackDAO.GetAllFeedbacks();
        }

        public List<Feedback> GetFeedbacksByCustomerId(int customerId)
        {
            return _feedbackDAO.GetFeedbacksByCustomerId(customerId);
        }

        public Feedback? GetFeedbackById(int id)
        {
            return _feedbackDAO.GetFeedbackById(id);
        }

        public List<Feedback> GetFeedbacksByBookingId(int bookingId)
        {
            return _feedbackDAO.GetFeedbacksByBookingId(bookingId);
        }

        public void AddFeedback(Feedback feedback)
        {
            _feedbackDAO.AddFeedback(feedback);
        }

        public void UpdateFeedback(Feedback feedback)
        {
            _feedbackDAO.UpdateFeedback(feedback);
        }

        public void DeleteFeedback(int id)
        {
            _feedbackDAO.DeleteFeedback(id);
        }
    }
} 