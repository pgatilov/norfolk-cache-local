using System;
using System.Collections.Generic;

namespace NorfolkCache.Services
{
    public abstract class CacheServiceDecorator : ICacheService
    {
        private readonly ICacheService _cacheService;

        public CacheServiceDecorator(ICacheService cacheService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public virtual void Clear()
        {
            _cacheService.Clear();
        }

        public virtual CacheServiceInfo GetInfo()
        {
            return _cacheService.GetInfo();
        }

        public virtual IList<string> GetNamespaces()
        {
            return _cacheService.GetNamespaces();
        }

        public virtual void RemoveKey(string @namespace, string key)
        {
            _cacheService.RemoveKey(@namespace, key);
        }

        public virtual void RemoveNamespace(string @namespace)
        {
            _cacheService.RemoveNamespace(@namespace);
        }

        public virtual void Set(string @namespace, string key, string value)
        {
            _cacheService.Set(@namespace, key, value);
        }

        public virtual bool TryGet(string @namespace, string key, out string value)
        {
            return _cacheService.TryGet(@namespace, key, out value);
        }

        public virtual bool TryGetNamespaceKeys(string @namespace, out IList<string> keys)
        {
            return _cacheService.TryGetNamespaceKeys(@namespace, out keys);
        }
    }
}
