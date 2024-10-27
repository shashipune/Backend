using AutoMapper;
using HomeAssignment.API.Models;
using HomeAssignment.Core;

namespace HomeAssignment.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ContactVM, Contact>()
                .ReverseMap();
        }
    }
}
