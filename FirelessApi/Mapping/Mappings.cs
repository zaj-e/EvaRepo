using FirelessApi.Controllers.DataObjects;
using FirelessApi.Domain;
using AutoMapper;

namespace FirelessApi.Mapping;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<NewUser, User>();
        CreateMap<User, User>();
        CreateMap<Data, Data>();
        CreateMap<Alert, Alert>();
        CreateMap<Region, Region>();
    }
}