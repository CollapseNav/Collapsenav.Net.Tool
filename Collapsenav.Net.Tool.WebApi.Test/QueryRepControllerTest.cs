using System;
using System.Linq;
using System.Threading.Tasks;
using Collapsenav.Net.Tool.Data;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.WebApi.Test;
    public class QueryControllerTest
    {

        protected readonly IServiceProvider Provider;
        protected readonly IQueryRepController<int, TestEntity, TestEntityGet> RepController;
        public QueryControllerTest()
        {
            Provider = DIConfig.GetProvider();
            RepController = GetService<IQueryRepController<int, TestEntity, TestEntityGet>>();
        }
        protected T GetService<T>()
        {
            return Provider.GetService<T>();
        }
        [Fact]
        public async Task QueryTest()
        {
            var data = await RepController.FindQueryAsync(new TestEntityGet { });
            Assert.True(data.Count() == 10);
            data = await RepController.FindQueryAsync(new TestEntityGet { Id = 2 });
            Assert.True(data.Count() == 1);
            data = await RepController.FindQueryAsync(new TestEntityGet { Number = 2333 });
            Assert.True(data.Count() == 3);
            data = await RepController.FindQueryAsync(new TestEntityGet { Code = "2333" });
            Assert.True(data.Count() == 8);
            data = await RepController.FindQueryAsync(new TestEntityGet { IsTest = false });
            Assert.True(data.Count() == 3);
            data = await RepController.FindQueryAsync(new TestEntityGet
            {
                Number = 2333,
                Code = "2333",
                IsTest = false
            });
            Assert.True(data.Count() == 1);
        }

        [Fact]
        public async Task PageTest()
        {
            var data = await RepController.FindPageAsync(new TestEntityGet { });
            Assert.True(data.Length == 10);
            data = await RepController.FindPageAsync(new TestEntityGet { Id = 2 });
            Assert.True(data.Length == 1);
            data = await RepController.FindPageAsync(new TestEntityGet { Number = 2333 });
            Assert.True(data.Length == 3);
            data = await RepController.FindPageAsync(new TestEntityGet { Code = "2333" });
            Assert.True(data.Length == 8);
            data = await RepController.FindPageAsync(new TestEntityGet { IsTest = false });
            Assert.True(data.Length == 3);
            data = await RepController.FindPageAsync(new TestEntityGet
            {
                Number = 2333,
                Code = "2333",
                IsTest = false
            });
            Assert.True(data.Length == 1);
            data = await RepController.FindPageAsync(new TestEntityGet { Code = "2333" }, new PageRequest { Max = 3 });
            Assert.True(data.Length == 3);
            data = await RepController.FindPageAsync(new TestEntityGet { Code = "2333" }, new PageRequest { Index = 2, Max = 3 });
            Assert.True(data.Length == 3);
            data = await RepController.FindPageAsync(new TestEntityGet { Code = "2333" }, new PageRequest { Index = 3, Max = 3 });
            Assert.True(data.Length == 2);
        }

        [Fact]
        public async Task FindByIdTest()
        {
            var data = await RepController.FindAsync(5);
            Assert.True(data.Code == "233345");
            var datas = await RepController.FindByIdsAsync(new[] { 7, 8, 9, 10 });
            Assert.True(datas.All(item => item.Number == 2333));
            datas = await RepController.FindByIdsPostAsync(new[] { 3, 6, 7 });
            Assert.True(datas.All(item => item.IsTest == false));
        }
    }


