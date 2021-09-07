using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Collapsenav.Net.Tool
{
    public class Files
    {
        public static async Task<CntFileInfo> GetAsync(string url, HttpClient client = null)
        {
            client ??= new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>())
            };

            request.Headers.Add("accept", "application/json");

            var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode ? CntFileInfo.Convert(response) : throw new Exception("Faild");
        }

        public static async Task<bool> SendAsync(string url, IEnumerable<CntFileInfo> files, HttpClient client = null)
        {
            var formData = new MultipartFormDataContent();
            foreach (var file in files)
                formData.Add(new StreamContent(file.File), "file", file.FileName);
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = formData
            };
            request.Headers.Add("accept", "application/json");

            var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode ? true : throw new Exception("Faild");
        }
    }
}
