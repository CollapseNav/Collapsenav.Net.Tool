using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Collapsenav.Net.Tool
{
    public static class HttpClientExt
    {
        /// <summary>
        ///  发送文件
        /// </summary>
        public static async Task<bool> SendFileAsync(this HttpClient client, string url, IEnumerable<CntFileInfo> files)
        {
            return await FileTool.SendAsync(url, files, client);
        }
        /// <summary>
        /// 发送文件
        /// </summary>
        public static async Task<bool> SendFileAsync(this HttpClient client, string url, CntFileInfo file)
        {
            return await FileTool.SendAsync(url, new[] { file }, client);
        }
        /// <summary>
        /// 获取文件
        /// </summary>
        public static async Task<CntFileInfo> GetFileAsync(this HttpClient client, string url)
        {
            return await FileTool.GetAsync(url, client);
        }
    }
}
