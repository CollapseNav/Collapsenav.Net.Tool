using AutoMapper;

namespace Collapsenav.Net.Tool.WebApi.Test;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TestEntity, TestEntityCreate>().ReverseMap();
        CreateMap<TestQueryEntity, TestQueryEntityCreate>().ReverseMap();
        CreateMap<TestModifyEntity, TestModifyEntityCreate>().ReverseMap();

        CreateMap<TestNotBaseEntity, TestNotBaseEntityCreate>().ReverseMap();
        CreateMap<TestNotBaseQueryEntity, TestNotBaseQueryEntityCreate>().ReverseMap();
        CreateMap<TestNotBaseModifyEntity, TestNotBaseModifyEntityCreate>().ReverseMap();
    }
}
