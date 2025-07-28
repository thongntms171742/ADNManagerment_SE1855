using BusinessObjects;
using System.Collections.Generic;

namespace Services
{
    public interface IServiceService
    {
        List<Service> GetAllServices();
        Service GetServiceById(int id);
        void AddService(Service service);
        void UpdateService(Service service);
        void DeleteService(int id);
    }
} 