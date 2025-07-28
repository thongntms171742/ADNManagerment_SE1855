using BusinessObjects;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService()
        {
            _serviceRepository = new ServiceRepository();
        }

        public List<Service> GetAllServices() => _serviceRepository.GetAllServices();
        public Service GetServiceById(int id) => _serviceRepository.GetServiceById(id);
        public void AddService(Service service) => _serviceRepository.AddService(service);
        public void UpdateService(Service service) => _serviceRepository.UpdateService(service);
        public void DeleteService(int id) => _serviceRepository.DeleteService(id);
    }
} 