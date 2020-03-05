using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace SKGPortalCore.SeedDataInitial.MasterData
{
    public static class DeptSeed
    {
        public static void CreateDept(SysMessageLog Message, ApplicationDbContext DataAccess)
        {
            try
            {
                Message.Prefix = "新增「部門」-初始資料：";
                using DeptRepository repo = new DeptRepository(DataAccess) { User = SystemOperator.SysOperator, Message = Message };
                List<DeptSet> depts = new List<DeptSet>()
                {
                    new DeptSet() { Dept=new DeptModel(){ DeptId="0019", DeptName="中正分行",   IsBranch=true } },
                    new DeptSet() { Dept=new DeptModel(){ DeptId="0028", DeptName="東台北分行", IsBranch=true } },
                    new DeptSet() { Dept=new DeptModel(){ DeptId="0037", DeptName="龍山分行",   IsBranch=true } },
                    new DeptSet() { Dept=new DeptModel(){ DeptId="0046", DeptName="西園分行",   IsBranch=true } },
                    new DeptSet() { Dept=new DeptModel(){ DeptId="0055", DeptName="西門分行",   IsBranch=true } },

                };

                depts.ForEach(dept =>
                {
                    if (null == repo.QueryData(new[] { dept.Dept.DeptId })) repo.Create(dept);
                });
                repo.CommitData(FuncAction.Create);
            }
            finally
            {
                Message.Prefix = string.Empty;
            }
        }
    }
}
