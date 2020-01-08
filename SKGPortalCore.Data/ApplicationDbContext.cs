using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData.OperateSystem;
using Toolbelt.ComponentModel.DataAnnotations;

namespace SKGPortalCore.Data
{
    public class ApplicationDbContext : DbContext// IdentityDbContext
    {
        #region Property
        public DbSet<OperateLog> OperateLog { get; set; }
        public DbSet<DataFlowNo> DataFlowNo { get; set; }
        #endregion
        #region Construct
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        #endregion
        #region Protected
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ModelDbSetting(builder);
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
            Type[] modelTypes = Assembly.Load("SKGPortalCore.Model").GetTypes().Where(p => (p.BaseType == typeof(DetailRowState) || p.BaseType == typeof(MasterDataModel) || p.BaseType == typeof(BillDataModel))).ToArray();
            foreach (Type type in modelTypes)
            {
                string tableName = type.Name;
                if (tableName.Substring(tableName.Length - 5, 5) == "Model")
                {
                    tableName = tableName.Substring(0, tableName.Length - 5);
                }

                string[] keyPropName = type.GetProperties().Where(p => p.CustomAttributes.Any(p => p.AttributeType == typeof(KeyAttribute))).Select(p => p.Name).ToArray();
                if (keyPropName.Length != 0)
                {
                    builder.Entity(type).ToTable(tableName).HasKey(keyPropName);
                }
                else
                {
                    builder.Entity(type).ToTable(tableName);
                }
            }
        }
        #endregion
    }

    public class LibDataAccess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext CreateDataAccess()
        {
            return new ApplicationDbContext(GetConnectionOption());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DbContextOptions<ApplicationDbContext> GetConnectionOption()
        {
            DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer("Server=.;Database=SKGPortalCore;Trusted_Connection=True;MultipleActiveResultSets=true");
            return builder.Options;
        }
    }
}
