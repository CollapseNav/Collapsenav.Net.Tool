using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Collapsenav.Net.Tool.Excel.Test;
public class ExcelTest
{
    [Fact]
    public async Task HeaderTest()
    {
        using var fs = new FileStream("./TestExcel.xlsx", FileMode.Open, FileAccess.Read, FileShare.Read);
        var data = await ReadConfig<ExcelDefaultDto>.ExcelToEntityAsync(fs);
        Assert.True(data.Count() == 3000);
    }
}
