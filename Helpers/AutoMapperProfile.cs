using AutoMapper;
using CustomerApi.Models;
using CustomerApi.Models.DTO;

namespace CustomerApi.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AccountModel, AccountDTO>();
        CreateMap<AccountDTO, AccountModel>();
        CreateMap<CustomerAccountsDTO, CustomerModel>();
        CreateMap<CustomerModel, CustomerAccountsDTO>();
    }
}
