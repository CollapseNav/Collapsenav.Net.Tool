using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IExcelExportController<T, GetT> : IController
{
    Task<FileStreamResult> ExportExcelAsync([FromQuery] GetT input);
}

#region 无泛型约束
public interface INoConstraintsExcelExportController<T, GetT> : INoConstraintsController
{
    Task<FileStreamResult> ExportExcelAsync([FromQuery] GetT input);
}
#endregion
