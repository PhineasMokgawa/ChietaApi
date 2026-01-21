using AutoMapper;
using CHIETAMIS.Authorization.Users;
using CHIETAMIS.People;
using CHIETAMIS.People.Dtos;
using CHIETAMIS.Organisations;
using CHIETAMIS.Organisations.Dtos;

namespace CHIETAMIS.Users.Dto
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore());

            CreateMap<CreateUserDto, User>();
            CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());
        }
    }
}
