using Data.DtoModels;
using Data.Enums;
using Data.Models;
using Data.Repositories.Interfaces;

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
                var queueList = await _unitOfWork.GetAllQueueEntriesAsync(customerName, requestedTime);

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
                // Query both the existing customer and the requested time conflict in a single call
                var queueEntities = await _unitOfWork.Repository<QueueEntry>().GetAllAsync(x =>
                    x.Id == customer.Id || x.RequestedTime == customer.RequestedTime);

                // Check if the requested time already exists
                if (queueEntities.Any(x => x.RequestedTime == customer.RequestedTime))
                {
                    return (EnumResponse.DateRequestExist, null);
                }

                // Find the entity to update
                var queueEntity = queueEntities.FirstOrDefault(x => x.Id == customer.Id);

                if (queueEntity != null)
                {
                    // Update the entity
                    queueEntity.RequestedTime = customer.RequestedTime;
                    queueEntity.CreatedAt = DateTime.Now;

                    _unitOfWork.Repository<QueueEntry>().Update(queueEntity);
                    var isCustomerAdded = await _unitOfWork.SaveChangesAsync();

                    if (isCustomerAdded > 0)
                    {
                        return (EnumResponse.CustomerUpdate, queueEntity);
                    }
                }
            }
            catch (Exception e)
            {
                // Log the exception
                throw new Exception("Error occurred while updating the customer.", e);
            }

            return (EnumResponse.CustomerRequestNotFound, null);
        }
    }
}
