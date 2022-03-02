using AutoMapper;

namespace Collapsenav.Net.Tool.WebApi;

public class AutoMap : IMap
{
    private readonly IMapper Mapper;
    public AutoMap(IMapper mapper)
    {
        Mapper = mapper;
    }
    public T Map<T>(object obj)
    {
        return Mapper.Map<T>(obj);
    }
}