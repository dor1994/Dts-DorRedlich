using Data.DtoModels;
using Data.Enums;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
