這是一個控制台程式，可以將內政部實價登入上的交易資料下載下來：[實價登入網址](https://plvr.land.moi.gov.tw/DownloadOpenData "實價登入網址") 
> ## Dowload_APRORET_File.
> 
> 1.   DownloadPath：主要功用為建置各個實價登入檔案下載路徑之參數，包括是否為當期下載、交易的季節、交易種類、以及交易的城市，可以自行創建單一實例並初始化給各屬性賦值，也可以使用工廠方法來創建實例集合。.
> 2.   DownloadFileAciton：提供不同靜態下載方法，有單一檔案下載的DownloadSingleFileAsync和可以下載多個檔案的DownloadFilesAsync，輸入參數都是DownloadPath之實例。
> 
>這裡演示單一檔案與多個檔案的下載:
> <p>以下範例程式演示下載108S2的台中市不動產交易紀錄，下載路徑為：執行檔目錄\Test\108\S2\B_lvr_land_A.xml</p>
<pre><code>
　　　DownloadPath downloadPath = new DownloadPath()
      { 
        IsInSeason = false,
        ROCYear = 108,
        Season = Season.S2,
        CityNotation = CityNotation.B,
        TransactionType = TransactionType.A,
      };
     string path = @$".\Test\{downloadPath.ROCYear}{downloadPath.Season}";
     string filename = $"{downloadPath.CityNotation}_lvr_land_{downloadPath.TransactionType}";
     await DownloadFileAciton.DownloadSingleFileAsync(downloadPath, path, filename, FileExtension.xml);
</code></pre>


> <p>以下範例程式演示下載105年的全台灣所有城市和交易類別，下載路徑為：執行檔目錄\Download\105\{Season}\{CityNotation}_lvr_land_{TransactionType}.csv</p>
<pre><code>
     var SeasonList = Enum.GetValues<Season>();
     var CityNotationList = Enum.GetValues<CityNotation>();
     var TransactionTypeList = Enum.GetValues<TransactionType>();
     var Datalist = DownloadPath.DownloadPathFactory(105, 105, SeasonList, CityNotationList, TransactionTypeList, FileExtension.csv);
     await DownloadFileAciton.DownloadFilesAsync(Datalist);
</code></pre>












