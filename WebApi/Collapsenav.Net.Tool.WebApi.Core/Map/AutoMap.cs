using AutoMapper;

namespace Collapsenav.Net.Tool.WebApi;

/// <summary>
/// 使用 automapper 进行map, 常规做法
/// </summary>
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