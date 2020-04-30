using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SKGPortalCore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
        }
    }
}
/* 開發規範
 *********************************************************************************************
 *效率提升規範：                                                                             *
 * 1. string比較時，使用.Equals()                                                            *
 * 2. 除特殊情況，避免循環SQL Commit資料                                                     *
 *--------------------------------------------------------------------------------------------
 *開發規範：                                                                                 *
 * 1. 除組合、簡單符號、SystemCP外，一律禁止在程式碼內寫死值                                 *
 *  Ex: string msg="錯誤訊息："、 int times=20...等                                          *
 * 2. 一段程式碼(Function/函數)總行數不得超過「15行」                                        *
 * 3. Public Function寫功能大綱、Private Function寫實際業務邏輯                              *
 * 4. 區域變數名稱首字需小寫                                                                 *
 * 5. 除特殊情況(Ex:為了大批數據更新的效率等問題)，在對其他單進行「增刪改」等動作時，        *
 *  一律使用該單的Repository進行動作                                                         *
 * 6. 每一Function應註明Summary                                                              *
 *-------------------------------------------------------------------------------------------- 
 *Model建置規範(1)                                                                           *
 * 1. 表單
 *  A. 
 *  B. 
 * 2. 主資料
 *  A. 
 *  B. 
 * 3. 報表
 *--------------------------------------------------------------------------------------------
 *Repository建置規範(2)                                                                      *
 * 1. 表單                                                                                   *
 *  A. 繼承「BasicRepository」                                                               *
 * 2. 報表                                                                                   *
 *  A. 繼承「BasicRptRepository」                                                            *
 *--------------------------------------------------------------------------------------------
 *GraphQL建置規範(3)                                                                         *
 * 1.                                                                                        *
 *--------------------------------------------------------------------------------------------
 ********************************************************************************************* 
 */
