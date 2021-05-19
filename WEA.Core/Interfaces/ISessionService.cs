using System;
using System.Collections.Generic;
using System.Text;

namespace WEA.Core.Interfaces
{
    public interface ISessionService
    {
        public Guid? UserId { get; }
        T Get<T>(string key);
        void Set<T>(string key, T value);

        void Remove(params string[] keys);
    }
}
