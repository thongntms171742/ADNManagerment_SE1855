using BusinessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public interface IFeedbackRepository
    {
        List<Feedback> GetAllFeedbacks();
        Feedback GetFeedbackById(int id);
        List<Feedback> GetFeedbacksByCustomerId(int customerId);
        List<Feedback> GetFeedbacksByBookingId(int bookingId);
        void AddFeedback(Feedback feedback);
        void UpdateFeedback(Feedback feedback);
        void DeleteFeedback(int id);
    }
} 