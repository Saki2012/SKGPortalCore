using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SKGPortalCore.Business.Func;
using SKGPortalCore.Data;
using SKGPortalCore.Model.MasterData.OperateSystem;
using SKGPortalCore.Repository.Func;

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
}