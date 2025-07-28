using BusinessObjects;
using System.Collections.Generic;

namespace Services
{
    public interface ISampleService
    {
        List<Sample> GetAllSamples();
        Sample GetSampleById(int id);
        Sample GetSampleByBookingId(int bookingId);
        void AddSample(Sample sample);
        void UpdateSample(Sample sample);
        void DeleteSample(int id);
    }
} 