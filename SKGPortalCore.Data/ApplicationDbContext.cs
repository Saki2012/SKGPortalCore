using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Models.BillData;

namespace SKGPortalCore.Data
{
    public class ApplicationDbContext : DbContext// IdentityDbContext
    {
        #region Property
        public DbSet<BillModel> Bill { get; set; }
        public DbSet<BillDetailModel> BillDetail { get; set; }
        public DbSet<BillReceiptDetailModel> BillReceiptDetail { get; set; }
        public DbSet<ReceiptBillModel> ReceiptBill { get; set; }
        public DbSet<CustomerModel> Customer { get; set; }
        public DbSet<RoleModel> Role { get; set; }
        public DbSet<RolePermissionModel> RolePermission { get; set; }
        public DbSet<BackendUserModel> BackendUser { get; set; }
        public DbSet<BackendUserRoleModel> BackendUserRole { get; set; }
        public DbSet<CustUserModel> CustUser { get; set; }
        public DbSet<CustUserRoleModel> CustUserRole { get; set; }
        public DbSet<ChannelModel> Channel { get; set; }
        public DbSet<ChannelMapModel> ChannelMap { get; set; }
        public DbSet<CollectionTypeModel> CollectionType { get; set; }
        public DbSet<CollectionTypeDetailModel> CollectionTypeDetail { get; set; }
        public DbSet<BizCustomerModel> BizCustomer { get; set; }
        public DbSet<BizCustFeeDetailModel> BizCustFeeDetail { get; set; }
        public DbSet<BillTermModel> BillTerm { get; set; }
        public DbSet<BillTermDetailModel> BillTermDetail { get; set; }
        public DbSet<PayerModel> Payer { get; set; }

        public DbSet<ChannelEAccountBillModel> ChannelEAccountBill { get; set; }
        public DbSet<ChannelEAccountBillDetailModel> ChannelEAccountBillDetail { get; set; }
        #endregion
        #region Private
        /// <summary>
        /// 建立多主鍵
        /// 註1：目前EFCore暫時不支援直接在Model上建立多主鍵Attribute進行Migration
        /// 參考資料：https://stackoverflow.com/questions/43246727/possible-to-set-column-ordering-in-entity-framework
        /// 註2：多主鍵的情況下仍需建立多個KeyAttribute,供BaseControl基底使用該Attribute來判斷主鍵
        /// </summary>
        /// <param name="builder"></param>
        private void SetMultiplePK(ModelBuilder builder)
        {
            builder.Entity<BillDetailModel>(entity =>
            {
                entity.HasKey(tb => new { tb.BillNo, tb.RowId });
            }).
            Entity<BillReceiptDetailModel>(entity =>
            {
                entity.HasKey(tb => new { tb.BillNo, tb.RowId });
            }).
            Entity<BackendUserRoleModel>(entity =>
           {
               entity.HasKey(tb => new { tb.KeyId, tb.RoleId });
           }).
            Entity<CustUserRoleModel>(entity =>
            {
                entity.HasKey(tb => new { tb.KeyId, tb.RoleId });
            }).
            Entity<RolePermissionModel>(entity =>
            {
                entity.HasKey(tb => new { tb.RoleId, tb.RowId });
            }).
            Entity<BizCustFeeDetailModel>(entity =>
            {
                entity.HasKey(tb => new { tb.CustomerCode, tb.RowId });
            }).
            Entity<CollectionTypeDetailModel>(entity =>
            {
                entity.HasKey(tb => new { tb.CollectionTypeId, tb.RowId });
            }).
            Entity<PayerModel>(entity =>
            {
                entity.HasKey(tb => new { tb.CustomerId, tb.PayerId });
            }).
            Entity<ChannelMapModel>(entity =>
            {
                entity.HasKey(tb => new { tb.ChannelId, tb.TransCode });
            }).
            Entity<BillTermModel>(entity =>
            {
                entity.HasKey(tb => new { tb.CustomerCode, tb.BillTermId });
            }).
            Entity<BillTermDetailModel>(entity =>
            {
                entity.HasKey(tb => new { tb.CustomerCode, tb.BillTermId, tb.RowId });
            })
            ;
        }
        #endregion
        #region Protected
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SetMultiplePK(builder);
        }
        #endregion
        #region Construct
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        #endregion
    }
}
