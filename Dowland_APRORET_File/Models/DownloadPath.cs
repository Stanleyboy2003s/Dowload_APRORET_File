namespace Dowload_APRORET_File.Models;
public class DownloadPath 
{
    #region Property and Field
    private int _rocYear;
    public bool IsInSeason { get;  set; }
    public int ROCYear
    {
        get
        {
            return _rocYear;
        }
        set
        {
            if (value <= DateTime.UtcNow.Year || value >= DateTime.UtcNow.Year - 10)
            {
                _rocYear = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("輸入的年份超過範圍");
            }
        }
    }
    public Season Season { get;  set; }
    public CityNotation CityNotation { get;  set; }
    public TransactionType TransactionType { get;  set; }
    public FileExtension FileExtension { get;  set; }
    #endregion

    #region Method
    /// <summary>
    /// 取得實價登入下載路徑中的檔名(不包括副檔名)
    /// </summary>
    /// <returns></returns>
    public string GetFilename()
    {
        return $"{CityNotation}_lvr_land_{TransactionType}";
    }
    /// <summary>
    /// 取得預設的檔案下載路徑，路徑為程式集\Download\民國時間\四季
    /// </summary>
    /// <returns>返回字串，名稱等同創建的資料夾名稱</returns>
    public string GetDefaultPath()
    {
        return @$"./Download/{ROCYear}/{Season}";
    }

    /// <summary>
    /// 取得實價登入之檔案下載路由的網址
    /// </summary>
    /// <returns>返回Uri?類型的網址， 如果有異常則返回null</returns>
    /// <exception cref="NullReferenceException"></exception>
    public Uri? GetDownFileUrl()
    {
        string host = "plvr.land.moi.gov.tw";
        string scheme = "https";
        string path = string.Empty;
        string querystring = string.Empty;
        Uri? uri = null;

        try
        {
            if (IsInSeason == true)
            {
                path = "Download";
                querystring = $"fileName={CityNotation}_lvr_land_{TransactionType}.{FileExtension}";
            }
            else
            {
                path = "DownloadSeason";
                querystring = $"season={ROCYear}{Season}";
                querystring += $"&fileName={CityNotation}_lvr_land_{TransactionType}.{FileExtension}";
            }
            uri = DownloadPath.UriFactory(scheme, host, path, querystring);
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException("下載路徑不得為空值");
        }
        catch (Exception)
        {
            throw;
        }
        return uri;

    }

    /// <summary>
    /// 取得實價登入之檔案下載路由的網址集合，返回值是IEnumerable的DownloadPath
    /// </summary>
    /// <param name="MinRocYear">要下載檔案的年份範圍最小值</param>
    /// <param name="MaxRocYear">要下載檔案的年份範圍最大值，如果為當年份，則最後會下載到最新季節</param>
    /// <param name="Scollection">要下載檔案的季節集合，只下載第一季則集合放入Season.S1即可，若要下載二三季則集合中需添加Season.S2, Season.S3，以此類推</param>
    /// <param name="CNcollection">要下載檔案的城市集合</param>
    /// <param name="TTcollection">要下載檔案的交易總類集合</param>
    /// <param name="extension">要下載的檔案之格式</param>
    /// <returns>返回DownloadPath的集合，依據參數將各屬性賦值決定</returns>
    public static IEnumerable<DownloadPath> DownloadPathFactory(int MinRocYear, int MaxRocYear, IEnumerable<Season> Scollection,
           IEnumerable<CityNotation> CNcollection, IEnumerable<TransactionType> TTcollection, FileExtension extension)
    {
        var downloadPaths = new List<DownloadPath>();
        // 時間長度以年為單位
        int duration = MaxRocYear - MinRocYear + 1;
        // 季節總數
        int SeasonsCount = Scollection.Count();
        // 台灣縣市的總數
        int CNsCount = CNcollection.Count();
        // 不動產交易的總類項目
        int TTsCount = TTcollection.Count();
        //換算成今年度的中華民國
        int RocThisYr = DateTime.Now.Year - 1911;
        //總共要下載檔案的數量，由要輸入要下載的年份長度、四季長度、台灣城市數量、交易的品項數量決定。
        int listLength = duration * SeasonsCount * CNsCount * TTsCount;

        //決定
        int Count = 0;

        //根據交易方式及輸入時間決定今年度的目前截止季節
        Season A_LastSeason = DownloadPath.DecideLastSeason(DateTime.Now, TransactionType.A);
        Season BC_LastSeason = DownloadPath.DecideLastSeason(DateTime.Now, TransactionType.B);

        //時間輸入判斷正確與否
        if (MaxRocYear < MinRocYear)
        {
            Console.WriteLine("年份範圍輸入相反");
            return downloadPaths;
        }
        else if (MinRocYear < RocThisYr - 10)
        {
            Console.WriteLine("年分範圍的最小值超過本年分減去十年");
            return downloadPaths;
        }
        else if (MaxRocYear > RocThisYr)
        {
            Console.WriteLine("年分範圍的最大值超過本年度");
            return downloadPaths;
        }

        try
        {
            for (int i = 0; i < listLength; i++)
            {
                DownloadPath downloadPath = new DownloadPath();

                downloadPath.FileExtension = extension;
                downloadPath.ROCYear = MinRocYear + (i / (SeasonsCount * TTsCount * CNsCount));

                if (SeasonsCount == 1) downloadPath.Season = Scollection.ElementAt(0);
                else downloadPath.Season = Scollection.ElementAt((i / (TTsCount * CNsCount)) % SeasonsCount);

                if (CNsCount == 1) downloadPath.CityNotation = CNcollection.ElementAt(0);
                else downloadPath.CityNotation = CNcollection.ElementAt((i / TTsCount) - CNsCount * (i / (CNsCount * TTsCount)));

                if (TTsCount == 1) downloadPath.TransactionType = TTcollection.ElementAt(0);
                else downloadPath.TransactionType = TTcollection.ElementAt(i % TTsCount);

                if (downloadPath.ROCYear == RocThisYr)
                {
                    if (downloadPath.TransactionType == TransactionType.A && downloadPath.Season == A_LastSeason)
                    {
                        downloadPath.IsInSeason = true;
                        Count++;
                    }
                    else if ((downloadPath.TransactionType == TransactionType.B || downloadPath.TransactionType == TransactionType.C)
                             && downloadPath.Season == BC_LastSeason)
                    {
                        downloadPath.IsInSeason = true;
                        Count++;
                    }
                    else downloadPath.IsInSeason = false;
                }
                else downloadPath.IsInSeason = false;
                downloadPaths.Add(downloadPath);

                if (Count == CNsCount * TTsCount) break;
            }
            return downloadPaths;

        }
        catch (Exception)
        {
            throw;
        }
    }
    public static Uri UriFactory(string scheme, string host, string path, string query)
    {
        UriBuilder uriBuilder = new UriBuilder()
        {
            Scheme = scheme,
            Host = host,
            Path = path,
            Query = query
        };
        return uriBuilder.Uri;
    }


