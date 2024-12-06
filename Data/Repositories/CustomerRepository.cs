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

            try
            {
                var queueEntity = await _unitOfWork.FirstOrDefaultAsync<QueueEntry>(x => x.RequestedTime == customer.RequestedTime);
                var isCustomerAdded = 0;
                if (queueEntity == null)
                {
                    QueueEntry queueEntryEntity = _unitOfWork.Mapper<CustomerModel, QueueEntry>(customer);
                    queueEntryEntity.CreatedAt = DateTime.Now;
                    
                    await _unitOfWork.Repository<QueueEntry>().AddAsync(queueEntryEntity);
                    
                    isCustomerAdded = await _unitOfWork.SaveChangesAsync();
                    return (EnumResponse.CustomerAdded, queueEntryEntity);

                }
            }
            catch (Exception e)
            {

                throw e; //Write the error to logger
            }
           

            return (EnumResponse.DateRequestExist, null);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.Repository<QueueEntry>().GetByIdAsync(id);
               
                _unitOfWork.Repository<QueueEntry>().Delete(entity);
                
                int isCustomerDeleted = await _unitOfWork.SaveChangesAsync();

                return isCustomerDeleted > 0;

            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public async Task<List<QueueEntry>> GetFilterCustomersAsync(string? customerName, DateTime? requestedTime)
        {
            try
            {
                var queueList = await _unitOfWork.Repository<QueueEntry>().GetAllAsync();

                return queueList.ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public async Task<(EnumResponse enumResponse, QueueEntry customerEntity)> UpdateCustomerAsync(CustomerModel customer)
        {
            try
            {
                var queueEntity = await _unitOfWork.FirstOrDefaultAsync<QueueEntry>(x => x.Id == customer.Id);
                var isCustomerAdded = 0;
                if (queueEntity != null)
                {
                    queueEntity.RequestedTime = customer.RequestedTime;
                    queueEntity.CreatedAt = DateTime.Now;

                    _unitOfWork.Repository<QueueEntry>().Update(queueEntity);

                    isCustomerAdded = await _unitOfWork.SaveChangesAsync();
                    return (EnumResponse.CustomerUpdate, queueEntity);

                }
            }
            catch (Exception e)
            {

                throw e; //Write the error to logger
            }

            return (EnumResponse.CustomerRequestNotFound, null);
        }
    }
}
