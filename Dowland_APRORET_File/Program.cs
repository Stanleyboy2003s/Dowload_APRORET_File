// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


#region 單個檔案下載測試
//DownloadPath downloadPath = new DownloadPath()
//{
//    IsInSeason = false,
//    ROCYear = 108,
//    Season = Season.S2,
//    CityNotation = CityNotation.B,
//    TransactionType = TransactionType.A,
//};
//string path = @$".\Test\{downloadPath.ROCYear}{downloadPath.Season}";
//string filename = $"{downloadPath.CityNotation}_lvr_land_{downloadPath.TransactionType}";
//await DownloadFileAciton.DownloadSingleFileAsync(downloadPath, path, filename, FileExtension.xml);
#endregion

#region 多個檔案下載測試
var SeasonList = Enum.GetValues<Season>();
var CityNotationList = Enum.GetValues<CityNotation>();
var TransactionTypeList = Enum.GetValues<TransactionType>();
var Datalist = DownloadPath.DownloadPathFactory(105, 105, SeasonList, CityNotationList, TransactionTypeList, FileExtension.csv);
await DownloadFileAciton.DownloadFilesAsync(Datalist);
Console.WriteLine("Download End");
#endregion