using GraphQL;
using GraphQL.Types;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.BillData;
using SKGPortalCore.Model.System;
using SKGPortalCore.Repository.BillData;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace SKGPortalCore.Graph.BillData
{
    //Schema
    public class BillSchema : BaseSchema<BillQuery, BillMutation>
    {
        public BillSchema(IDependencyResolver resolver) : base(resolver) { }
    }
    //Operate
    public class BillQuery : BaseQueryType<BillSet, BillSetType, BillType>
    {
        public BillQuery(BillRepository repository, ISessionWrapper session) : base(repository, session) { }
    }
    public class BillMutation : BaseMutationType<BillSet, BillSetType, BillSetInputType>
    {
        public BillMutation(BillRepository repository, ISessionWrapper session) : base(repository, session)
        {
            Field(
            type: typeof(BooleanGraphType),
            name: SystemCP.GQL_UploadFile,
            description: ResxManage.GetDescription(SystemCP.GQL_UploadFile),
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<FileInfo>> { Name = SystemCP.GQL_FileInfo, Description = ResxManage.GetDescription(SystemCP.GQL_FileInfo) }
            ),
            resolve: context =>
            {
                FileInfoModel file = context.GetArgument<FileInfoModel>(SystemCP.GQL_FileInfo);
                byte[] bytes = file.Content.Select(x => LibData.ToByte(x)).ToArray();
                File.WriteAllBytes(@"C:\Users\Suikoden\Desktop\zxccxz\zxccxzC.7z", bytes);
                return true;
            });

            Field(
            type: typeof(BooleanGraphType),
            name: nameof(BillRepository.BillPayProgressRpt),
            description: ResxManage.GetDescription(typeof(BillRepository), nameof(BillRepository.BillPayProgressRpt)),
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<FileInfo>> { Name = SystemCP.GQL_FileInfo, Description = ResxManage.GetDescription(SystemCP.GQL_FileInfo) }
            ),
            resolve: context =>
            {
                return repository.BillPayProgressRpt("", "");
            });



        }
    }
    //Input
    public class BillSetInputType : BaseInputSetGraphType<BillSet> { }
    public class BillInputType : BaseInputFieldGraphType<BillModel>
    {
        protected override PropertyInfo[] SetExpectProperties(Expression<Func<BillModel, dynamic>> propertyExpression)
        {
            return base.SetExpectProperties(p => new dynamic[] { p.PayAmount, p.HadPayAmount, p.VirtualAccountCode, p.ImportBatchNo, p.PayStatus });
        }
    }
    public class BillDetailInputType : BaseInputFieldGraphType<BillDetailModel> { }
    public class BillReceiptDetailInputType : BaseInputFieldGraphType<BillReceiptDetailModel> { }
    //Query
    public class BillSetType : BaseQuerySetGraphType<BillSet> { }
    public class BillType : BaseQueryFieldGraphType<BillModel> { }
    public class BillDetailType : BaseQueryFieldGraphType<BillDetailModel> { }
    public class BillReceiptDetailType : BaseQueryFieldGraphType<BillReceiptDetailModel> { }
}
