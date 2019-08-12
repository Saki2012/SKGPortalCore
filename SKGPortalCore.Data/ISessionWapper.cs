using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SKGPortalCore.Model.MasterData.OperateSystem;

namespace SKGPortalCore.Data
{
    public interface ISessionWapper
    {
        public string SessionId { get; }
        IUserModel User { get; set; }
        void Clear();
    }
    public interface ISessionWapper<T> : ISessionWapper where T : IUserModel { }
    public class SessionWapper<T> : ISessionWapper<T>, ISessionWapper where T : IUserModel
    {
        private static readonly string _userKey = "session.user";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionWapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private ISession Session
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session;
            }
        }
        public IUserModel User
        {
            get
            {
                return Session.GetObject<T>(_userKey);
            }
            set
            {
                Session.SetObject(_userKey, value);
            }
        }
        public string SessionId
        {
            get
            {
                return Session.Id;
            }
        }
        public void Clear()
        {
            Session.Clear();
        }
    }
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