    /// <summary>
    /// 判定輸入日期的截止季節
    /// </summary>
    /// <param name="dateTime">輸入判定的日期</param>
    /// <param name="transactionType">輸入交易方式</param>
    /// <returns>輸入日期的季節，類型是Season，若輸入日期非當年日期，則返回Season.S1</returns>
    public static Season DecideLastSeason(DateTime dateTime, TransactionType transactionType)
    {
        int thisYear = DateTime.Now.Year;
        //預售屋買賣和不動產租賃的第一季截止時間為當年度2/10(2/10也算S1)
        DateTime BC_BaseDateLimit = new DateTime(thisYear, 2, 10);
        //不動產買賣第一季截止時間為當年度3/10(3/10也算S1)
        DateTime A_BaseDateLimit = BC_BaseDateLimit.AddMonths(1);
        //預售屋買賣和不動產租賃的二三季截止時間為5/10(S2)和8/10(S3)
        DateTime[] BC_DateLimits = { BC_BaseDateLimit, BC_BaseDateLimit.AddMonths(3), BC_BaseDateLimit.AddMonths(6) };
        //不動產買賣的二三季截止時間為6/10(S2)8/10(S3)
        DateTime[] A_DateLimits = { A_BaseDateLimit, A_BaseDateLimit.AddMonths(3), A_BaseDateLimit.AddMonths(6) };

        //判斷輸入參數的時間為非當年份，則返回S1
        if (dateTime.Year != thisYear)
        {
            Console.WriteLine("輸入的時間非當季年份");
            return Season.S1;
        }

        if (transactionType == TransactionType.B || transactionType == TransactionType.C)
        {
            if (dateTime.CompareTo(BC_DateLimits[0]) <= 0) return Season.S1;
            else if (dateTime.CompareTo(BC_DateLimits[1]) <= 0) return Season.S2;
            else if (dateTime.CompareTo(BC_DateLimits[2]) <= 0) return Season.S3;
            else return Season.S4;
        }
        else
        {
            if (dateTime.CompareTo(A_DateLimits[0]) <= 0) return Season.S1;
            else if (dateTime.CompareTo(A_DateLimits[1]) <= 0) return Season.S2;
            else if (dateTime.CompareTo(A_DateLimits[2]) <= 0) return Season.S3;
            else return Season.S4;
        }
        //else
        //{
        //    Console.WriteLine($"{transactionType} is a null");
        //    return null;
        //}                
    }
    #endregion
}
