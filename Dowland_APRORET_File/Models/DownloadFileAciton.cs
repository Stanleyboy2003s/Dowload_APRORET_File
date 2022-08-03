namespace Dowload_APRORET_File.Models;

public class DownloadFileAciton
{
    /// <summary>
    /// 使用非同步下載單一個實價登入的交易檔案，可以自行定義下載路徑與檔案名稱。
    /// </summary>
    /// <param name="downloadPath">實價登入檔案Url的設定類別實例</param>
    /// <param name="SavePath">下載的存放路徑</param>
    /// <param name="SaveFilename">下載的檔案名稱</param>
    /// <param name="extension">下載檔案之副檔名</param>
    /// <returns></returns>
    public static async Task DownloadSingleFileAsync(DownloadPath downloadPath, string SavePath, string SaveFilename, FileExtension extension)
    {
        HttpClient client = new HttpClient();
        FileStream fs = null;
        if (Directory.Exists(SavePath) == false)
        {
            Directory.CreateDirectory(SavePath);
        }

        string filePath = Path.Combine(SavePath, SaveFilename+'.'+extension.ToString());
        if (File.Exists(filePath) == false)
        {
            fs = File.Create(filePath);
            fs.Dispose();
        }

        try
        {
            using (fs = File.OpenWrite(filePath))
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, downloadPath.GetDownFileUrl());
                HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
                var ContentType = responseMessage.Content.Headers.ContentType;

                if (ContentType != null && ContentType.MediaType == "application/octet-stream")
                {
                    using (var ResContent = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        await ResContent.CopyToAsync(fs);
                    }
                }
            }          
        }
        catch (Exception)
        {
            throw;
        }
    }
    /// <summary>
    /// 使用預設存放路徑來下載實價登入之交易檔案，預設路徑為:./Download/{ROCYear}/{Season}/{CityNotation}_lvr_land_{TransactionType}
    /// </summary>
    /// <param name="downloadPath">實價登入檔案Url的設定</param>
    /// <returns></returns>
    public static async Task DownloadSingleFileAsync(DownloadPath downloadPath)
    {
        string SavePath = downloadPath.GetDefaultPath();
        string filename = downloadPath.GetFilename();
        await DownloadFileAciton.DownloadSingleFileAsync(downloadPath, SavePath, filename, downloadPath.FileExtension);
    }
    /// <summary>
    /// 使用預設存放路徑來存放多個檔案，預設路徑為:./Download/{ROCYear}/{Season}/{CityNotation}_lvr_land_{TransactionType}
    /// </summary>
    /// <param name="downloadPaths">實價登入檔案Url的設定</param>
    /// <returns></returns>
    public static async Task DownloadFilesAsync(IEnumerable<DownloadPath> downloadPaths)
    {
        try
        {
            foreach (var downloadPath in downloadPaths)
            {
                await DownloadSingleFileAsync(downloadPath);
                Console.WriteLine($"{downloadPath.GetDefaultPath()}/{downloadPath.GetFilename()} 已下載完畢");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

}
