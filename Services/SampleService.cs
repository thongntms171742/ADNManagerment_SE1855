using BusinessObjects;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class SampleService : ISampleService
    {
        private readonly ISampleRepository _sampleRepository;

        public SampleService()
        {
            _sampleRepository = new SampleRepository();
        }

        public List<Sample> GetAllSamples() => _sampleRepository.GetAllSamples();
        public Sample GetSampleById(int id) => _sampleRepository.GetSampleById(id);
        public Sample GetSampleByBookingId(int bookingId) => _sampleRepository.GetSampleByBookingId(bookingId);
        public void AddSample(Sample sample) => _sampleRepository.AddSample(sample);
        public void UpdateSample(Sample sample) => _sampleRepository.UpdateSample(sample);
        public void DeleteSample(int id) => _sampleRepository.DeleteSample(id);
    }
} 