namespace SessionHelpers
{
    public interface ISessionHelper 
    {
        object this[string key]
        {
            get;
            set;
        }
    }

    public class SessionHelper : ISessionHelper
    {
        private HttpContext _HttpContext;
        private const string SESSION_ID_KEY = "SESSION_ID";
        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContext = httpContextAccessor.HttpContext;
        }

        public object this[string key] 
        {
            get
            {
                if(_HttpContext.Session.TryGetValue(SESSION_ID_KEY, out byte[] id))
                {
                    Guid sessionGuid = new Guid(id);
                    return SessionDataStore.Get(sessionGuid, key);
                }
                return null;
            }
            set
            {
                if (!_HttpContext.Session.TryGetValue(SESSION_ID_KEY, out byte[] id))
                {
                    id = Guid.NewGuid().ToByteArray();
                    _HttpContext.Session.Set(SESSION_ID_KEY, id);
                }
                SessionDataStore.Set(new Guid(id), key, value);
            }
        }
    }
    public static class SessionDataStore
    {
        public static Dictionary<Guid, Dictionary<string, object>> _AllSessionData = new Dictionary<Guid, Dictionary<string, object>>();

        public static object Get(Guid sessionId, string key)
        {
            if(_AllSessionData.ContainsKey(sessionId))
            {
                Dictionary<string, object> userSessionData = _AllSessionData[sessionId];
                
                if(userSessionData.ContainsKey(key))
                {
                    return userSessionData[key];
                }
            }
            return null;
        }

        public static void Set(Guid sessionId, string key, object value)
        {
            if (!_AllSessionData.ContainsKey(sessionId))
            {
                _AllSessionData[sessionId] = new Dictionary<string, object>();
            }

            _AllSessionData[sessionId][key] = value;
        }
    }
}
