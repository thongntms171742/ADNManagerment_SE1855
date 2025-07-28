using BusinessObjects;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class SampleRepository : ISampleRepository
    {
        private readonly SampleDAO _sampleDAO;

        public SampleRepository()
        {
            _sampleDAO = new SampleDAO();
        }

        public List<Sample> GetAllSamples() => _sampleDAO.GetAllSamples();
        public Sample GetSampleById(int id) => _sampleDAO.GetSampleById(id);
        public Sample GetSampleByBookingId(int bookingId) => _sampleDAO.GetSampleByBookingId(bookingId);
        public void AddSample(Sample sample) => _sampleDAO.AddSample(sample);
        public void UpdateSample(Sample sample) => _sampleDAO.UpdateSample(sample);
        public void DeleteSample(int id) => _sampleDAO.DeleteSample(id);
    }
} 