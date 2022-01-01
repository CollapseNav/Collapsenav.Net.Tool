namespace Collapsenav.Net.Tool.Data.Test;
    public class TestEntity : BaseEntity<int>
    {
        public TestEntity() { }
        public TestEntity(int id, string code, int? number, bool? isTest)
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
