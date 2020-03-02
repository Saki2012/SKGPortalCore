using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.SourceData;
using SKGPortalCore.Repository.SKGPortalCore.Business.Import;
using SKGPortalCore.SeedDataInitial.BillData;
using SKGPortalCore.SeedDataInitial.MasterData;
using SKGPortalCore.SeedDataInitial.SourceData;

namespace SKGPortalCore.SeedDataInitial
{
    internal class Program
    {
        private static readonly ApplicationDbContext DataAccess = LibDataAccess.CreateDataAccess();
        private static readonly SysMessageLog Message = new SysMessageLog(SystemOperator.SysOperator, logFileName: "SKGPortalCore.SeedDataInitial");
        private static IImportData ImportData { get; set; }

        public static void Main()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (DataAccess.Set<BackendUserModel>().Find(ConstParameter.SysOperator) == null) DataAccess.Add(SystemOperator.SysOperator);
            //資料
            CreateImportDataSources();
            CreateSeedData(DataAccess);
            ImportReceiptData();
        }
        /// <summary>
        /// 新增初始資料
        /// </summary>
        /// <param name="dataAccess"></param>
        private static void CreateSeedData(ApplicationDbContext dataAccess)
        {
            try
            {
                CustomerSeedData.CreateCustomer(Message, dataAccess);
                DeptSeed.CreateDept(Message, dataAccess);
                RoleSeeddData.CreateRole(Message, DataAccess);//OK
                ChannelSeedData.CreateChannel(Message, dataAccess);//OK
                CollectionTypeSeedData.CreateCollectionType(Message, dataAccess);//OK
                PayerSeedData.CreatePayer(Message, dataAccess);
                BillTermSeedData.CreateBillTerm(Message, dataAccess);

                //單據
                BillSeedData.CreateBill(Message, dataAccess);


                //List<WorkDateModel> workDates = new List<WorkDateModel>();
                //DateTime date = DateTime.Now.AddYears(-1).Date;
                //DateTime date2 = DateTime.Now.AddYears(1).Date;
                //while (date != date2)
                //{
                //    workDates.Add(new WorkDateModel() { Date = date, Description = "", HolidayCategory = "", IsWorkDate = !date.DayOfWeek.In(DayOfWeek.Sunday, DayOfWeek.Saturday), Name = "" });
                //    date = date.AddDays(1);
                //}
                //dataAccess.WorkDate.AddRange(workDates);

                dataAccess.BulkSaveChanges();
            }
            catch (Exception e)
            {
                Message.AddExceptionError(e);
                throw;
            }
            finally
            {
                Message.WriteLogTxt();
            }
        }
        /// <summary>
        /// 新增資訊流源(txt檔案)
        /// </summary>
        private static void CreateImportDataSources()
        {
            try
            {
                //ACCFTTSeedData.ACCFTTData(Message);
                ReceiptInfoBankSeedData.ReceiptInfoBankData(Message);
                ReceiptInfoPostSeedData.ReceiptInfoPostData(Message);
                ReceiptInfoMarketSeedData.ReceiptInfoMarketData(Message);
                //RemitInfoSeedData.RemitInfoData(Message);
            }
            catch (Exception e)
            {
                Message.AddExceptionError(e);
                throw;
            }
            Message.WriteLogTxt();
        }
        /// <summary>
        /// 導入資訊流
        /// </summary>
        private static void ImportReceiptData()
        {
            ImportData = new ReceiptInfoImportBANK(DataAccess); ImportData.ExecuteImport();
            ImportData = new ReceiptInfoImportPOST(DataAccess); ImportData.ExecuteImport();
            ImportData = new ReceiptInfoImportMARKET(DataAccess); ImportData.ExecuteImport();
            //ImportData = new ReceiptInfoImportMARKETSPI(DataAccess); ImportData.ExecuteImport();
            //ImportData = new ReceiptInfoImportFARM(DataAccess); ImportData.ExecuteImport();
            //ImportData = new RemitInfoImport(DataAccess); ImportData.ExecuteImport();
        }
    }
}
