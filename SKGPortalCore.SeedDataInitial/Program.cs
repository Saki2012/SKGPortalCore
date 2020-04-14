using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.SKGPortalCore.Business.Import;
using SKGPortalCore.SeedDataInitial.BillData;
using SKGPortalCore.SeedDataInitial.MasterData;
using SKGPortalCore.SeedDataInitial.SourceData;
using System;
using System.Text;

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
            //資料
            CreateImportDataSources();
            DataAccess.Database.BeginTransaction();
            try
            {
                CreateSeedData_MasterData();
                CreateSeedData_BillData();
                ImportReceiptData();
                DataAccess.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                DataAccess.Database.RollbackTransaction();
                throw ex;
            }
        }
        /// <summary>
        /// 新增初始資料
        /// </summary>
        /// <param name="DataAccess"></param>
        private static void CreateSeedData_MasterData()
        {
            try
            {
                if (DataAccess.Set<BackendUserModel>().Find(SystemCP.SysOperator) == null) DataAccess.Add(SystemOperator.SysOperator);
                WorkDatesSeedData.CreateWorkDates(DataAccess);//OK
                DeptSeed.CreateDept(Message, DataAccess);//OK
                RoleSeeddData.CreateRole(Message, DataAccess);//OK
                ChannelSeedData.CreateChannel(Message, DataAccess);//OK
                CollectionTypeSeedData.CreateCollectionType(Message, DataAccess);//OK
                CustomerSeedData.CreateCustomer(Message, DataAccess);//OK
                PayerSeedData.CreatePayer(Message, DataAccess);//OK
                BillTermSeedData.CreateBillTerm(Message, DataAccess);//OK
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

        private static void CreateSeedData_BillData()
        {
            try
            {
                BillSeedData.CreateBill(Message, DataAccess);
                if (Message.Errors.Count == 0)
                    DataAccess.BulkSaveChanges();
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
        }
    }
}
