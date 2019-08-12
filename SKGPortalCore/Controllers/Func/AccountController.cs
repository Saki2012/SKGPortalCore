using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SKGPortalCore.Business.Func;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SKGPortalCore.Controllers.Func
{
    [Route("[Controller]")]
    public class AccountController : Controller
    {
        #region Property
        private readonly ISessionWapper _sessionWapper;
        private readonly AccountRepository _accountRepository;
        #endregion
        #region Constructor
        public AccountController(AccountRepository accountRepository, ISessionWapper sessionWapper)
        {
            _sessionWapper = sessionWapper;
            _accountRepository = accountRepository;
        }
        #endregion
        #region Public
        [Route("")]
        public IActionResult Index()
        {
            return View("Login");
        }
        [HttpPost("[Action]")]
        public IActionResult Login(LoginInfo info)
        {
            CustUserSet userSet = _accountRepository.Login(info.CustomerId, info.UserId, info.Pasuwado);
            if (null == userSet) return BadRequest();
            _sessionWapper.User = userSet.User;
            Dictionary<string, string> permissions = BizAccount.GetRolePermissionsToken(_sessionWapper.SessionId, userSet.UserRoles);
            return Ok(permissions);
        }
        #endregion
    }
    public class LoginInfo
    {
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public string Pasuwado { get; set; }
    }
    #region Repository
    public class AccountRepository
    {
        ApplicationDbContext Database { get; }
        public AccountRepository(ApplicationDbContext db)
        {
            Database = db;
        }
        public CustUserSet Login(string customerId, string userId, string pasuwado)
        {
            string key = $"{customerId},{userId}";
            Func<CustUserModel, bool> where1 = new Func<CustUserModel, bool>(p => p.KeyId == key);
            Func<CustUserRoleModel, bool> where2 = new Func<CustUserRoleModel, bool>(p => p.KeyId == key);
            var UserRoles = Database.Set<CustUserRoleModel>().Include(p => p.Role).ThenInclude(role => role.Permissions)
                .Where(where2).ToList();
            CustUserSet user = new CustUserSet() { User = Database.Set<CustUserModel>().Find(), UserRoles = UserRoles };
            if (!BizAccount.CheckAccountPasuwado(user, pasuwado)) return null;
            return user;
        }
    }
    #endregion
}