using BusinessObjects;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;

        public ResultService()
        {
            _resultRepository = new ResultRepository();
        }

        public List<Result> GetAllResults() => _resultRepository.GetAllResults();
        public Result GetResultById(int id) => _resultRepository.GetResultById(id);
        public Result GetResultByBookingId(int bookingId) => _resultRepository.GetResultByBookingId(bookingId);
        public List<Result> GetResultsByStaffId(int staffId) => _resultRepository.GetResultsByStaffId(staffId);
        public List<Result> GetResultsByCustomerId(int customerId) => _resultRepository.GetResultsByCustomerId(customerId);
        public void AddResult(Result result) => _resultRepository.AddResult(result);
        public void UpdateResult(Result result) => _resultRepository.UpdateResult(result);
        public void DeleteResult(int id) => _resultRepository.DeleteResult(id);
    }
} 