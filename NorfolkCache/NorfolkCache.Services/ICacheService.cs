using System.Collections.Generic;

namespace NorfolkCache.Services
{
    // TODO void Append(string @namespace, string key, string value);
    public interface ICacheService : ICacheServiceInfoProvider
    {
        IList<string> GetNamespaces();

        bool TryGetNamespaceKeys(string @namespace, out IList<string> keys);

        bool TryGet(string @namespace, string key, out string value);

        void RemoveKey(string @namespace, string key);

        void RemoveNamespace(string @namespace);

        void Set(string @namespace, string key, string value);

        void Clear();
    }
}
