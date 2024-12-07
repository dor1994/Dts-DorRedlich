using AutoMapper;
using Data.DtoModels;
using Data.Models;

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
