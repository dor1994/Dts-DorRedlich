using CustomersBarBer.CustomerServices.Interfaces;
using Data.Enums;
using Data.Models;
using Data.Repositories.Interfaces;

namespace CustomersBarBer.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ApiResponse<CustomerModel, EnumResponse>> AddCustomerAsync(CustomerModel customer)
        {
            var result = await _customerRepository.AddCustomerAsync(customer);

            return new ApiResponse<CustomerModel, EnumResponse>
            {
                Status = result.enumResponse == EnumResponse.CustomerAdded,
                Message = result.enumResponse switch
                {
                    EnumResponse.CustomerAdded => "Customer added successfully!",
                    EnumResponse.DateRequestExist => "Failed to add customer - The date request already exists.",
                    _ => "An unknown error occurred while adding the customer."
                },
                Data = result.enumResponse == EnumResponse.CustomerAdded ? new CustomerModel
                {
                    Id = result.customerEntity.Id,
                    CustomerId = customer.CustomerId,
                    CreatedAt = result.customerEntity.CreatedAt,
                    CustomerName = customer.CustomerName,
                    RequestedTime = customer.RequestedTime
                } : null,
                EnumMessage = result.enumResponse
            };
        }

        public async Task<ApiResponse<bool, EnumResponse>> DeleteCustomerAsync(int id)
        {
            var isDeleted = await _customerRepository.DeleteCustomerAsync(id);

            return new ApiResponse<bool, EnumResponse>
            {
                Status = isDeleted,
                Message = isDeleted
                    ? "Customer deleted successfully!"
                    : "Failed to delete customer.",
                Data = isDeleted,
                EnumMessage = isDeleted ? EnumResponse.CustomerDeleted : EnumResponse.CustomerNotFound
            };
        }

        public async Task<ApiResponse<List<CustomerModel>, EnumResponse>> GetFilterCustomersAsync(string? customerName, DateTime? requestedTime)
        {
            var customers = await _customerRepository.GetFilterCustomersAsync(customerName, requestedTime);

            return new ApiResponse<List<CustomerModel>, EnumResponse>
            {
                Status = customers != null,
                Message = customers != null
                    ? "Customers retrieved successfully!"
                    : "No customers found.",
                Data = customers?.Select(dbModel => new CustomerModel
                {
                    Id = dbModel.Id,
                    CustomerId = dbModel.CustomerId,
                    CustomerName = dbModel.CustomerName,
                    CreatedAt = dbModel.CreatedAt,
                    RequestedTime = dbModel.RequestedTime
                }).ToList(),
                EnumMessage = customers != null && customers.Any()
                    ? EnumResponse.CustomersFound
                    : EnumResponse.CustomersNotFound
            };
        }

        public async Task<ApiResponse<CustomerModel, EnumResponse>> UpdateCustomerAsync(CustomerModel customer)
        {
            var result = await _customerRepository.UpdateCustomerAsync(customer);

            return new ApiResponse<CustomerModel, EnumResponse>
            {
                Status = result.enumResponse == EnumResponse.CustomerUpdate,
                Message = result.enumResponse switch
                {
                    EnumResponse.CustomerUpdate => "Customer updated successfully!",
                    EnumResponse.CustomerRequestNotFound => "Update failed - Customer not found.",
                    EnumResponse.DateRequestExist => "Failed to add customer - The date request already exists.",
                    _ => "An unknown error occurred during the update."
                },
                Data = result.enumResponse == EnumResponse.CustomerUpdate ? new CustomerModel
                {
                    Id = result.customerEntity.Id,
                    CreatedAt = result.customerEntity.CreatedAt,
                    CustomerName = customer.CustomerName,
                    RequestedTime = customer.RequestedTime
                } : null,
                EnumMessage = result.enumResponse
            };
        }
    }
}
