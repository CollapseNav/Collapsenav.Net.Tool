using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi.Test
{
    public class TestTwoEntity : BaseEntity<int>
    {
        public TestTwoEntity() { }
        public TestTwoEntity(int id, string code, int? number, bool? isTest)
        {
            Id = id;
            Code = code;
            Number = number;
            IsTest = isTest;
        }
        public string Code { get; set; }
        public int? Number { get; set; }
        public bool? IsTest { get; set; }
    }
}
