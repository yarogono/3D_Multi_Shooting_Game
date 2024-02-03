using AccountServer.Model;
using AutoMapper;

namespace AccountServer.Mapper
{
    public class AccountProfile : Profile
    {
        public AccountProfile() 
        {
            CreateMap<Account, AccountLoginReqDto>().ReverseMap();
            CreateMap<Account, AccountLoginResDto>().ReverseMap();

            CreateMap<Account, AccountSignupReqDto>().ReverseMap();
            CreateMap<Account, AccountSignupResDto>().ReverseMap();
        }
    }
}
