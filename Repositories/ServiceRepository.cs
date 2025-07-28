using BusinessObjects;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ServiceDAO _serviceDAO;

        public ServiceRepository()
        {
            _serviceDAO = new ServiceDAO();
        }

        public List<Service> GetAllServices() => _serviceDAO.GetAllServices();
        public Service GetServiceById(int id) => _serviceDAO.GetServiceById(id);
        public void AddService(Service service) => _serviceDAO.AddService(service);
        public void UpdateService(Service service) => _serviceDAO.UpdateService(service);
        public void DeleteService(int id) => _serviceDAO.DeleteService(id);
    }
} 