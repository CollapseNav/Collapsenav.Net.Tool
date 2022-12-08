using System.ComponentModel.DataAnnotations;
using Collapsenav.Net.Tool.Data;

namespace DataDemo.EntityLib;

public class FirstEntity : Entity<long>
{
    // [Key]
    // public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}