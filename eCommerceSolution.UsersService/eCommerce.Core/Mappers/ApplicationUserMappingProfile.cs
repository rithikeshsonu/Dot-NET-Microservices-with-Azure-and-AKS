using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Mappers
{
    public class ApplicationUserMappingProfile : Profile
    {
        public ApplicationUserMappingProfile()
        {
            CreateMap<ApplicationUser, AuthenticationResponse>()
                .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.PersonName, options => options.MapFrom(src => src.PersonName))
                .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Success, options => options.Ignore())
                .ForMember(dest => dest.Token, options => options.Ignore())
            ;
        }
    }
}
