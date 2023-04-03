namespace Collapsenav.Net.Tool.Excel;

public class MulitSheetsExportConfigItem
{
    public MulitSheetsExportConfigItem(string sheetName, IEnumerable<object> data, ExportConfig config)
    {
        SheetName = sheetName;
        Data = data;
        Config = config;
    }

    public string SheetName { get; set; }
    public IEnumerable<object> Data { get; set; }
    public ExportConfig Config { get; set; }
}