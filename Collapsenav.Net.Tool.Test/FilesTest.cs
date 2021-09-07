using Xunit;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Collapsenav.Net.Tool.Test
{
    public class FilesTest
    {
        [Fact]
        public async Task GetFileTest()
        {
            var dd = await Files.GetAsync(@"http://202.182.125.80:9090/api/File/download/f63890ab-4b3b-4163-a03b-ad389be50b08");
            var result = await dd.SaveToAsync("/test.mp3", true);
        }
    }
}

