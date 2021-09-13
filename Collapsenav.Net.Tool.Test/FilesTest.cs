using Xunit;
using System.Threading.Tasks;
using System.IO;

namespace Collapsenav.Net.Tool.Test
{
    public class FilesTest
    {
        [Fact]
        public async Task GetFileTest()
        {
            var dd = await FileTool.GetAsync(@"http://202.182.125.80:9090/api/File/download/2bca1ba9-19b3-4691-ba46-385d91aef7d7");
            var result = await dd.SaveToAsync("/index.html", true);

            using var fs1 = new FileStream("/test.mp3", FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
            var md5dd = dd.File.Md5En();
            var md5fs = fs1.Md5En();
            Assert.False(md5dd == md5fs);
        }
    }
}

