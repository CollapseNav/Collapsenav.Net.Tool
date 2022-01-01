namespace Collapsenav.Net.Tool.WebApi.Test;
    public class TestEntityCreate : BaseCreate<TestEntity>
    {
        public string Code { get; set; }
        public int? Number { get; set; }
        public bool? IsTest { get; set; }
        public TestEntityCreate(string code, int? number, bool? isTest)
        {
            Code = code;
            Number = number;
            IsTest = isTest;
        }
}
