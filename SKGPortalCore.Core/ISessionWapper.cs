using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SKGPortalCore.Core.Model.User;

namespace SKGPortalCore.Core
{
    public interface ISessionWrapper
    {
        public string SessionId { get; }
        IUserModel User { get; set; }
        public string IP { get; }
        public string Browser { get; }
        void Clear();
    }
    public interface ISessionWapper<T> : ISessionWrapper where T : IUserModel { }
    [Serializable]
    public class SessionWapper<T> : ISessionWapper<T>, ISessionWrapper where T : IUserModel
    {
        private static readonly string _userKey = SystemCP.UserKey;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionWapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private ISession Session => _httpContextAccessor.HttpContext.Session;
        public IUserModel User
        {
            get => Session.GetObject<T>(_userKey);
            set => Session.SetObject(_userKey, value);
        }
        public string IP => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        public string Browser => _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
        public string SessionId => Session.Id;
        public void Clear()
        {
            Session.Clear();
        }
    }
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value) where T : IUserModel
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetObject<T>(this ISession session, string key) where T : IUserModel
        {
            string value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
