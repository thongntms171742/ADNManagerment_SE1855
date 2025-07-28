using BusinessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public interface IResultRepository
    {
        List<Result> GetAllResults();
        Result GetResultById(int id);
        Result GetResultByBookingId(int bookingId);
        List<Result> GetResultsByStaffId(int staffId);
        List<Result> GetResultsByCustomerId(int customerId);
        void AddResult(Result result);
        void UpdateResult(Result result);
        void DeleteResult(int id);
    }
} 