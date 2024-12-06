using Azure;
using CustomersBarBer.CustomerServices.Interfaces;
using Data.DtoModels;
using Data.Enums;
using Data.Models;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ApiResponse<CustomerModel, EnumResponse> response = new ApiResponse<CustomerModel, EnumResponse>();

            var result = await _customerRepository.AddCustomerAsync(customer);

            switch (result.enumResponse)
            {
                case EnumResponse.CustomerAdded:
                    customer.Id = result.customerEntity.Id;
                    customer.CreatedAt = result.customerEntity.CreatedAt;
                    response.Status = true;
                    response.Message = "Customer Added successfully!";
                    response.Data = customer;
                    response.EnumMessage = EnumResponse.CustomerAdded;
                    break;
                case EnumResponse.DateRequestExist:
                    response.Status = false;
                    response.Message = "Added customer failed - The Date Request Already Exist";
                    response.EnumMessage = EnumResponse.DateRequestExist;
                    break;

                default:
                    break;
            }

            return response;

        }

        public async Task<ApiResponse<bool, EnumResponse>> DeleteCustomerAsync(int id)
        {
            ApiResponse<bool, EnumResponse> response = new ApiResponse<bool, EnumResponse>();

            var isDelete = await _customerRepository.DeleteCustomerAsync(id);

            if (isDelete)
            {
                response.Status = isDelete;
                response.Data = isDelete;
                response.Message = "Delete successfully";
            }

            else
            {
                response.Status = isDelete;
                response.Data = isDelete;
                response.Message = "Failed to delete queue";
            }

            return response;
        }

        public async Task<ApiResponse<List<CustomerModel>, EnumResponse>> GetFilterCustomersAsync(string? customerName, DateTime? requestedTime)
        {
            var queueList = await _customerRepository.GetFilterCustomersAsync(customerName, requestedTime);

            ApiResponse<List<CustomerModel>, EnumResponse> response = new ApiResponse<List<CustomerModel>, EnumResponse>();

            if(queueList != null)
            {
                List<CustomerModel> clientRestaurants = queueList
                                .Select(dbModel => new CustomerModel
                                {
                                    Id = dbModel.Id,
                                    CustomerId = dbModel.CustomerId,
                                    CustomerName = dbModel.CustomerName,
                                    CreatedAt = dbModel.CreatedAt,
                                    RequestedTime = dbModel.RequestedTime,
                                })
                                .ToList();

                response.Status = true;
                response.Message = "successfully!";
                response.Data = clientRestaurants;

                return response;

            }

            response.Status = false;
            response.Message = "Somthing get worng!";
            response.Data = null;

            return response;
        }

        public async Task<ApiResponse<CustomerModel, EnumResponse>> UpdateCustomerAsync(CustomerModel customer)
        {
            ApiResponse<CustomerModel, EnumResponse> response = new ApiResponse<CustomerModel, EnumResponse>();

            var result = await _customerRepository.UpdateCustomerAsync(customer);

            switch (result.enumResponse)
            {
                case EnumResponse.CustomerUpdate:
                    customer.CreatedAt = result.customerEntity.CreatedAt;
                    response.Status = true;
                    response.Message = "Customer Update successfully!";
                    response.Data = customer;
                    response.EnumMessage = EnumResponse.CustomerAdded;
                    break;
                case EnumResponse.CustomerRequestNotFound:
                    response.Status = false;
                    response.Message = "Update customer failed - Clould not found original queue";
                    response.EnumMessage = EnumResponse.CustomerRequestNotFound;
                    break;

                default:
                    break;
            }

            return response;
        }
    }
}
