using GraphQL.Types;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using SKGPortalCore.Core.Model.User;
using SKGPortalCore.Core.Repository;
using SKGPortalCore.Core.Repository.Interface;
using SKGPortalCore.Core.Libary;
using SKGPortalCore.Core.Model;
using SKGPortalCore.Core.LibEnum;

namespace SKGPortalCore.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class BizAccountLogin
    {
        #region Public
        /// <summary>
        /// 獲取使用者的權限列表(JWT Token)
        /// </summary>
        /// <param name="secret">密鑰</param>
        /// <param name="userSet">使用者</param>
        /// <returns></returns>
        public static List<PermissionTokenModel> GetRolePermissionsToken(string secret, List<IRoleModel> userRoles)
        {
            Dictionary<string, int> funcPermissionDic = new Dictionary<string, int>();
            List<PermissionTokenModel> funcPermissionTokenDic = new List<PermissionTokenModel>();
            foreach (IRoleModel userRole in userRoles)
            {
                foreach (RolePermissionModel permission in userRole.Role.Permissions)
                {
                    if (!funcPermissionDic.ContainsKey(permission.FuncName))
                    {
                        funcPermissionDic.TryAdd(permission.FuncName, 0);
                    }
                    funcPermissionDic[permission.FuncName] |= permission.FuncAction;
                }
            }
            foreach (string funcName in funcPermissionDic.Keys)
            {
                funcPermissionTokenDic.Add(new PermissionTokenModel() { FuncName = funcName, Token = LibJWT.GenerateToken(secret, funcName, funcPermissionDic[funcName].ToString()) });
            }
            return funcPermissionTokenDic;
        }
        /// <summary>
        /// 確認輸入帳號是否有誤
        /// </summary>
        /// <param name="userSet"></param>
        /// <param name="pasuwado"></param>
        /// <returns></returns>
        public static bool CheckAccountPasuwado(CustUserSet userSet, string pasuwado)
        {
            if (userSet is null || !pasuwado.Equals(userSet.CustUser?.Pasuwado))
            {
                //查無資料(帳號or密碼錯誤 訊息)
                return false;
            }
            return userSet.CustUser.AccountStatus switch
            {
                AccountStatus.Unable => false,
                AccountStatus.Freeze => false,
                _ => true,
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userSet"></param>
        /// <param name="pasuwado"></param>
        /// <returns></returns>
        public static bool CheckADAccountPasuwado(BackendUserSet userSet, string pasuwado)
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSet"></typeparam>
        /// <param name="context"></param>
        /// <param name="repository"></param>
        /// <param name="sessionId"></param>
        /// <param name="progId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool CheckAuthenticate<TSet>(ResolveFieldContext<object> context, IBasicRepository<TSet> repository, string sessionId, string progId, FuncAction action)
        {
            if (repository.User.KeyId != SystemOperator.SysOperator.KeyId && !CheckAuthenticate(sessionId, context.GetArgument<string>(SystemCP.JWT), progId, action))
            {
                repository.Message.AddCustErrorMessage(MessageCode.Code0002, ResxManage.GetDescription<TSet>(), ResxManage.GetDescription(action));
                context.Errors.AddRange(repository.Message.Errors);
                repository.Message.WriteLogTxt();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 驗證功能權限是否有效
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="token"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        public static bool CheckAuthenticate(string secret, string token, string claimType, FuncAction claimValue)
        {
            if (!LibJWT.TryValidateToken(secret, token, out ClaimsPrincipal principal))
            {
                return false;
            }
            string srcActionType = principal.Claims.Where(c => c.Type == "ClaimType").Select(c => c.Value).SingleOrDefault().ToString();
            int srcAction = principal.Claims.Where(c => c.Type == "ClaimValue").Select(c => c.Value).SingleOrDefault().ToInt32();
            if (srcActionType.CompareTo(claimType) != 0 || (int)claimValue != ((int)claimValue & srcAction))
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
