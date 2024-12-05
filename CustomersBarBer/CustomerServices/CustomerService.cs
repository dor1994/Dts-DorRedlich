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
                    response.Status = true;
                    response.Message = "User Login successfully!";
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


            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool, EnumResponse>> DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<CustomerModel>, EnumResponse>> GetAllCustomersAsync()
        {
            var queueList = _customerRepository.GetAllCustomersAsync();
            throw new NotImplementedException();
        }

        public Task<ApiResponse<CustomerModel, EnumResponse>> UpdateCustomerAsync(CustomerModel customer)
        {
            throw new NotImplementedException();
        }
    }
}
