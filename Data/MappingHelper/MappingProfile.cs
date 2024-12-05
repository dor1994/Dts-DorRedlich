using AutoMapper;
using Data.DtoModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MappingHelper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, User>();
            CreateMap<CustomerModel, QueueEntry>();
        }
    }
}
