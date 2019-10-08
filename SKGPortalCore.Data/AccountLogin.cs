using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using GraphQL.Types;
using SKGPortalCore.Data;
using SKGPortalCore.Lib;
using SKGPortalCore.Model;
using SKGPortalCore.Model.MasterData.OperateSystem;

namespace SKGPortalCore.Business.Func
{
    /// <summary>
    /// 帳戶管理
    /// </summary>
    public class AccountLogin
    {
        #region Public
        /// <summary>
        /// 獲取使用者的權限列表(JWT Token)
        /// </summary>
        /// <param name="secret">密鑰</param>
        /// <param name="userSet">使用者</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetRolePermissionsToken(string secret, List<CustUserRoleModel> userRoles)
        {
            Dictionary<string, int> funcPermissionDic = new Dictionary<string, int>();
            Dictionary<string, string> funcPermissionTokenDic = new Dictionary<string, string>();
            foreach (CustUserRoleModel userRole in userRoles)
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
                funcPermissionTokenDic.Add(funcName, LibJWT.GenerateToken(secret, funcName, funcPermissionDic[funcName].ToString()));
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
            if (null == userSet || pasuwado != userSet.User?.Pasuwado)
            {
                //查無資料(帳號or密碼錯誤 訊息)
                return false;
            }
            return userSet.User.AccountStatus switch
            {
                AccountStatus.Unable => false,
                AccountStatus.Freeze => false,
                _ => true,
            };
        }
        /// <summary>
        /// 驗證功能權限是否有效
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="token"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        public static bool CheckAuthenticate(ResolveFieldContext<object> context, string claimType, FuncAction claimValue)
        {
            return CheckAuthenticate(((ISessionWapper)context.UserContext)?.SessionId, context.GetArgument<string>("jwt"), claimType, claimValue);
        }
        /// <summary>
        /// 驗證功能權限是否有效
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="token"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        private static bool CheckAuthenticate(string secret, string token, string claimType, FuncAction claimValue)
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
