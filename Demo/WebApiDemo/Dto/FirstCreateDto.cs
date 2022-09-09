using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo;


public class FirstCreateDto : BaseCreate<FirstEntity>
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}