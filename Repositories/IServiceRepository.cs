using BusinessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public interface IServiceRepository
    {
        List<Service> GetAllServices();
        Service GetServiceById(int id);
        void AddService(Service service);
        void UpdateService(Service service);
        void DeleteService(int id);
    }
} 