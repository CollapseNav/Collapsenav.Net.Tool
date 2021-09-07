using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Collapsenav.Net.Tool
{
    public class CntFileInfo
    {
        public CntFileInfo() { }
        public CntFileInfo(Stream stream, string filename)
        {
            Init(stream, filename);
        }
        public CntFileInfo(HttpResponseMessage response)
        {
            var nameHeader = response.Content.Headers
            .FirstOrDefault(item => item.Key == "Content-Disposition").Value?.FirstOrDefault();
            Init(response.Content.ReadAsStream(), nameHeader[(nameHeader.LastIndexOf("filename=") + 9)..]);
        }
        public static CntFileInfo Convert(HttpResponseMessage response)
        {
            return new CntFileInfo(response);
        }
        public void Init(Stream stream, string filename)
        {
            File = stream;
            FileName = filename;
            Size = File.Length.ToString();
            Ext = Path.GetExtension(FileName);
        }

        public async Task<bool> SaveToAsync(string path, bool checkExt = false)
        {
            if (checkExt && Path.GetExtension(path) != Ext)
                return false;
            using var fs = new FileStream(path, FileMode.OpenOrCreate);
            await File.CopyToAsync(fs);
            return true;

        }

        public Stream File { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public string Ext { get; set; }
        public string ContentType { get; set; }
    }
}
