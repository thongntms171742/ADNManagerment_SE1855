using BusinessObjects;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly ResultDAO _resultDAO;

        public ResultRepository()
        {
            _resultDAO = new ResultDAO();
        }

        public List<Result> GetAllResults() => _resultDAO.GetAllResults();
        public Result GetResultById(int id) => _resultDAO.GetResultById(id);
        public Result GetResultByBookingId(int bookingId) => _resultDAO.GetResultByBookingId(bookingId);
        public List<Result> GetResultsByStaffId(int staffId) => _resultDAO.GetResultsByStaffId(staffId);
        public List<Result> GetResultsByCustomerId(int customerId) => _resultDAO.GetResultsByCustomerId(customerId);
        public void AddResult(Result result) => _resultDAO.AddResult(result);
        public void UpdateResult(Result result) => _resultDAO.UpdateResult(result);
        public void DeleteResult(int id) => _resultDAO.DeleteResult(id);
    }
} 