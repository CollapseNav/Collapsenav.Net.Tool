namespace Collapsenav.Net.Tool.Excel;

public class MulitSheetsExportConfig
{
    protected MulitSheetsExportConfig()
    {
        Datas = new();
    }
    public MulitSheetsExportConfig(Dictionary<string, IEnumerable<object>> datas)
    {
        BuildExportConfig(datas);
    }
    public List<MulitSheetsExportConfigItem> Datas { get; set; }
    private void BuildExportConfig(Dictionary<string, IEnumerable<object>> dicts)
    {
        Datas = dicts.Select(item =>
        {
            return new MulitSheetsExportConfigItem(item.Key, item.Value, ExportConfig.InitByExportConfig(ExportConfig.GenDefaultConfig(item.Value)));
        }).ToList();
    }

    public async Task<ISheetCellReader> ExportToSheetCellReader(ISheetCellReader reader)
    {
        foreach (var item in Datas)
            await item.Config.ExportAsync(reader[item.SheetName]);
        return reader;
    }

    public static MulitSheetsExportConfig Build<T>(string sheetName, IEnumerable<T> datas, ExportConfig<T> config) where T : class
    {
        var exportConfig = new MulitSheetsExportConfig();
        exportConfig.Datas.Add(new MulitSheetsExportConfigItem(sheetName, datas, ExportConfig.InitByExportConfig(config)));
        return exportConfig;
    }

    public MulitSheetsExportConfig Add<T>(string sheetName, IEnumerable<T> datas, ExportConfig<T> config) where T : class
    {
        Datas.Add(new MulitSheetsExportConfigItem(sheetName, datas, ExportConfig.InitByExportConfig(config)));
        return this;
    }
}