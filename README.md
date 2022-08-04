這是一個控制台程式，可以將內政部實價登入上的交易資料下載下來：[實價登入網址](https://plvr.land.moi.gov.tw/DownloadOpenData "實價登入網址") 
主要由兩個類組成：DownloadFileAciton和DownloadPath。
DownloadPath：主要功用為建置各個實價登入檔案下載路徑之參數，包括是否為當期下載、交易的季節、交易種類、以及交易的城市，可以自行創建單一實例並初始化給各屬性賦值，也可以使用工廠方法來
　　　　　　　　創建實例集合。
DownloadFileAciton：提供不同靜態下載方法，有單一檔案下載的DownloadSingleFileAsync和可以下載多個檔案的DownloadFilesAsync，輸入參數都是DownloadPath之實例。
