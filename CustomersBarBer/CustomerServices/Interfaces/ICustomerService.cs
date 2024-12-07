﻿using Data.Enums;
using Data.Models;

namespace CustomersBarBer.CustomerServices.Interfaces
{
    public interface ICustomerService
    {
        public Task<ApiResponse<List<CustomerModel>, EnumResponse>> GetFilterCustomersAsync(string? customerName, DateTime? requestedTime);
        public Task<ApiResponse<CustomerModel, EnumResponse>> AddCustomerAsync(CustomerModel customer);
        public Task<ApiResponse<bool, EnumResponse>> DeleteCustomerAsync(int id);
        public Task<ApiResponse<CustomerModel, EnumResponse>> UpdateCustomerAsync(CustomerModel customer);
    }
}
