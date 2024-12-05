using Data.DtoModels;
using Data.Enums;
using Data.Models;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(EnumResponse enumResponse, QueueEntry customerEntity)> AddCustomerAsync(CustomerModel customer)
        {
            var queueEntity = await _unitOfWork.FirstOrDefaultAsync<QueueEntry>(x => x.RequestedTime == customer.RequestedTime);
            var isCustomerAdded = 0;
            if (queueEntity == null)
            {
                QueueEntry queueEntryEntity = _unitOfWork.MapperModelToDto<CustomerModel, QueueEntry>(customer);

                await _unitOfWork.Repository<QueueEntry>().AddAsync(queueEntity);
                isCustomerAdded = await _unitOfWork.SaveChangesAsync();
                return (EnumResponse.CustomerAdded, queueEntity);

            }

            return (EnumResponse.DateRequestExist, new QueueEntry());
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<QueueEntry>> GetAllCustomersAsync()
        {
            var queueList = await _unitOfWork.Repository<QueueEntry>().GetAllAsync();
            return queueList.ToList();
        }

        public async Task<bool> UpdateCustomerAsync(CustomerModel customer)
        {
            throw new NotImplementedException();
        }
    }
}
