namespace Dowload_APRORET_File;

public class EnumData
{
    public enum TransactionXmlNode
    {
        /// <summary>
        /// 根結點
        /// </summary>
        lvr_land,
        /// <summary>
        /// 不動產買賣 (TransactionType.A) 第一個子節點
        /// </summary>
        買賣,
        /// <summary>
        /// 預售屋買賣 (TransactionType.B) 第一個子節點
        /// </summary>
        預售屋,
        /// <summary>
        /// 不動產租賃 (TransactionType.C) 第一個子節點
        /// </summary>
        租賃,
    }

    public enum Season
    {
        /// <summary>
        /// 第一季
        /// </summary>
        S1,
        /// <summary>
        /// 第二季
        /// </summary>
        S2,
        /// <summary>
        /// 第三季
        /// </summary>
        S3,
        /// <summary>
        /// 第四季
        /// </summary>
        S4,
    }
    public enum TransactionType
    {
        /// <summary>
        /// 不動產買賣
        /// </summary>
        A,
        /// <summary>
        /// 預售屋買賣
        /// </summary>
        B,
        /// <summary>
        /// 不動產租賃
        /// </summary>
        C,
    }
    public enum CityNotation
    {
        /// <summary>
        /// Keelung (基隆)
        /// </summary>
        C,
        /// <summary>
        /// Taipei (台北)
        /// </summary>
        A,
        /// <summary>
        /// NewTaipei (新北)
        /// </summary>
        F,
        /// <summary>
        /// Taoyuan (桃園)
        /// </summary>
        H,
        /// <summary>
        /// HsinchuCity (新竹市)
        /// </summary>
        O,
        /// <summary>
        /// HsinchuCountry (新竹縣) 
        /// </summary>
        J,
        /// <summary>
        /// Miaoli (苗栗) 
        /// </summary>
        K,
        /// <summary>
        /// Taichung (台中)
        /// </summary>
        B,
        /// <summary>
        /// Nantou (南投)
        /// </summary>
        M,
        /// <summary>
        /// Changhua (彰化)
        /// </summary>
        N,
        /// <summary>
        /// Yunlin (雲林)
        /// </summary>
        P,
        /// <summary>
        /// ChiayiCity (嘉義市)
        /// </summary>
        I,
        /// <summary>
        /// ChiayiCountry (嘉義縣)
        /// </summary>
        Q,
        /// <summary>
        /// Tainan (台南)
        /// </summary>
        D,
        /// <summary>
        /// Kaohsiung (高雄)
        /// </summary>
        E,
        /// <summary>
        /// Pingtung (屏東)
        /// </summary>
        T,
        /// <summary>
        /// Yilan (宜蘭)
        /// </summary>
        G,
        /// <summary>
        /// Hualien (花蓮)
        /// </summary>
        U,
        /// <summary>
        /// Taitung (台東)
        /// </summary> 
        V,
        /// <summary>
        /// Penghu (澎湖)
        /// </summary>
        X,
        /// <summary>
        /// Kinmen (金門) 
        /// </summary>
        W,
        /// <summary>
        /// Lienchiang (連江)
        /// </summary>
        Z,
    }
    public enum FileExtension 
    {
        /// <summary>
        /// 副檔名(xml)
        /// </summary>
        xml,
        /// <summary>
        /// 副檔名(txt)
        /// </summary>
        txt,
        /// <summary>
        /// 副檔名(csv)
        /// </summary>
        csv,
        /// <summary>
        /// 副檔名(xls)
        /// </summary>
        xls,       
    }

}
