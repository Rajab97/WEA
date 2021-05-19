using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Interfaces;
using WEA.Presentation.Helpers.Identity;

namespace WEA.Presentation.Services
{
    public class SessionService : ISessionService
    {
        private readonly CurrentUser _currentUser;

		private readonly ISession _session;

		public SessionService(IHttpContextAccessor httpContextRepository, CurrentUser currentUser)
		{
			_session = httpContextRepository.HttpContext.Session;
			_currentUser = currentUser;

			if (_session == null)
				throw new ArgumentNullException("Session cannot be null.");
		}
        public Guid? UserId { get { return _currentUser.UserId; } private set { } }

		public T Get<T>(string key)
		{
			string value = _session.GetString(key);
			if (string.IsNullOrWhiteSpace(value))
				return default(T);

			return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
		}

		public void Set<T>(string key, T value)
		{
			if (value == null)
				Remove(key);
			else
				_session.SetString(key, Newtonsoft.Json.JsonConvert.SerializeObject(value));
		}

		public void Remove(params string[] keys)
		{
			if (!keys.Any())
				return;

			foreach (string key in keys)
				_session.Remove(key);
		}
	}
}
