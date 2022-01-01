using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
public interface IExcelExportController<T, GetT> : IController
{
    Task<FileStreamResult> ExportExcelAsync([FromQuery] GetT input);
}
