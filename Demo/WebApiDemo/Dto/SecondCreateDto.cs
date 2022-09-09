using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo;


public class SecondCreateDto : BaseCreate<SecondEntity>
{
    public string Name { get; set; }
    public int? Age { get; set; }
    public string Description { get; set; }
}