using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SKGPortalCore.Core.Model;
using SKGPortalCore.Core.SystemTable;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Toolbelt.ComponentModel.DataAnnotations;

namespace SKGPortalCore.Core.DB
{
    public class ApplicationDbContext : DbContext
    {
        #region Property
        /// <summary>
        /// 操作日誌
        /// </summary>
        public DbSet<OperateLog> OperateLog { get; set; }
        /// <summary>
        /// 流水號
        /// </summary>
        public DbSet<DataFlowNo> DataFlowNo { get; set; }
        /// <summary>
        /// 變更日誌
        /// </summary>
        public DbSet<DataChangeLog> DataChangeLog { get; set; }
        /// <summary>
        /// 變更日誌明細
        /// </summary>
        public DbSet<DataChangeLogDetail> DataChangeLogDetail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public DbSet<WorkDateModel> WorkDate { get; set; }
        #endregion

        #region Construct
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        #endregion

        #region Protected
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ModelDbSetting(builder);
            builder.Entity<DataChangeLogDetail>().HasKey(p => new { p.DataChangeId, p.RowId });
            builder.BuildIndexesFromAnnotations();//設置Index套件
        }
        #endregion

        #region Private
        /// <summary>
        /// 模型與Database綁定設置
        /// </summary>
        /// <param name="builder"></param>
        private void ModelDbSetting(ModelBuilder builder)
        {
            Type[] modelTypes = Assembly.Load("SKGPortalCore.Model").GetTypes().Where(p =>
            p.BaseType == typeof(DetailRowState) || p.BaseType == typeof(MasterDataModel) || p.BaseType == typeof(BillDataModel) ||
            p.Namespace.Contains("SKGPortalCore.Model.SystemTable")
            ).ToArray();
            foreach (Type type in modelTypes)
            {
                string tableName = type.Name;
                if (tableName.Substring(tableName.Length - 5, 5) == SystemCP.Model) tableName = tableName[0..^5];
                string[] keyPropName = type.GetProperties().Where(p => p.IsDefined(typeof(KeyAttribute))).Select(p => p.Name).ToArray();
                if (keyPropName.Length != 0) builder.Entity(type).ToTable(tableName).HasKey(keyPropName);
                else builder.Entity(type).ToTable(tableName);
            }
        }
        #endregion
    }

    public static class LibDataAccess
    {
        #region Property
        /// <summary>
        /// 
        /// </summary>
        private static readonly IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(SystemCP.AppSettingsJsonPath).AddJsonFile(SystemCP.AppSettingsJson).Build();
        #endregion
        #region Public
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext CreateDataAccess(IConfiguration config = null)
        {
            return new ApplicationDbContext(GetConnectionOption(config));
        }
        #endregion
        #region Private
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static DbContextOptions<ApplicationDbContext> GetConnectionOption(IConfiguration config)
        {
            if (config is null) config = Configuration;
            DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(config.GetConnectionString(SystemCP.SqlConnection), b => b.MigrationsAssembly(nameof(SKGPortalCore)));
            return builder.Options;
        }
        #endregion
    }
}
