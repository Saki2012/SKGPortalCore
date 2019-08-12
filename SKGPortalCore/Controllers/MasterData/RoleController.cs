using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Business.Func;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData.OperateSystem;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using SKGPortalCore.Controllers.BillData;

namespace SKGPortalCore.Controllers.MasterData
{
    #region Controller
    [Route("[Controller]")]
    public class RoleController : BaseController
    {
        public RoleController(IDocumentExecuter documentExecuter, RoleSchema schema, ISessionWapper sessionWapper) : base(documentExecuter, schema, sessionWapper) { }
    }
    #endregion

    #region GraphQL
    public class RoleSchema : Schema
    {
        public RoleSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RoleQuery>();
            Mutation = resolver.Resolve<RoleMutation>();
        }
    }
    public class RoleQuery : ObjectGraphType<object>
    {
        public RoleQuery(RoleRepository repository)
        {
            Field<RoleSetType>(
                name: "dataQuery",
                description: "角色權限查詢",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "RoleId", Description = "角色ID" }, new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" }),
                resolve: context =>
                {
                    if (!BizAccount.CheckAuthenticate(context, "Role", FuncAction.Query)) return null;
                    string roleId = context.GetArgument<string>("roleId");
                    return repository.QueryData(roleId);
                });
            //Field<ListGraphType<RoleType>>(
            //    name: "listQuery",
            //    description: "角色權限列表查詢",
            //    //arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "billNo" }),
            //    resolve: context =>
            //    {
            //        if (!BizAccount.CheckAuthenticate(context, "Role", FuncAction.Query)) return null;
            //        //string billNo = context.GetArgument<string>("billNo");
            //        return repository.QueryList(null);
            //    });
        }
    }
    public class RoleMutation : ObjectGraphType
    {
        public RoleMutation(RoleRepository repository)
        {
            Field<RoleSetType>(
                name: "Create",
                description: "新增角色",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "jwt", Description = "Token" },
                                              new QueryArgument<NonNullGraphType<RoleInputType>> { Name = "Role" },
                                              new QueryArgument<NonNullGraphType<ListGraphType<RolePermissionInputType>>> { Name = "RolePermission" }),
                resolve: context =>
                {
                    if (!BizAccount.CheckAuthenticate(context, "Role", FuncAction.Create)) return null;
                    RoleSet set = new RoleSet()
                    {
                        Role = context.GetArgument<RoleModel>("role"),
                        RolePermission = context.GetArgument<List<RolePermissionModel>>("rolePermission")
                    };
                    return repository.Create(set);
                });
            Field<RoleSetType>(
                name: "Update",
                description: "修改角色",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<BillInputType>> { Name = "ReceiptBill" }),
                resolve: context =>
                {
                    if (!BizAccount.CheckAuthenticate(context, "Role", FuncAction.Update)) return null;
                    RoleSet set = new RoleSet()
                    {
                        Role = context.GetArgument<RoleModel>("role"),
                        RolePermission = context.GetArgument<List<RolePermissionModel>>("rolePermission")
                    };
                    set.Role.ModifyStaff = ((ISessionWapper)context.UserContext).User.KeyId;
                    return repository.Update(set);
                });
            Field<RoleSetType>(
                name: "Delete",
                description: "刪除角色",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "RoleId" }),
                resolve: context =>
                {
                    if (!BizAccount.CheckAuthenticate(context, "Role", FuncAction.Delete)) return null;
                    string billNo = context.GetArgument<string>("RoleId");
                    repository.Delete(billNo);
                    return null;
                });
        }
    }
    public class RoleInputType : InputObjectGraphType<RoleModel>
    {
        public RoleInputType()
        {
            PropertyInfo[] properties = typeof(RoleModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }

    public class RolePermissionInputType : InputObjectGraphType<RolePermissionModel>
    {
        public RolePermissionInputType()
        {
            PropertyInfo[] properties = typeof(RolePermissionModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }
    public class RoleSetType : ObjectGraphType<RoleSet>
    {
        public RoleSetType()
        {
            Field<RoleType>("Role");
            Field<ListGraphType<RolePermissionType>>("RolePermission");
        }
    }
    public class RoleType : ObjectGraphType<RoleModel>
    {
        public RoleType()
        {
            PropertyInfo[] properties = typeof(RoleModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }
    public class RolePermissionType : ObjectGraphType<RolePermissionModel>
    {
        public RolePermissionType()
        {
            PropertyInfo[] properties = typeof(RolePermissionModel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type changeType = GraphQLChangeType.ChangeGrcaphQLType(property.PropertyType);
                if (property.PropertyType == changeType) continue;//暫時不處理特殊情況的Type(ex:enum、ModelClass)
                Field(changeType, property.Name, ResxManage.GetDescription(property));
            }
        }
    }
    #endregion

    #region Repository
    public class RoleRepository : BasicRepository<RoleSet>
    {
        public RoleRepository(ApplicationDbContext db) : base(db) { }
    }
    #endregion
}