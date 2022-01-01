using AutoMapper;

namespace Collapsenav.Net.Tool.WebApi.Test;
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TestEntity, TestEntityCreate>().ReverseMap();
        }
}
