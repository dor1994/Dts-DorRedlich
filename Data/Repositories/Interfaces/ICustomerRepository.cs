using Data.DtoModels;
using Data.Enums;
using Data.Models;

namespace Data.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<List<QueueEntry>> GetFilterCustomersAsync(string? customerName, DateTime? requestedTime);
        public Task<(EnumResponse enumResponse, QueueEntry customerEntity)> AddCustomerAsync(CustomerModel customer);
        public Task<bool> DeleteCustomerAsync(int id);
        public Task<(EnumResponse enumResponse, QueueEntry customerEntity)> UpdateCustomerAsync(CustomerModel customer);
    }
}
